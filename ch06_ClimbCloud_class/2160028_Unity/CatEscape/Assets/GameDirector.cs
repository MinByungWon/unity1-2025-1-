using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // UI 관련 네임스페이스를 추가하는 역할
using TMPro;
using UnityEngine.SceneManagement; //SceneManager 사용을 하기 위해 추가함.

public class GameDirector : MonoBehaviour
{
    /* HP Gauge Image Object를 저장할 멤버 변수
     * 감독 스크립트를 사용해 HP 게이지를 갱신하려면 감독 스크립트가 HP 게이지의 실체를 조작할 수 있어야 함
     * 그러기 위해서 Object 변수를 선언해서 HP Gauge Image Object를 저장
     */
    GameObject gHpGauge = null;
    GameObject GameOver = null;             // GameOver 텍스트 오브젝트
    public GameObject RetryButton;          // 인스펙터에서 할당 할 수 있도록 변경
    public bool isHpGaugeEmpty = false;     // HP가 0이 되었는지 체크하는 변수 추가

    public TextMeshProUGUI scoreText;       // 텍스트 UI 오브젝트 연결

    public TextMeshProUGUI timerText;  // Ui에 표시할 타이머
    public float countdownTime = 30f;  // 타이머 시작 시간

    private bool isTimeover = false;  // 타임오버
    private bool isTimeOver;


    public int Score                        // 점수를 위한 프로퍼티 추가
    { get; set; }                           // 프로퍼티 : 변수 + 함수



    //싱글톤 인스턴스
    private static GameDirector _instance = null; // static의 의미 : class의 인스턴스 생성과 관계없이 이 변수는 한번만 생성(메모리 할당)
    public static GameDirector instance // 변수(필드) + 함수(메소드) : 프로퍼티(외수 쓸 때는 변수처럼 쓰지만 내부 구성은 메소드를 가짐)
    {
        // get 메소드
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // 현재 GameObject의 인스턴스
            initParam();
        }
        else if (_instance != this)
        {
            Destroy(gameObject); //현재 인스턴스와 singleton의 인스턴스가 다르면 파괴
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* hpGauge 오브젝트 찾기
         * 각 오브젝트 상자에 대응하는 오브젝트를 씬 안에서 찾아 넣어야 한다.
         * 씬 안에서 오브젝트를 찾는 메서드 : Find
         * Find 메서드는 오브젝트 이름을 인수로 전달하고,
         * 인수 이름이 씬에 존재하면 해당 오브젝트를 반환
         * Find 메서드를 사용해 씬 중에서 HP 게이지의 오브젝트를 찾아서 오브젝트 변수인 gHpGauge에 저장
         * 
         */
        gHpGauge = GameObject.Find("hpGauge");

        GameOver = GameObject.Find("GameOver"); //GameOver 텍스트 오브젝트 찾기

        RetryButton = GameObject.Find("RetryButton");       //Retry 버튼 오브젝트 찾기

        // GameOver 텍스트 처음에는 보이지 않게 설정
        if (GameOver != null)                //GameOver 오브젝트가 있다면
        {
            GameOver.SetActive(false);      //있으면 비활성화
        }

        // Retry 버튼도 처음에는 보이지 않게 설정
        if (RetryButton != null)
        {
            RetryButton.SetActive(false);
        }

        if (timerText != null)
        {
            timerText.text = FormatTime(countdownTime);
        }

    }



    /*  나중에 화살 컨트롤러에서 HP게이지 표시를 줄이는 처리를 호출할 것을 고려해
     *    HP 게이지의 처리는 public 메서드를 작성
     *  화살의 기능은 화살과 플레이어가 충돌했을 때 Image 오브젝트의 fillAmount를 줄여
     *    HP 게이지를 표시하는 비율을 10%씩 낮춤
     */

    public void f_DecreaseHp()
    {
        /* 유니티 오브젝트는 GameObject라는 빈 상자에 설정 자료(컴퍼넌트)를 추가해서 기능을 확장함
         * 예) 오브젝트를 물리적으로 움직이게 하려면 Rigidbody 컴퍼넌트 추가
         * 예) 소리를 내게 하려면 AudioSource 컴퍼넌트 추가
         * 예) 자체 기능을 늘리고 싶다면 스크립트를 추가함
         * 컴퍼넌트 접근 방법 : GetComponent<>
         *  GetComponent는 게임 오브젝트에 대해 'oo 컴포넌트 주세요'라고 부탁하면,
         *  해당되는 컴포넌트(기능)을 돌려주는 메서드
         * 예) Audio 컴퍼넌트를 원하면 -> GetComponent<AudioSource>()
         * 예) Text 컴퍼넌트를 원하면->GetComponent<Text>()
         * 예) 직접 만든 스크립트도 컴퍼넌트의 일종이므로 GetComponent 메서드를 사용해서 구할 수 있음
         */

        // 화살과 플레이어가 충돌했을 때 Image 오브젝트(hpGauge)의 fillAmount를 줄여
        //   HP 게이지를 표시하는 비율을 10%씩 낮춤
        gHpGauge.GetComponent<Image>().fillAmount -= 0.1f;


        // HP 게이지가 0 되면 gHpGaugeEmpty를 true로 설정
        if (gHpGauge.GetComponent<Image>().fillAmount == 0f)
        {
            isHpGaugeEmpty = true;      // 활성화
            Time.timeScale = 0f;        // 게임 멈추기 (0f일때 멈춤)
        }
    }

    //retry 버튼을 눌렀을 때 호출되는 함수
    public void RetryGame()
    {
        Time.timeScale = 1f;  // 게임 속도 다시 설정(1f일때 정상속도)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 현재 씬 다시 로드

        // 게임이 재시작될 때 점수를 초기화
        initParam();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 게임이 시작될 때 점수를 0으로 초기화하는 함수
    void initParam()
    {
        Score = 0; // Score 0으로 설정
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score : {Score}";
        }
    }

    public void IncScore()  // 점수 오르는 기능 + 텍스트 갱신
    {
        Score += 1;  // 점수 증가
        UpdateScoreText();
    }

    void Update()
    {
        // 체력이 0이고, GameOver 오브젝트가 존재할 경우
        if (isHpGaugeEmpty && GameOver != null)
        {
            GameOver.SetActive(true);           // GameOver Ui 보이게 하기
            RetryButton.SetActive(true);        // Retry 버튼도 보이게 설정
        }

        //타이머 작동
        if (!isHpGaugeEmpty && !isTimeOver)
        {
            countdownTime -= Time.unscaledDeltaTime;

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;                 // 카운트 타임 0으로 고정    
                isTimeOver = true;                  // 
                Time.timeScale = 0f;                // 게임 멈추기
                GameOver.SetActive(true);           // GameOver Ui 보이게 하기
                RetryButton.SetActive(true);        // Retry 버튼도 보이게 설정
            }

            if (timerText != null)                              //TimerText 오브젝트가 연결돼 있다면
            {
                timerText.text = FormatTime(countdownTime);     //formatTime
            }
        }

    }
}