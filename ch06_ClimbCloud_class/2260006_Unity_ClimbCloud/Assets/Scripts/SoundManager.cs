/*
 * SoundManager(����Ŵ���)
 * ---------------------------------------------------------------
 * ���� �������� BGM(�������)�� SFX(ȿ����)�� ���� �����ϴ� �Ŵ��� Ŭ����
 * 
 * �ֿ� ���:
 * - AudioSource�� AudioClip�� ���� AudioUnit ������ ���带 ������.
 * - Enum(SoundName)�� ���� BGM�� SFX�� �����Ͽ� ���/�ߴ��� �� �ִ�.
 * - Inspector�� ���� �ս��� AudioSource, AudioClip�� ������ �� �ֵ��� ������.
 * - PlayBGM(), PlaySFX(), StopBGM() �޼ҵ� ȣ��� �ٸ� ��ũ��Ʈ���� ��밡��
 * 
 * ���� Ư¡:
 * - AudioSource�� AudioClip�� �̿�ȭ�Ͽ� ������ �ҽ� �߰� �۾� ���� ������ �� �ִ�.
 * - �̸��� �ƴ� Enum ������� ��� ����� �����Ͽ� ��Ÿ�� �����ϰ� �ڵ� �������� ����.
 * - Singleton ������ ����� SoundManager �ν��Ͻ��� �ϳ��� �����ϸ�, ���� �ٲ� �ı����� �ʴ� Ư���� ����.
 * 
 * Ȯ�强:
 * - BGM�� SFX�� �߰��� ������ AudioUnit�� Inspector�� �߰��� �ϸ� �ǹǷ� �ڵ� ���� ���� �߰� ����
 * - �߰� ����� �ɼ� �޴��� ���� ���� ����, ���̵� ��/�ƿ� ��� �� �߰� Ȯ���� �����ϵ��� ������.
 * 
 * ��� ���:
 * 1. SoundManager ������Ʈ�� �̵� �� Audio Units�� ����Ʈ�� �߰��ϰ� SoundName, Audio Source, Audio Clip ���� ����
 * 2. PlayBGM() �Ǵ� PlaySFX() �޼��带 ȣ���� ���带 �������
 * 3. �ʿ� �� StopBGM()���� Ư�� BGM�� ��������
 * ---------------------------------------------------------------
 */
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Enum"�� "Enumeration"�� ���ڷ�, Ư�� �ڷ����� ���� �̸� ���ǵ� ����� �����Ͽ� ����ϰ��� �� �� ����ϴ� ������ Ÿ��
/// <summary> ���� �� ���� ��� BGM/SFX ��� Enum Ÿ�� </summary>
public enum SoundName
{
    //�������
    BGM_Title,      //Ÿ��Ʋ �������
    BGM_MainMenu,   //���θ޴� �������
    BGM_StageBGM1,  //��������1 �������
    BGM_StageBGM2,  //��������2 �������

    //ȿ����
    SFX_GameOver,   //���� ���� ȿ����
    SFX_GameClear,  //���� Ŭ���� ȿ����
    SFX_Jump,       //���� ȿ����
    SFX_ButtonClick //��ư Ŭ�� ȿ����
}
//[System.Serializable] : C#�� �Ӽ�(attribute) / Ŭ����, ����ü, �ʵ忡 ���밡�� / ����ȭ�� ���� �߰���, �ش� ������ �߰������μ� Inspector�� ǥ���
//Inspector���� ���� ���� �ϱ����� ���� AudioUnit Ŭ���� ����

/// <summary> AudioSource�� AudioClip�� �ϳ��� �������� ���� Ŭ����</summary>
[System.Serializable]
public class AudioUnit
{
    /*
     * C#���� C++�� friend ������ ���⿡, ��ü������ ������ ä�� ȸ���ϴ� ����� ����Ѵ�.
     * Unity���� �����ϴ� ������ [SerializeField] private + public getter ����� ä����
     * ���� AudioUnit Ŭ������ �ʵ�� [SerializeField] private�� ����� �б� ���� public ������Ƽ�� ������
     */
    [SerializeField] private SoundName soundName = 0;         //����� ���� �̸�(Enum���� ����)
    [SerializeField] private AudioSource audioSource = null;  //�Ҹ��� ����ϴ� AudioSource
    [SerializeField] private AudioClip audioClip = null;      //����� ����� ������ AudioClip

    /* �б� ����(Read-Only) ������Ƽ(�ܺο��� �������� ������ �Ұ���)
     * ������Ƽ(Property)�� �Ӽ��̶� �ǹ�.
     * ������Ƽ�� ����ϰ� �Ǹ�, �Ӽ� ���� ��ȯ�ϰų� �� ���� �Ҵ��� �� �ִ�.
     */
    public SoundName SoundName { get { return soundName; } }
    public AudioSource AudioSource { get { return audioSource; } }
    public AudioClip AudioClip { get { return audioClip; } }
}

