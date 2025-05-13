using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    //���� ���� ��ũ��Ʈ�� �̱������� ����
    private static GameDirector instance;
    PlayerController player = null;
    ArrowJenerator arrowJenerator = null;
    ItemJenerator itemJenerator = null;
    //�ʿ��� ���� ������Ʈ�� �������� ���� ������ �ʱ�ȭ


    public float maxGameTime = 60.0f;               //���� �ð�
    public float gameTime = 0f;                     //�ð� ���
    public bool _isDead = false;                    //ĳ������ ü���� �� ��Ҵ����� ���� ����

    public int GameLevel = 1;                       //���� ���� �����ϸ� �μ�

    
    GameObject hpGauge = null;              
    GameObject loseScreen = null;   
    GameObject winScreen = null;
    //�ʿ��� ���� ������Ʈ�� �������� ���� ������ �ʱ�ȭ



    public static GameDirector Instance
    {
        get
        {
            if (instance == null) instance = new GameDirector();
            return instance;
        }
    }//�̱��� ����


    /// <summary>
    /// ���� ������Ʈ �� ����
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
        //Ȱ��ȭ �Ǿ��ִ� ���� ������Ʈ�� GameObject.Find�� ������



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
        //��Ȱ��ȭ�Ǿ��ִ� ���� ������Ʈ�� FInd�� ���� �����Ƿ� Ȱ��ȭ �Ǿ��ִ� �θ� ������Ʈ�� ���� ��������,
        //�� �ڿ� ��Ȱ��ȭ �Ǿ��ִ� �ڽ� ������Ʈ�� ������
    }


    

    /// <summary>
    /// ������ ���۵� �� ȣ��Ǵ� �Լ� :
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
        //ȭ��� ������ ����, �÷��̾� ������Ʈ Ȱ��ȭ, ���� �ð� �ʱ�ȭ ��� �� �ʱ�ȭ ����
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        //FixedUpdate()�� ��ü ������
    }

    // Update is called once per frame
    void Update()
    {

        GameLevel = (int)gameTime / 10;
        if (!_isDead)
        {
            gameTime += Time.deltaTime;                 //�������ִ� �ð���ŭ ����ð��� ���Ѵ�. == Ÿ�̸� ���� 
            if (gameTime >= maxGameTime)                //���ص� �ð����� �� Ŀ���� == ������ �ð����� �����ϸ�
            {
                GameWin();                              //���� �¸�
            }
        }
    }
    //������Ʈ �Լ� ��ü���� Ÿ�̸� ��ɰ� �¸� ���θ� ������





    public void DecreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
        if (this.hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            GameOver();
        }
    }
    //ȭ���� ĳ���Ϳ� �浹�� �� ü���� �����ϴ� �޼���


    public void IncreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount += 0.3f;
    }
    //�������� ĳ���Ϳ� �浹�� �� ü���� �����ϴ� �޼���

    void GameWin()
    {
        _isDead = true;
        winScreen.SetActive(true);
        Time.timeScale = 0; // ���� �¸� �� �ð� �������� 0���� �����Ͽ� ������ ����
        
    }
    //������ �¸������� ȣ��Ǵ� �޼���


    void GameOver()
    {
        
        _isDead = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    //ü���� �� ��� ������ ������ �� ���� �� ȣ��Ǵ� �޼���

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    //���� ����� �޼���

}
