/*
 * SoundManager(사운드매니저)
 * ---------------------------------------------------------------
 * 게임 전역에서 BGM(배경음악)과 SFX(효과음)를 통합 관리하는 매니저 클래스
 * 
 * 주요 기능:
 * - AudioSource와 AudioClip을 묶은 AudioUnit 단위로 사운드를 관리함.
 * - Enum(SoundName)을 통해 BGM과 SFX를 구분하여 재생/중단할 수 있다.
 * - Inspector를 통해 손쉽게 AudioSource, AudioClip을 세팅할 수 있도록 설계함.
 * - PlayBGM(), PlaySFX(), StopBGM() 메소드 호출로 다른 스크립트에서 사용가능
 * 
 * 설계 특징:
 * - AudioSource와 AudioClip을 이원화하여 별도의 소스 추가 작업 없이 관리할 수 있다.
 * - 이름이 아닌 Enum 기반으로 재생 대상을 지정하여 오타를 방지하고 코드 안정성을 높임.
 * - Singleton 패턴을 사용해 SoundManager 인스턴스를 하나만 유지하며, 씬이 바뀌어도 파괴되지 않는 특성을 지님.
 * 
 * 확장성:
 * - BGM과 SFX를 추가할 때마다 AudioUnit을 Inspector에 추가만 하면 되므로 코드 수정 없이 추가 가능
 * - 추가 기능인 옵션 메뉴를 통한 볼륨 조정, 페이드 인/아웃 기능 등 추가 확장이 용이하도록 설계함.
 * 
 * 사용 방법:
 * 1. SoundManager 오브젝트로 이동 → Audio Units내 리스트를 추가하고 SoundName, Audio Source, Audio Clip 파일 지정
 * 2. PlayBGM() 또는 PlaySFX() 메서드를 호출해 사운드를 재생가능
 * 3. 필요 시 StopBGM()으로 특정 BGM을 정지가능
 * ---------------------------------------------------------------
 */
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Enum"은 "Enumeration"의 약자로, 특정 자료형의 값을 미리 정의된 값들로 제한하여 사용하고자 할 때 사용하는 데이터 타입
/// <summary> 게임 내 사용된 모든 BGM/SFX 목록 Enum 타입 </summary>
public enum SoundName
{
    //배경음악
    BGM_Title,      //타이틀 배경음악
    BGM_MainMenu,   //메인메뉴 배경음악
    BGM_StageBGM1,  //스테이지1 배경음악
    BGM_StageBGM2,  //스테이지2 배경음악

    //효과음
    SFX_GameOver,   //게임 오버 효과음
    SFX_GameClear,  //게임 클리어 효과음
    SFX_Jump,       //점프 효과음
    SFX_ButtonClick //버튼 클릭 효과음
}
//[System.Serializable] : C#의 속성(attribute) / 클래스, 구조체, 필드에 적용가능 / 직렬화를 위해 추가함, 해당 구문을 추가함으로서 Inspector에 표출됨
//Inspector에서 쉽게 관리 하기위해 통합 AudioUnit 클래스 생성

/// <summary> AudioSource와 AudioClip을 하나의 유닛으로 묶은 클래스</summary>
[System.Serializable]
public class AudioUnit
{
    /*
     * C#에는 C++의 friend 개념이 없기에, 객체지향을 유지한 채로 회피하는 방법을 사용한다.
     * Unity에서 권장하는 구조인 [SerializeField] private + public getter 방식을 채택함
     * 따라서 AudioUnit 클래스는 필드는 [SerializeField] private로 숨기고 읽기 전용 public 프로퍼티를 제공함
     */
    [SerializeField] private SoundName soundName = 0;         //재생할 사운드 이름(Enum으로 지정)
    [SerializeField] private AudioSource audioSource = null;  //소리를 출력하는 AudioSource
    [SerializeField] private AudioClip audioClip = null;      //출력할 오디오 데이터 AudioClip

    /* 읽기 전용(Read-Only) 프로퍼티(외부에서 참조가능 수정은 불가능)
     * 프로퍼티(Property)는 속성이란 의미.
     * 프로퍼티를 사용하게 되면, 속성 값을 반환하거나 새 값을 할당할 수 있다.
     */
    public SoundName SoundName { get { return soundName; } }
    public AudioSource AudioSource { get { return audioSource; } }
    public AudioClip AudioClip { get { return audioClip; } }
}

