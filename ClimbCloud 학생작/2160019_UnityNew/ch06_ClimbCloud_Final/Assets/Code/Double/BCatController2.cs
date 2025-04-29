using UnityEngine;
using UnityEngine.SceneManagement;

public class BCatController : MonoBehaviour
{
    // Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버변수(m_)
    Rigidbody2D m_rigid2DCat = null;

    Animator m_animator = null;

    GameObject m_Bcat = null;

    //Restart지점 및 체크 포인트 지정할 변수
    Vector2 currentCheckPoint = new Vector3(0, 0);

    // 플레이어에 가할 힘 값을 저장할 변수
    float fJumpForce = 680.0f;

    // 플레이어에 가할 넉백 값을 저장할 변수
    float fKnockBack = 5.0f;

    // 플레이어 좌,우로 움직이는 가속도
    float fwalkForce = 30.0f;

    // 플레이어의 이동 속도가 지정한 최고 속도
    float fmaxWalkSpeed = 2.0f; 
    float fthreshold = 0.2f;

    // 멤버 변수 선언
    float fMaxPositionX = 2.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최대값 설정 변수 
    float fMinPositionX = -2.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최소값 설정 변수 
    float fPositionX = 0.0f;    //플레이어가 좌, 우 이동할 수 있는 X 좌표 저장 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;    
        m_rigid2DCat = GetComponent<Rigidbody2D>();
        this.m_animator = GetComponent<Animator>();

        this.m_Bcat = GameObject.Find("Bcat");
    }

    // Update is called once per frame
    void Update()
    {
        /* [ AddForce 메서드 : 오브젝트에 일정한 힘을 주어 이동시키는 것 ]
         * Spacebar Key가 눌리면(GetKeyDown 메서드) AddForce 메서드를 사용해 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
         * 즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
         */

        //체크포인트로 이동
        BCheckPoint();

        // 점프한다.
        if (Input.GetKeyDown(KeyCode.UpArrow) && m_rigid2DCat.linearVelocity.y == 0)
        {
            this.m_animator.SetTrigger("JumpTrigger");
            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }



        // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1, 움직이지 않을 때 -> 0
        int nLeftRightKeyValue = 0;

        // 플레이어 좌,우 이동
        // 플레이어 좌우 움직임 키 값 : D -> 1, A -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }

        if(Input.acceleration.x > this.fthreshold)
        {
            nLeftRightKeyValue = 1;
        }
        if(Input.acceleration.x < -this.fthreshold)
        {
            nLeftRightKeyValue = -1;
        }

        // 플레이어의 x값이 최대와 최소를 넘지않게 만듬.
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, 0);

        
        // 플레이어 좌우 움직이는 속도
        float fPlayerMoveSpeed = Mathf.Abs(this.m_rigid2DCat.linearVelocity.x);

        if (fPlayerMoveSpeed < fmaxWalkSpeed) { 
            this.m_rigid2DCat.AddForce(transform.right * nLeftRightKeyValue * this.fwalkForce);
        }

        // 움직이는 방향에 따라 반전한다.
        if(nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        /*
         * 애니메이션 재생 속도가 플레이어 이동 속도에 비례하도록 수정
         * 플레이어 이동 속도가 빨라질수록 애니메이션 속도가 빨라짐
         * 애니메이션 재생 속도를 바꾸려면 컴퍼넌트의 speed 변수값으로 조정
         */

        this.m_animator.speed = fPlayerMoveSpeed / 1.0f;

       // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            this.m_animator.speed = fPlayerMoveSpeed / 2.0f;
        }
        else
        {
            this.m_animator.speed = 1.0f;
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene2");
        }
    }

    // 골 도착
    void OnTriggerEnter2D(Collider2D other)
    {
        // tag가 1인 깃발에 도달
        if (other.tag == "flag1")
        {
            // 현재 체크포인트 위치가 1, 18, 0으로 변환
            currentCheckPoint = new Vector2(1, 18);
        }
        // tag가 2인 깃발에 도달
        else if (other.tag == "flag2")
        {
            currentCheckPoint = new Vector2(0, 0);
            Debug.Log("골");
            SceneManager.LoadScene("ClearScene");
        }
    }

    void BCheckPoint()
    {
        // R을 누를시 cat의 위치가 체크포인트로 이동
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_Bcat.transform.position = currentCheckPoint;
            Debug.Log("이동");
        }
    }

    public void BNnockBack()
    {
        // AddForce로 물리적 힘을 가하고 ForceMode2D안에 있는 Impulse로 즉발적인 반응을 만듬.
        m_rigid2DCat.AddForce(-transform.localScale * this.fKnockBack, ForceMode2D.Impulse);
    }
}
