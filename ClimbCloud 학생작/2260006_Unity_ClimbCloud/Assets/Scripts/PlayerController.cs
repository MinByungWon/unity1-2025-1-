/*
 * 사용자가 입력한 대로 플레이거 좌우로 움직이거나 점프(뛰어오르는 기능)
 * 스페이스바를 누르면 점프하고, 키보드에 있는 좌우 화살표 키(←, →)를 누르면 이동하는 컨트롤러 스크립트를 작성
 * 1. 스페이스바를 누르면 점프
 * 2. 플레이어를 좌우로 움직이기
 */
using Unity.Hierarchy;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float fMaxPosition = 2.1f;  //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최대값 설정 변수
    float fMinPosition = -2.1f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최소값 설정 변수
    float fPositionX = 0.0f;    //플레이어의 위치 변수

    //Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버 변수(m_)
    Rigidbody2D m_rigid2DCat = null;

    float fJumpForce = 680.0f; //플레이어에 가할 힘 값을 저장할 변수
    float fWalkForce = 30.0f; //플레이어 좌, 우로 움직이는 가속도
    float fMaxWalkSpeed = 2.0f; //플레이어의 이동 속도가 지정한 최고 속도
    int nLeftRightKeyValue = 0; //플레이어 좌우 움직임 키 값 : 오른쪽 화살 키: 1, 왼쪽 화살 키: -1, 움직이지 않을 때: 0

    float fPlayerMoveSpeed = 0.0f; //플레이어 좌우 움직이는 속도

    //플레이어 이동 속도에 맞춰 애니메이션 재생 속도를 재생하기 위해 애니메이터 변수 선언
    Animator m_animatorCat = null;

    //강화 점프 기믹구현
    float fSpacebarPressTime = 0.0f;        //스페이스바를 누른 시간 변수
    float fMaxSpacebarPressTime = 1.0f;     //가산할 스페이스바 시간의 최대 시간 변수 (기본값 : 1.0f)
    [SerializeField] float fMinReinforceJumpForce = 500.0f;  //플레이어의 최소 점프 가속도
    [SerializeField] float fMaxReinforceJumpForce = 800.0f;  //플레이어의 최대 점프 가속도

    float fReinforceJumpForce = 0.0f;   //플레이어에 가할 힘 값을 저장하는 강화 점프 변수
    float fReinforceJumpRatio = 0.0f;   //플레이어가 사용할 수 있는 강화 점프의 강도 변수

    bool isSpacebarPress = false;   //스페이스바가 눌러져있는지 여부
    bool isPlayerOnCloud = false;   //플레이어가 구름위에 있는지 여부

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * 특정 오브젝트의 컴포넌트에 접근하기 위해서는 GetComponent 함수를 사용
         * Rigidbody2D 컴포넌트를 갖는 메소드를 사용하기 때문에 Start 메소드에서 GetComponent 메소드를 사용해서
         * Rigidbody2D 컴포넌트를 구해 멤버 변수에 저장
         */
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        //GetComponent 메소드를 사용해 Animator 컴포넌트를 구함
        m_animatorCat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //f_PlayerJump();           //플레이어가 'SpaceBar'를 누르면 점프하는 메소드
        f_PlayerReinforceJump();    //플레이어가 'SpaceBar'를 길게 누르면 강화 점프를 하는 메소드
        f_ReinforceJumpEffect();    //강화 점프를 위해 'SpaceBar'를 길게 누르는 동안 이펙트를 발생시키는 메소드
        f_PlayerMoveAxisX();        //플레이어를 좌, 우로 이동시키는 메소드
        f_PlayerMoveSpeedLimit();   //플레이어의 이동 속도를 제한하는 메소드

        f_SwitchPlayerDirection();  //플레이어가 바라보는 방향을 전환해주는 메소드
        f_SyncAnimationSpeed();     //플레이어의 속도에 따라 애니메이션 속도를 동기화시키는 메소드
        
        f_PlayerRangeLimit();       //플레이어가 화면밖으로 벗어나지 않게 하는 메소드
        f_PlayerFallingGround();    //플레이어가 땅으로 낙하하면 게임을 다시 시작하는 메소드
    }

    void f_PlayerMoveAxisX()
    {
        //nLeftRightKeyValue = 0;
        //움직이지 않을 경우, 전역변수인 nLeftRightKeyValue을 기본값인 0으로 다시 초기화
        //플레이어는 입력이 없을 경우 움직이지 않게 된다.

        /*
         * 플레이어 좌, 우 이동
         * 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키: 1, 왼쪽 화살 키: -1, 움직이지 않을 때: 0
         */
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }
        else
        {
            nLeftRightKeyValue = 0;
        }
    }

    void f_PlayerJump()
    {
        /*
         * AddForce 메소드 : 오브젝트에 일정한 힘을 주어 이동시키는 것
         * Spacebar Key가 눌리면 GetKeyDown 메소드 AddForce 메소드를 사용해 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
         * 즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메소드를 사용한다.
         */

        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");
            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }
    }

    void f_PlayerReinforceJump()
    {
        /*
         * [아이디어] : 스페이스바를 길게 누르면 더 높이 점프 (강화 점프)
         * [사용할 기법] : 선형 보간법
         * [정의] : 선형 보간법(線型補間法, linear interpolation)은 끝점의 값이 주어졌을 때 그 사이에 위치한 값을 추정하기 위하여 직선 거리에 따라 선형적으로 계산하는 방법이다.
         * [보간이란] : 알려진 값 사이에 없는 값을 추정하거나 끼워넣는 것을 의미한다.
         * Unity에서 제공하는 Mathf.Lerp 메소드는 보간(Interpolation) 기법 중 하나인 선형 보간(Linear Interpolation)을 수행한다.
         */

        /* 디버그 중 linearVelocity.y 조건이 모호하여 공중에서 점프할 수 있는 경우가 재현되어 삭제
   
        //플레이어의 y축 가속도가 0이면 플레이어가 구름 위에 있는 것으로 판단한다. 
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            isPlayerOnCloud = true;
        }*/

        //플레이어가 구름 위에 있고 스페이스바를 누를 경우, 누른 시간 초기화 및 가산을 위한 bool 변수 전환
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerOnCloud)
        {
            fSpacebarPressTime = 0.0f; //누른 시간 초기화

            isSpacebarPress = true; //스페이스바 참값

            Debug.Log("PressTime : " + fSpacebarPressTime);
        }

        //스페이스바를 누르고 있으면 누른 시간을 deltaTime 만큼 가산한다.
        if (Input.GetKey(KeyCode.Space) && isSpacebarPress)
        {
            fSpacebarPressTime += Time.deltaTime;

            //최대 스페이스바 시간을 넘지 못하도록 두 매개변수 중 최솟값 반환
            fSpacebarPressTime = Mathf.Min(fSpacebarPressTime, fMaxSpacebarPressTime);

            /*
             * Mathf.Clamp 메소드를 사용하여도 같은 결과를 유지하나, 상한값만 제한하면 되기때문에 Mathf.Min을 사용함
             * Mathf.Clamp(float a, float min, float max)
             * Mathf.Min(float a, float b)
             */
        }

        //스페이스바를 뗀다면 누른 시간에 비례하여 강화된 힘으로 점프한다.
        if (Input.GetKeyUp(KeyCode.Space) && isSpacebarPress && isPlayerOnCloud)
        {
            //강화 점프 비율 = 스페이스바 누른 시간 / 최대 스페이스바 시간
            fReinforceJumpRatio = fSpacebarPressTime / fMaxSpacebarPressTime;

            //강화 점프 가속도= 선형 보간(최소 힘, 최대 힘, 강화 점프 비율)
            fReinforceJumpForce = Mathf.Lerp(fMinReinforceJumpForce, fMaxReinforceJumpForce, fReinforceJumpRatio);

            m_animatorCat.SetTrigger("JumpTrigger"); //점프 애니메이션 활성화
            m_rigid2DCat.AddForce(transform.up * fReinforceJumpForce); //강화 점프 힘 만큼 up 방향으로 힘을 가함

            SoundManager.Instance.f_PlaySFX(SoundName.SFX_Jump, 0.1f); //점프 효과음 10% 볼륨으로 재생

            Debug.Log("Jump Ratio: " + fReinforceJumpRatio + ", Force: " + fReinforceJumpForce);
            isSpacebarPress = false; //스페이스바 거짓값
        }
    }

    void f_ReinforceJumpEffect()
    {
        //PingPong 메소드를 사용하여 1.0f초에 5번(Time.time * 5) 깜박이는 효과를 구현하기 위한 깜박임 주기 변수
        //이 메소드 내부에서만 사용되므로 지역변수 구현
        float fBlinkSpan = Mathf.PingPong(Time.time * 5, 1.0f);

        if(isSpacebarPress)
        {
            //흰색과 노란색이 깜박임 주기만큼 변환됨
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.yellow, fBlinkSpan);
        }
        else
        {
            //스페이스바를 누르지 않으면 흰색으로 변경
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void f_PlayerMoveSpeedLimit()
    {
        /*
         * 플레이어의 스피드 제한
         * 프레임마다 AddForce 메소드를 사용해 힘을 가하면 플레이어가 계속 가속이 되는 문제점 발생
         * 그래서 플레이어의 이동속도가 지정한 최고속도 보다 빠르면 힘을 가하는 것을 멈추고 속도를 조절
         * 왼쪽화살표, 오른쪽화살표 Key가 눌리면 AddForce 메소드를 사용해 좌, 우 방향으로 가도록 플레이어에 힘을 가한다.
         * 즉 , 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForve 메소드를 사용한다.
         * Velocity : 같은 힘을 가해도 동일한 속도로 달릴 수 있도록 물리엔진이 자동으로 계산
         * AddForce의 경우 순간적으로 튀어 오르고 점차 속도가 줄어들면서 떨어지는 점프에 적합
         * Velocity는 동일한 속도를 달리는 러너게임 캐릭터 이동에 적합
         */
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);

        if (fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        }
    }

    void f_SwitchPlayerDirection()
    {
        /*
         * 움직이는 방향에 따라 플레이어 이미지를 반전
         * 플레이어가 오른쪽으로 움직이면 스트라이트도 오른쪽으로 향하고,
         * 왼쪽으로 움직이면 왼쪽으로 움직이도록 이미지를 반전시키려면,
         * 스프라이트의 X축 방향배율을 -1배로 함
         * 스프라이트의 배율을 바꾸려면 tranform 컴포넌트의 localScale 변수 값을 변경
         * 오른쪽 화살표는 1배, 왼쪽화살표는 X축 방향으로 -1배
         */
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1.0f, 1.0f);
        }
    }

    void f_SyncAnimationSpeed()
    {
        /*
         * 애니메이션 재생 속도가 플레이어 이동 속도에 비례하도록 수정
         * 플레이어 이동 속도가 0이면 애니메이션 이동 속도도 0으로 정지하고,
         * 플레이어 이동 속도가 빨라질수록 애니메이션 속도가 빨라짐
         * 애니메이션 재생 속도를 바꾸려면 컴포넌트의 speed 변수값으로 조정
         */
        //m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;

        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 2.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }
    }

    void f_PlayerRangeLimit()
    {
        //Clamp 메소드를 사용하여 x좌표값을 지정한 범위내 값 고정
        fPositionX = Mathf.Clamp(transform.position.x, fMinPosition, fMaxPosition);

        //고정된 값 내에서 변경
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
    }

    void f_PlayerFallingGround()
    {
        //-4.5f(최하단) 이하로 플레이어가 낙하시 플레이어 오브젝트 파괴 및 씬 다시 불러오기
        if(transform.position.y < -4.5f)
        {
            Destroy(gameObject);
            SoundManager.Instance.f_PlaySFX(SoundName.SFX_GameOver, 0.5f); //게임 오버 효과음 10% 볼륨으로 재생

            //SceneManager.LoadScene("GameScene");
            GameManager.Instance.f_RestartGame(); //게임 재시작
        }
    }

    private void f_ReturnToTitle()
    {
        SoundManager.Instance.f_PlayBGM(SoundName.BGM_Title, 0.1f);
        GameManager.Instance.f_OpenTitle(); //임시
    }

    /*
     * 플레이어가 깃발에 닿으면 게임이 종료됨
     * 이 경우 게임씬에서 클리어 씬으로 전환되어야 함
     * 플레이어가 깃발에 닿았는지는 OnTriggerEnter2D 메소드로 감지함
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //개선점 : 클리어 부분에서 BGM, SFX를 따로 재생 / 효과음을 재생하고 이동함
        if (collision.gameObject.CompareTag("Flag")) //깃발 태그 감지
        {
            Debug.Log("클리어!");
            SoundManager.Instance.f_StopAllBGM();
            SoundManager.Instance.f_PlaySFX(SoundName.SFX_GameClear, 0.5f); //클리어 효과음 재생
            Invoke("ReturnToTitle", 2.0f); //효과음 재생 후 2초 뒤 타이틀로 이동
        }

        /*
        Debug.Log("클리어!");
        //SoundManager.Instance.f_PlaySFX(SoundName.SFX_GameClear, 0.1f); //게임 클리어 효과음 10% 볼륨으로 재생

        //SceneManager.LoadScene("ClearScene");
        //GameManager.Instance.f_OpenClearGame();
        SoundManager.Instance.f_StopAllBGM();
        SoundManager.Instance.f_PlayBGM(SoundName.BGM_Title, 0.1f);
        GameManager.Instance.f_OpenTitle(); //임시
        */
    }



    /*
     * Cloud Tag를 인식하여 isPlayerOnCloud를 참, 거짓 값을 변동하도록 해보았으나, 간헐적으로 충돌을 감지하지 못하는 상황이 발생
     * 해당 조건이 발현될 경우, 플레이어는 구름 위에 있음에도 점프를 다시 하지 못하는 상황이 발생한다.
     * player 오브젝트 내부 Rigidbody2D 컴포넌트의 Collision Detection이 Discrete로 설정되어 있어 터널링 현상이 발생하는 것을 확인함.
     * 따라서 해당 설정값을 Discrete → Continuous로 변경하여 충돌 판정을 강화함.
     * 허나 Continuous를 사용하므로 퍼포먼스 비용이 증가(시스템 자원을 더 소모)하는 문제점을 내포함.
     * Raycast 방식을 사용한다면 보완 가능할 것으로 생각됨.
     */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어의 착지여부 감지
        //Debug.Log("충돌한 물체: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Cloud"))
        {
            //Debug.Log("플레이어가 착지함");
            isPlayerOnCloud = true; //플레이어 착지 참

            /*
             * 플레이어가 구름에 충돌한 경우 충돌한 오브젝트에 FallingCloud 스크립트가 존재하는가?
             * 아래와 같이 작성한 이유는 GetComponent 메소드의 경우 아무것도 없을 경우 반환을 null값으로 리턴함
             * 따라서 if문에서 null값인지 검사하는 절차로 떨어지는 구름과 고정된 구름을 구분할 수 있게됨.
             */
            FallingCloud g_fallingCloud = collision.gameObject.GetComponent<FallingCloud>();

            //컴포넌트가 존재한다면
            if (g_fallingCloud != null)
            {
                g_fallingCloud.f_ActiveFallingCloud(); //FallingCloud의 f_ActiveFallingCloud 메소드(떨어지는 구름 기능) 호출
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //플레이어의 점프여부 감지
        if (collision.gameObject.CompareTag("Cloud"))
        {
            //Debug.Log("플레이어가 점프함");
            isPlayerOnCloud = false; //플레이어 착지 거짓
        }
    }
}
