using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    //게임 디렉터 스크립트를 싱글톤으로 구현
    private static GameDirector instance;
    PlayerController player = null;
    ArrowJenerator arrowJenerator = null;
    ItemJenerator itemJenerator = null;
    //필요한 게임 오브젝트를 가져오기 위한 변수들 초기화


    public float maxGameTime = 60.0f;               //생존 시간
    public float gameTime = 0f;                     //시간 경과
    public bool _isDead = false;                    //캐릭터의 체력이 다 닳았는지에 대한 여부

    public int GameLevel = 1;                       //게임 레벨 스케일링 인수

    
    GameObject hpGauge = null;              
    GameObject loseScreen = null;   
    GameObject winScreen = null;
    //필요한 게임 오브젝트를 가져오기 위한 변수들 초기화



    public static GameDirector Instance
    {
        get
        {
            if (instance == null) instance = new GameDirector();
            return instance;
        }
    }//싱글톤 구현


    /// <summary>
    /// 게임 오브젝트 값 지정
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        } 
        player = GameObject.Find("PlayerParent").transform.GetChild(0).GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController not found");
        }
        arrowJenerator = GameObject.Find("ArrowJenerator").GetComponent<ArrowJenerator>();
        if (arrowJenerator == null)
        {
            Debug.LogError("ArrowJenerator not found");
        }
        itemJenerator = GameObject.Find("ItemJenerator").GetComponent<ItemJenerator>();
        if (itemJenerator == null)
        {
            Debug.LogError("ItemJenerator not found");
        }
        //활성화 되어있는 게임 오브젝트를 GameObject.Find로 가져옴



        Transform canvasTransform = GameObject.Find("Canvas").transform;
        hpGauge = canvasTransform.Find("hpGauge")?.gameObject;
        if (hpGauge == null)
        {
            Debug.LogError("hpGauge not found.");
        }

        loseScreen = canvasTransform.Find("GameLose")?.gameObject;
        if (loseScreen == null)
        {
            Debug.LogError("GameLose not found.");
        }

        winScreen = canvasTransform.Find("GameWin")?.gameObject;
        if (winScreen == null)
        {
            Debug.LogError("GameWin not found.");
        }
        //비활성화되어있는 게임 오브젝트는 FInd가 되지 않으므로 활성화 되어있는 부모 오브젝트를 먼저 가져오고,
        //그 뒤에 비활성화 되어있는 자식 오브젝트를 가져옴
    }


    

    /// <summary>
    /// 게임이 시작될 때 호출되는 함수 :
    /// </summary>
    public void GameStart()
    {
        GameLevel = 1;
        _isDead = false;
        gameTime = 0f;
        Time.timeScale = 1;
        player.gameObject.SetActive(true);
        arrowJenerator.StartArrowJen();
        itemJenerator.StartItemJen();
        //화살과 아이템 생성, 플레이어 오브젝트 활성화, 게임 시간 초기화 등등 값 초기화 과정
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        //FixedUpdate()로 대체 가능함
    }

    // Update is called once per frame
    void Update()
    {

        GameLevel = (int)gameTime / 10;
        if (!_isDead)
        {
            gameTime += Time.deltaTime;                 //생존해있는 시간만큼 현재시간을 더한다. == 타이머 역할 
            if (gameTime >= maxGameTime)                //정해둔 시간보다 더 커지면 == 지정된 시간동안 생존하면
            {
                GameWin();                              //게임 승리
            }
        }
    }
    //업데이트 함수 자체에는 타이머 기능과 승리 여부만 구현함





    public void DecreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
        if (this.hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            GameOver();
        }
    }
    //화살이 캐릭터에 충돌할 시 체력을 감소하는 메서드


    public void IncreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount += 0.3f;
    }
    //아이템이 캐릭터에 충돌할 시 체력을 증가하는 메서드

    void GameWin()
    {
        _isDead = true;
        winScreen.SetActive(true);
        Time.timeScale = 0; // 게임 승리 시 시간 스케일을 0으로 설정하여 게임을 멈춤
        
    }
    //게임을 승리했을때 호출되는 메서드


    void GameOver()
    {
        
        _isDead = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    //체력을 다 닳아 게임을 진행할 수 없을 때 호출되는 메서드

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    //게임 재시작 메서드

}