/// <summary> 게임 전역에서 사운드를 관리하는 매니저 클래스 </summary>
public class SoundManager : MonoBehaviour
{
    [Header("Audio Units")] //Inspector에서 리스트를 구분하기 위해 머릿말("Audio Units") 추가
    [SerializeField] private List<AudioUnit> UnitBGM = new List<AudioUnit>(); //BGM 전용 AudioUnit 리스트
    [SerializeField] private List<AudioUnit> UnitSFX = new List<AudioUnit>(); //SFX 전용 AudioUnit 리스트

    // singleton pattern: 클래스 하나에 인스턴스가 하나만 생성되는 프래그래밍 패턴
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("SoundManager is null.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; //this : 현재 인스턴스를 가리키는 레퍼런스
        }
        else if (_instance != this)
        {
            Debug.Log("SoundManager has another instance.");

            Destroy(gameObject); //현재 인스턴스 파괴(GameManger Object)
        }
        DontDestroyOnLoad(gameObject); //씬이 변경되어도 현재 게임 오브젝트를 유지시키는 메소드
    }

    /*
     * foreach 문이란?
     * ---------------------------------------------------------------
     * 컬렉션(List, Array 등)에 저장된 모든 요소를 하나씩 꺼내어 반복 실행하는 반복문
     * 
     * 기본 구조:
     * foreach (var element in collection)
     * {
     *     // element에 대해 반복적으로 수행할 작업
     * }
     * 
     * 작동 방식:
     * - collection(리스트, 배열 등) 안에 있는 모든 요소를 처음부터 끝까지 하나씩 꺼냅니다.
     * - 꺼낸 요소를 임시 변수(var element)에 저장하여 블록 안에서 사용합니다.
     * - 컬렉션의 크기나 인덱스 관리를 신경쓰지 않아도 됩니다.
     * 
     * 장점:
     * - 코드가 간결해집니다.
     * - 인덱스 오류(예: IndexOutOfRange)를 방지할 수 있습니다.
     * - 읽기 전용 작업(값을 읽어오기)에 매우 적합합니다.
     * ---------------------------------------------------------------
     * 
     * foreach문을 사용하면 리스트 크기만큼 반복가능하므로 자동화를 위해 사용함
     */

    /// <summary> 매개변수로 전달받은 배경음악을 재생하는 메소드 </summary>
    public void f_PlayBGM(SoundName soundName, float volume)
    {
        //사용된 오디오 갯수만큼 반복한다면 자동화된 검색가능 
        foreach(AudioUnit unit in UnitBGM) //var대신 명확성을 위해 AudioUnit unit형태로 지정함
        {
            if(unit.SoundName == soundName) //매개변수 사운드를 찾으면?
            {
                unit.AudioSource.clip = unit.AudioClip; //AudioSource에 Clip 연결
                unit.AudioSource.volume = volume;       //매개변수로 받은 볼륨으로 지정
                unit.AudioSource.loop = true;           //반복 재생 설정
                unit.AudioSource.Play();                //재생 시작
                return; //재생이 시작되었으므로 foreach 루프 종료
            }
        }
        Debug.LogWarning($"BGM '{soundName}' not found!"); //매개변수로 전달받은 배경음악이 없을 경우 경고 출력
    }

    /// <summary> 매개변수로 전달받은 효과음을 재생하는 메소드 </summary>
    public void f_PlaySFX(SoundName soundName, float volume)
    {
        foreach(AudioUnit unit in UnitSFX)
        {
            if(unit.SoundName == soundName)
            {
                unit.AudioSource.volume = volume;               //매개변수로 받은 볼륨으로 지정
                unit.AudioSource.PlayOneShot(unit.AudioClip);   //PlayOneShot 메소드를 이용하여 1회만 효과음 재생
                return; //재생이 시작되었으므로 foreach 루프 종료
            }
        }
        Debug.LogWarning($"SFX '{soundName}' not found!"); //매개변수로 전달받은 효과음이 없을 경우 경고 출력
    }

    /// <summary> 매개변수로 전달받은 BGM을 재생 중지하는 메소드 </summary>
    public void f_StopBGM(SoundName soundName)
    {
        foreach(AudioUnit unit in UnitBGM)
        {
            if(unit.SoundName == soundName && unit.AudioSource.isPlaying) //재생중인 사운드 명칭 == 매개변수 사운드 명칭 and 사운드가 재생중
            {
                unit.AudioSource.Stop(); //재생중인 배경음악 정지
                return; //재생 정지되었으므로 foreach 루프 종료
            }
        }
    }

    /// <summary> 모든 BGM을 재생 중지하는 메소드 </summary>
    public void f_StopAllBGM()
    {
        foreach(AudioUnit unit in UnitBGM)
        {
            if(unit.AudioSource.isPlaying)
            {
                unit.AudioSource.Stop();
            }
        }
    }
    /*
     * 직접적인 public 선언은 객체 지향의 철학과 맞지 않기에 get, set 사용하여 접근
     * set은 private set으로 지정하여 읽기전용으로 변경
     * SoundManager 클래스 내에선 set 사용가능 / 외부클래스 접근불가
     */

    /* [Soure와 Clip 이원화 이유]
     * 1. Source 없이 Clip만 등록한다면?
     * AudioClip 데이터만 들고 있게 된다.
     * 재생하려면 별도로 AudioSource를 추가하고 연결해야 하는 문제점 발생
     * 로직이 비효율적이고 복잡해지는 결과를 초래한다.
     * 2. Source + Clip 같이 관리하면?
     * Source가 이미 세팅되어 있기 때문에
     * 코드로 재생, 정지, 볼륨 조정을 모두 할 수 있다.
     */

    /*
    //[SerializeField]: private 접근 제한을 유지하며 인스펙터에 노출되어 Unity 편집기로 설정할 수 있음.
    [SerializeField] private AudioSource sourceGameStageBGM = null; //게임 스테이지 배경음악 소스
    [SerializeField] private AudioSource sourceJumpSFX = null;      //점프 효과음 소스

    [SerializeField] private AudioClip clipGameStageBGM = null; //게임 스테이지 배경음악 클립
    [SerializeField] private AudioClip clipJumpSFX = null;      //점프 효과음 클립          

    

    //프로퍼티(Property)는 속성이란 의미.
    //프로퍼티를 사용하게 되면, 속성 값을 반환하거나 새 값을 할당할 수 있다.

    /// <summary> 게임 스테이지 배경음악 소스 접근자 </summary>
    public AudioSource SourceGameStageBGM
    {
        get { return sourceGameStageBGM; }
        private set { sourceGameStageBGM = value; }
    }

    /// <summary> 점프 효과음 소스 접근자 </summary>
    public AudioSource SourceJumpSFX
    {
        get { return sourceJumpSFX; }
        private set { sourceJumpSFX = value; }
    }

    /// <summary> 게임 스테이지 배경음악 클립 접근자 </summary>
    public AudioClip ClipGameStageBGM
    {
        get { return clipGameStageBGM; }
        private set { clipGameStageBGM = value; }
    }

    /// <summary> 점프 효과음 클립 접근자 </summary>
    public AudioClip ClipJumpSFX
    {
        get { return clipJumpSFX; }
        private set { clipJumpSFX = value; }
    }


    // singleton pattern: 클래스 하나에 인스턴스가 하나만 생성되는 프래그래밍 패턴
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; //this : 현재 인스턴스를 가리키는 레퍼런스
        }
        else if (_instance != this)
        {
            Debug.Log("SoundManager has another instance.");

            Destroy(gameObject); //현재 인스턴스 파괴(GameManger Object)
        }
        DontDestroyOnLoad(gameObject); //씬이 변경되어도 현재 게임 오브젝트를 유지시키는 메소드
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //f_PlayGameStageBGM(); //게임 배경음악 재생
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> 게임 스테이지의 배경음악을 반복재생하는 메소드 </summary>
    public void f_PlayGameStageBGM()
    {
        if (SourceGameStageBGM != null && ClipGameStageBGM != null) //소스와 클립이 모두 존재하는 경우(not null)
        {
            SourceGameStageBGM.clip = ClipGameStageBGM;
            SourceGameStageBGM.loop = true; //반복 재생 true값
            SourceGameStageBGM.volume = 0.1f; //볼륨 10%
            SourceGameStageBGM.Play(); //재생
        }
    }

    /// <summary> 게임 스테이지의 배경음악 재생을 중단하는 메소드 </summary>
    public void f_StopGameStageBGM()
    {
        if (SourceGameStageBGM != null && SourceGameStageBGM.isPlaying)
        {
            SourceGameStageBGM.Stop();
        }
    }

    /// <summary> 점프 효과음을 재생하는 메소드 </summary>
    public void f_PlayJumpSFX()
    {
        if (SourceJumpSFX != null && ClipJumpSFX != null) //소스와 클립이 모두 존재하는 경우(not null)
        {
            SourceJumpSFX.volume = 0.1f; //볼륨 10%
            SourceJumpSFX.PlayOneShot(ClipJumpSFX); //PlayOneShot 메소드를 이용하여 한 번만 재생
        }
    }

    */
}