/// <summary> ���� �������� ���带 �����ϴ� �Ŵ��� Ŭ���� </summary>
public class SoundManager : MonoBehaviour
{
    [Header("Audio Units")] //Inspector���� ����Ʈ�� �����ϱ� ���� �Ӹ���("Audio Units") �߰�
    [SerializeField] private List<AudioUnit> UnitBGM = new List<AudioUnit>(); //BGM ���� AudioUnit ����Ʈ
    [SerializeField] private List<AudioUnit> UnitSFX = new List<AudioUnit>(); //SFX ���� AudioUnit ����Ʈ

    // singleton pattern: Ŭ���� �ϳ��� �ν��Ͻ��� �ϳ��� �����Ǵ� �����׷��� ����
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
            _instance = this; //this : ���� �ν��Ͻ��� ����Ű�� ���۷���
        }
        else if (_instance != this)
        {
            Debug.Log("SoundManager has another instance.");

            Destroy(gameObject); //���� �ν��Ͻ� �ı�(GameManger Object)
        }
        DontDestroyOnLoad(gameObject); //���� ����Ǿ ���� ���� ������Ʈ�� ������Ű�� �޼ҵ�
    }

    /*
     * foreach ���̶�?
     * ---------------------------------------------------------------
     * �÷���(List, Array ��)�� ����� ��� ��Ҹ� �ϳ��� ������ �ݺ� �����ϴ� �ݺ���
     * 
     * �⺻ ����:
     * foreach (var element in collection)
     * {
     *     // element�� ���� �ݺ������� ������ �۾�
     * }
     * 
     * �۵� ���:
     * - collection(����Ʈ, �迭 ��) �ȿ� �ִ� ��� ��Ҹ� ó������ ������ �ϳ��� �����ϴ�.
     * - ���� ��Ҹ� �ӽ� ����(var element)�� �����Ͽ� ��� �ȿ��� ����մϴ�.
     * - �÷����� ũ�⳪ �ε��� ������ �Ű澲�� �ʾƵ� �˴ϴ�.
     * 
     * ����:
     * - �ڵ尡 ���������ϴ�.
     * - �ε��� ����(��: IndexOutOfRange)�� ������ �� �ֽ��ϴ�.
     * - �б� ���� �۾�(���� �о����)�� �ſ� �����մϴ�.
     * ---------------------------------------------------------------
     * 
     * foreach���� ����ϸ� ����Ʈ ũ�⸸ŭ �ݺ������ϹǷ� �ڵ�ȭ�� ���� �����
     */

    /// <summary> �Ű������� ���޹��� ��������� ����ϴ� �޼ҵ� </summary>
    public void f_PlayBGM(SoundName soundName, float volume)
    {
        //���� ����� ������ŭ �ݺ��Ѵٸ� �ڵ�ȭ�� �˻����� 
        foreach(AudioUnit unit in UnitBGM) //var��� ��Ȯ���� ���� AudioUnit unit���·� ������
        {
            if(unit.SoundName == soundName) //�Ű����� ���带 ã����?
            {
                unit.AudioSource.clip = unit.AudioClip; //AudioSource�� Clip ����
                unit.AudioSource.volume = volume;       //�Ű������� ���� �������� ����
                unit.AudioSource.loop = true;           //�ݺ� ��� ����
                unit.AudioSource.Play();                //��� ����
                return; //����� ���۵Ǿ����Ƿ� foreach ���� ����
            }
        }
        Debug.LogWarning($"BGM '{soundName}' not found!"); //�Ű������� ���޹��� ��������� ���� ��� ��� ���
    }

    /// <summary> �Ű������� ���޹��� ȿ������ ����ϴ� �޼ҵ� </summary>
    public void f_PlaySFX(SoundName soundName, float volume)
    {
        foreach(AudioUnit unit in UnitSFX)
        {
            if(unit.SoundName == soundName)
            {
                unit.AudioSource.volume = volume;               //�Ű������� ���� �������� ����
                unit.AudioSource.PlayOneShot(unit.AudioClip);   //PlayOneShot �޼ҵ带 �̿��Ͽ� 1ȸ�� ȿ���� ���
                return; //����� ���۵Ǿ����Ƿ� foreach ���� ����
            }
        }
        Debug.LogWarning($"SFX '{soundName}' not found!"); //�Ű������� ���޹��� ȿ������ ���� ��� ��� ���
    }

    /// <summary> �Ű������� ���޹��� BGM�� ��� �����ϴ� �޼ҵ� </summary>
    public void f_StopBGM(SoundName soundName)
    {
        foreach(AudioUnit unit in UnitBGM)
        {
            if(unit.SoundName == soundName && unit.AudioSource.isPlaying) //������� ���� ��Ī == �Ű����� ���� ��Ī and ���尡 �����
            {
                unit.AudioSource.Stop(); //������� ������� ����
                return; //��� �����Ǿ����Ƿ� foreach ���� ����
            }
        }
    }

    /// <summary> ��� BGM�� ��� �����ϴ� �޼ҵ� </summary>
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
     * �������� public ������ ��ü ������ ö�а� ���� �ʱ⿡ get, set ����Ͽ� ����
     * set�� private set���� �����Ͽ� �б��������� ����
     * SoundManager Ŭ���� ������ set ��밡�� / �ܺ�Ŭ���� ���ٺҰ�
     */

    /* [Soure�� Clip �̿�ȭ ����]
     * 1. Source ���� Clip�� ����Ѵٸ�?
     * AudioClip �����͸� ��� �ְ� �ȴ�.
     * ����Ϸ��� ������ AudioSource�� �߰��ϰ� �����ؾ� �ϴ� ������ �߻�
     * ������ ��ȿ�����̰� ���������� ����� �ʷ��Ѵ�.
     * 2. Source + Clip ���� �����ϸ�?
     * Source�� �̹� ���õǾ� �ֱ� ������
     * �ڵ�� ���, ����, ���� ������ ��� �� �� �ִ�.
     */

    /*
    //[SerializeField]: private ���� ������ �����ϸ� �ν����Ϳ� ����Ǿ� Unity ������� ������ �� ����.
    [SerializeField] private AudioSource sourceGameStageBGM = null; //���� �������� ������� �ҽ�
    [SerializeField] private AudioSource sourceJumpSFX = null;      //���� ȿ���� �ҽ�

    [SerializeField] private AudioClip clipGameStageBGM = null; //���� �������� ������� Ŭ��
    [SerializeField] private AudioClip clipJumpSFX = null;      //���� ȿ���� Ŭ��          

    

    //������Ƽ(Property)�� �Ӽ��̶� �ǹ�.
    //������Ƽ�� ����ϰ� �Ǹ�, �Ӽ� ���� ��ȯ�ϰų� �� ���� �Ҵ��� �� �ִ�.

    /// <summary> ���� �������� ������� �ҽ� ������ </summary>
    public AudioSource SourceGameStageBGM
    {
        get { return sourceGameStageBGM; }
        private set { sourceGameStageBGM = value; }
    }

    /// <summary> ���� ȿ���� �ҽ� ������ </summary>
    public AudioSource SourceJumpSFX
    {
        get { return sourceJumpSFX; }
        private set { sourceJumpSFX = value; }
    }

    /// <summary> ���� �������� ������� Ŭ�� ������ </summary>
    public AudioClip ClipGameStageBGM
    {
        get { return clipGameStageBGM; }
        private set { clipGameStageBGM = value; }
    }

    /// <summary> ���� ȿ���� Ŭ�� ������ </summary>
    public AudioClip ClipJumpSFX
    {
        get { return clipJumpSFX; }
        private set { clipJumpSFX = value; }
    }


    // singleton pattern: Ŭ���� �ϳ��� �ν��Ͻ��� �ϳ��� �����Ǵ� �����׷��� ����
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
            _instance = this; //this : ���� �ν��Ͻ��� ����Ű�� ���۷���
        }
        else if (_instance != this)
        {
            Debug.Log("SoundManager has another instance.");

            Destroy(gameObject); //���� �ν��Ͻ� �ı�(GameManger Object)
        }
        DontDestroyOnLoad(gameObject); //���� ����Ǿ ���� ���� ������Ʈ�� ������Ű�� �޼ҵ�
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //f_PlayGameStageBGM(); //���� ������� ���
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> ���� ���������� ��������� �ݺ�����ϴ� �޼ҵ� </summary>
    public void f_PlayGameStageBGM()
    {
        if (SourceGameStageBGM != null && ClipGameStageBGM != null) //�ҽ��� Ŭ���� ��� �����ϴ� ���(not null)
        {
            SourceGameStageBGM.clip = ClipGameStageBGM;
            SourceGameStageBGM.loop = true; //�ݺ� ��� true��
            SourceGameStageBGM.volume = 0.1f; //���� 10%
            SourceGameStageBGM.Play(); //���
        }
    }

    /// <summary> ���� ���������� ������� ����� �ߴ��ϴ� �޼ҵ� </summary>
    public void f_StopGameStageBGM()
    {
        if (SourceGameStageBGM != null && SourceGameStageBGM.isPlaying)
        {
            SourceGameStageBGM.Stop();
        }
    }

    /// <summary> ���� ȿ������ ����ϴ� �޼ҵ� </summary>
    public void f_PlayJumpSFX()
    {
        if (SourceJumpSFX != null && ClipJumpSFX != null) //�ҽ��� Ŭ���� ��� �����ϴ� ���(not null)
        {
            SourceJumpSFX.volume = 0.1f; //���� 10%
            SourceJumpSFX.PlayOneShot(ClipJumpSFX); //PlayOneShot �޼ҵ带 �̿��Ͽ� �� ���� ���
        }
    }

    */
}
