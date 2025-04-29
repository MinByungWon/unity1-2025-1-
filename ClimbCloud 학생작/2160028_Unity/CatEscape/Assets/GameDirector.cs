using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // UI ���� ���ӽ����̽��� �߰��ϴ� ����
using TMPro;
using UnityEngine.SceneManagement; //SceneManager ����� �ϱ� ���� �߰���.

public class GameDirector : MonoBehaviour
{
    /* HP Gauge Image Object�� ������ ��� ����
     * ���� ��ũ��Ʈ�� ����� HP �������� �����Ϸ��� ���� ��ũ��Ʈ�� HP �������� ��ü�� ������ �� �־�� ��
     * �׷��� ���ؼ� Object ������ �����ؼ� HP Gauge Image Object�� ����
     */
    GameObject gHpGauge = null;
    GameObject GameOver = null;             // GameOver �ؽ�Ʈ ������Ʈ
    public GameObject RetryButton;          // �ν����Ϳ��� �Ҵ� �� �� �ֵ��� ����
    public bool isHpGaugeEmpty = false;     // HP�� 0�� �Ǿ����� üũ�ϴ� ���� �߰�

    public TextMeshProUGUI scoreText;       // �ؽ�Ʈ UI ������Ʈ ����

    public TextMeshProUGUI timerText;  // Ui�� ǥ���� Ÿ�̸�
    public float countdownTime = 30f;  // Ÿ�̸� ���� �ð�

    private bool isTimeover = false;  // Ÿ�ӿ���
    private bool isTimeOver;


    public int Score                        // ������ ���� ������Ƽ �߰�
    { get; set; }                           // ������Ƽ : ���� + �Լ�



    //�̱��� �ν��Ͻ�
    private static GameDirector _instance = null; // static�� �ǹ� : class�� �ν��Ͻ� ������ ������� �� ������ �ѹ��� ����(�޸� �Ҵ�)
    public static GameDirector instance // ����(�ʵ�) + �Լ�(�޼ҵ�) : ������Ƽ(�ܼ� �� ���� ����ó�� ������ ���� ������ �޼ҵ带 ����)
    {
        // get �޼ҵ�
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // ���� GameObject�� �ν��Ͻ�
            initParam();
        }
        else if (_instance != this)
        {
            Destroy(gameObject); //���� �ν��Ͻ��� singleton�� �ν��Ͻ��� �ٸ��� �ı�
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* hpGauge ������Ʈ ã��
         * �� ������Ʈ ���ڿ� �����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־�� �Ѵ�.
         * �� �ȿ��� ������Ʈ�� ã�� �޼��� : Find
         * Find �޼���� ������Ʈ �̸��� �μ��� �����ϰ�,
         * �μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * Find �޼��带 ����� �� �߿��� HP �������� ������Ʈ�� ã�Ƽ� ������Ʈ ������ gHpGauge�� ����
         * 
         */
        gHpGauge = GameObject.Find("hpGauge");

        GameOver = GameObject.Find("GameOver"); //GameOver �ؽ�Ʈ ������Ʈ ã��

        RetryButton = GameObject.Find("RetryButton");       //Retry ��ư ������Ʈ ã��

        // GameOver �ؽ�Ʈ ó������ ������ �ʰ� ����
        if (GameOver != null)                //GameOver ������Ʈ�� �ִٸ�
        {
            GameOver.SetActive(false);      //������ ��Ȱ��ȭ
        }

        // Retry ��ư�� ó������ ������ �ʰ� ����
        if (RetryButton != null)
        {
            RetryButton.SetActive(false);
        }

        if (timerText != null)
        {
            timerText.text = FormatTime(countdownTime);
        }

    }



    /*  ���߿� ȭ�� ��Ʈ�ѷ����� HP������ ǥ�ø� ���̴� ó���� ȣ���� ���� �����
     *    HP �������� ó���� public �޼��带 �ۼ�
     *  ȭ���� ����� ȭ��� �÷��̾ �浹���� �� Image ������Ʈ�� fillAmount�� �ٿ�
     *    HP �������� ǥ���ϴ� ������ 10%�� ����
     */

    public void f_DecreaseHp()
    {
        /* ����Ƽ ������Ʈ�� GameObject��� �� ���ڿ� ���� �ڷ�(���۳�Ʈ)�� �߰��ؼ� ����� Ȯ����
         * ��) ������Ʈ�� ���������� �����̰� �Ϸ��� Rigidbody ���۳�Ʈ �߰�
         * ��) �Ҹ��� ���� �Ϸ��� AudioSource ���۳�Ʈ �߰�
         * ��) ��ü ����� �ø��� �ʹٸ� ��ũ��Ʈ�� �߰���
         * ���۳�Ʈ ���� ��� : GetComponent<>
         *  GetComponent�� ���� ������Ʈ�� ���� 'oo ������Ʈ �ּ���'��� ��Ź�ϸ�,
         *  �ش�Ǵ� ������Ʈ(���)�� �����ִ� �޼���
         * ��) Audio ���۳�Ʈ�� ���ϸ� -> GetComponent<AudioSource>()
         * ��) Text ���۳�Ʈ�� ���ϸ�->GetComponent<Text>()
         * ��) ���� ���� ��ũ��Ʈ�� ���۳�Ʈ�� �����̹Ƿ� GetComponent �޼��带 ����ؼ� ���� �� ����
         */

        // ȭ��� �÷��̾ �浹���� �� Image ������Ʈ(hpGauge)�� fillAmount�� �ٿ�
        //   HP �������� ǥ���ϴ� ������ 10%�� ����
        gHpGauge.GetComponent<Image>().fillAmount -= 0.1f;


        // HP �������� 0 �Ǹ� gHpGaugeEmpty�� true�� ����
        if (gHpGauge.GetComponent<Image>().fillAmount == 0f)
        {
            isHpGaugeEmpty = true;      // Ȱ��ȭ
            Time.timeScale = 0f;        // ���� ���߱� (0f�϶� ����)
        }
    }

    //retry ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void RetryGame()
    {
        Time.timeScale = 1f;  // ���� �ӵ� �ٽ� ����(1f�϶� ����ӵ�)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ���� �� �ٽ� �ε�

        // ������ ����۵� �� ������ �ʱ�ȭ
        initParam();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ������ ���۵� �� ������ 0���� �ʱ�ȭ�ϴ� �Լ�
    void initParam()
    {
        Score = 0; // Score 0���� ����
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score : {Score}";
        }
    }

    public void IncScore()  // ���� ������ ��� + �ؽ�Ʈ ����
    {
        Score += 1;  // ���� ����
        UpdateScoreText();
    }

    void Update()
    {
        // ü���� 0�̰�, GameOver ������Ʈ�� ������ ���
        if (isHpGaugeEmpty && GameOver != null)
        {
            GameOver.SetActive(true);           // GameOver Ui ���̰� �ϱ�
            RetryButton.SetActive(true);        // Retry ��ư�� ���̰� ����
        }

        //Ÿ�̸� �۵�
        if (!isHpGaugeEmpty && !isTimeOver)
        {
            countdownTime -= Time.unscaledDeltaTime;

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;                 // ī��Ʈ Ÿ�� 0���� ����    
                isTimeOver = true;                  // 
                Time.timeScale = 0f;                // ���� ���߱�
                GameOver.SetActive(true);           // GameOver Ui ���̰� �ϱ�
                RetryButton.SetActive(true);        // Retry ��ư�� ���̰� ����
            }

            if (timerText != null)                              //TimerText ������Ʈ�� ����� �ִٸ�
            {
                timerText.text = FormatTime(countdownTime);     //formatTime
            }
        }

    }
}