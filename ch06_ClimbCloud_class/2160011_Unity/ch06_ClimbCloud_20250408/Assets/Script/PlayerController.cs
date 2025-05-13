using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene을 사용하는 데 필요하다
using UnityEngine.UI;// UI를 사용하므로 잊지 않고 추가한다 

public class PlayerController : MonoBehaviour
{
    int gScore = 0; //점수 변수 

    GameObject ScoreText; // 점수 표시용 텍스트 
    
    // Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버변수(m_)
    Rigidbody2D m_rigid2DCat = null;
    Animator m_animatorCat = null;
    // 플레이어에 가할 힘 값을 저장할 변수
    float jumpForce = 680.0f;


    // 플레이어 좌,우로 움직이는 가속도
    float fWalkForce = 30.0f;

    // 플레이어의 이동 속도가 지정한 최고 속도
    float fMaxWalkSpeed = 2.0f;


    // 플레이어 좌우 움직이는 속도
    float fPlayerMoveSpeed = 0.2f;

    // 멤버 변수 선언
    float fMaxPositionX = 2.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최대값 설정 변수 
    float fMinPositionX = -2.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최소값 설정 변수 
    float fPositionX = 0.0f;    //플레이어가 좌, 우 이동할 수 있는 X 좌표 저장 변수 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.m_rigid2DCat = GetComponent<Rigidbody2D>();
        this.m_animatorCat = GetComponent<Animator>();
        this.ScoreText = GameObject.Find("Score1");
    }

    public void AddScore(int value) //  점수 값 인자로 받음
    {
        gScore += value;
        TextMeshProUGUI scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score1: " + gScore.ToString("0");
    }

    public void SubScore(int value) //  점수 값 인자로 받음
    {
        gScore += value;
        TextMeshProUGUI scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score1: " + gScore.ToString("0");
    }
    // Update is called once per frame
    void Update()
        {
        // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1, 움직이지 않을 때 -> 0
        int nLeftRightKeyValue = 0;
        /*
         * [ AddForce 메서드 : 오브젝트에 일정한 힘을 주어 이동시키는 것 ]
         *  Spacebar Key가 눌리면(GetKeyDown 메서드) AddForce 메서드를 사용해 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
         *  즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
         */
        

        if (Input.GetKeyDown(KeyCode.RightShift) && m_rigid2DCat.linearVelocity.y == 0)
        {
            this.m_animatorCat.SetTrigger("JumpTrigger");
            this.m_rigid2DCat.AddForce(transform.up * this.jumpForce);
        }

        // 플레이어 좌,dn dlehd
        // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        //m_rigid2DCat.AddForce(transform.right * fWalkForce * nLefrRightKeyValue);
        /*
         *  플레이어의 스피드 제한
         *  프레임마다 AddForce 메서드를 사용해 힘을 가하면 플레이어가 계속 가속이 되는 문제점 발생
         *  그래서 플레이어의 이돌 속도가 지정한 최고속도(fMaxWalkSpeed)보다 빠르면 힘을 가하는 것을 멈추고 속도를 조절
         *  왼쪽화살표, 오른쪽화살표 key가 눌리면 AddForce 메서드를 사용해 좌, 우 방향으로 가도록 플레이어에 힘을 가한다.
         *  즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
         * Velocity : 같은 힘을 가해도 동일한 속도로 달릴 수 있도록 물리엔진이 자동으로 계산
         *  AddForce의 경우 순간적으로 튀어 오르고 점차 속도가 줄어들면서 떨어지는 점픙에 적합
         *  Velocity는 동일한 속도를 달리는 러너게임 캐릭터 이동에 적합
         */
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);
        if (fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        }

        /*
         * 애니메이션 재생 속도가 플레이어 이동 속도에 비례하도록 수정
         * 플레이어 이동 속도가 0이면 애니메이션 이동 속도도 0으로 정지하고
         * 플레이어 이동 속도가 빨라질수록 애니메이션 속도가 빨라짐
         * 애니메이션 재생 속도를 바꾸려면 컴포넌트의 speed 변수값으로 조정
         */
        // m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;

        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 2.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }


        // Score -100점이면 Gameover처리 

        if (gScore == -100 )
        {
            SceneManager.LoadScene("GameOverScene");
        }
        /*
         *  Mathf.Clamp(value, min, max) 메서드
         *  특정 값을 어떠한 범위에 제한시키고자 할 때 사용하는 메서드
         *  value 값의 범위 : min <= value <= max
         *  최소/최대값을 설정하여 지정한 범위 이외에 값이 되지 않도록 할 때 사용
         *  플레이어가 움직일 수 있는 최소(fMinPositionX)/최대(fMaxPositionX)범위값을 설정하여 그 범위를 벗어나지 않도록한다.
         */
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

    }
    
    
    // 골 도착 
    // 플레이어가 깃발에 닿으면 게임이 종료됨
    // 이 경우게임 씬에서 클리어 씬으로 전환되어야함
    // 플레이어가 깃발에 닿았는지는 OnTriggerEnter2D 메서드 감지함
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("골");
        SceneManager.LoadScene("ClearScene");
    }
}
