/*
 * < 사용자가 입력한 대로 플레이어가 좌우로 움직이거나 점프(뛰어오르는) 기능 >
 * 스페이스바를 누르면 점프하고, 키보드에 있는 좌우 화살표 키(<-, ->)를 누르면 이동하는 컨트롤러 스크립트를 작성
 * 1. 스페이스바를 누르면 점프
 * 2. 플레이어를 좌우로 움직이기
 */


using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버변수(m_)
    Rigidbody2D m_rigid2DCat = null;

    // 플레이어에 가할 힘 값을 저장할 변수
    float fJumpForce = 680.0f;

    // 플레이어 좌, 우로 움직이는 가속도
    float fWalkForce = 30.0f;

    // 플레이어의 이동 속도가 지정한 최고 속도
    float fMaxWalkSpeed = 2.0f;

    // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1, 움직이지 않을 때 -> 0
    int nLeftRightKeyValue = 0;

    // 플레이어 좌우 움직이는 속도
    float fPlayerMoveSpeed = 0.0f;

    // 플레이어 이동 속도에 맞춰 애니메이션 재생 속도를 재생하기 위해 애니메이터 변수 선언
    Animator m_animatorCat = null;

    //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최댓값 설정 변수
    float fMaxPositionX = 2.0f; // 플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최댓값 설정 변수
    float fminPositionX = -2.0f; // 플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최솟값 설정 변수
    float fPositionX = 0.0f;    // 플레이어가 좌, 우 이동할 수 있는 X 좌표 저장 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * 디바이스 성능에 따른 실행 결과의 차이 없애기
         * 어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리
         * 스마트폰은 60, 고속의 PC는 300이 될 수 있는 디바이스 성능에 따라 게임 동작에 영향을 미칠 수 있음
         * 프레임레이트를 60으로 고정
         */
        Application.targetFrameRate = 60;

        /*
         * 특정 오브젝트의 컴포넌트에 접근하기 위해서는 GetComponent 함수를 사용
         * Rigidbody2D 컴포넌트를 갖는 메서드를 사용하기 때문에 Start 메서드에서 GetComponent 메서드를 사용해서
         * Rigidbody2D 컴포넌트를 구해 멤버 변수 저장
         */
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        //GetComponent 메서드를 사용해 Animator 컴포넌트를 구함
        m_animatorCat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * [AddForce 메서드 : 오브젝트에 일정한 힘을 주어 이동시키는 것]
         * Spacebar Key가 눌리면(GetKeyDown 메서드) AddForce 메서드를 사용해 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
         * 즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
         */
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");
            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }

        //키 누르지 않았을 때 정지
        nLeftRightKeyValue = 0;

        // 플레이어 좌, 우 이동
        // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 ->, 왼쪽 화살 키 -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }

        /*
         * 플레이어의 스피드 제한
         *  프레임마다 AddForce 메서드를 사용해 힘을 가하면 플레이어가 계속 가속이 되는 문제점 발생
         *  그래서 플레이어의 이동 속도가 지정한 최고속도(fMaxWalkSpeed) 보다 빠르면 힘을 가하는 것을 멈추고 속도를 조절
         *  왼쪽화살표, 오른쪽화살표 Key가 눌리면 AddForce 메서드를 사용해 좌, 우 방향으로 가도록 플레이어에 힘을 가한다.
         *  즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
         * Velocity : 같은 힘을 가해도 동일한 속도로 달릴 수 있도록 물리엔진이 자동으로 계산
         *  AddForce의 경우 순간적으로 튀어 오르고 점차 속도가 줄어들면서 떨어지는 점프에 적합
         *  Velocity는 동일한 속도를 달리는 러너게임 캐릭터 이동에 적합
         */
        //m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);
        if(fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        }

        /*
         * 움직이는 방향에 따라 플레이어 이미지를 반전
         * 플레이어가 오른쪽으로 움직이면 스프라이트도 오른쪽으로 향하고,
         * 왼쪽으로 움직이면 왼쪽으로 움직이도록 이미지를 반전시키려면,
         * 스프라이트의 x축 방향배율을 -1배로 함
         * 스프라이트의 배율을 바꾸려면 tranform 컴포넌트의 localScale 변수 값을 변경
         * 오른쪽화살표는 1배, 왼쪽화살표는 x축 방향으로 -1배
         */
        if(nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        //플레이어가 화면 밖으로 나갔다면 처음부터
        if(transform.position.y < -10)
        {
            // 과일 개수를 초기화
            Fruit.ResetFruitCount();  // 과일 개수 초기화
            SceneManager.LoadScene("GameScene");
        }

        /*
         * 애니메이션 재생속도가 플레이어 이동 속도에 비례하도록 수정
         *  플레이어 이동속도가 0이면 애니메이션 이동속도도 0으로 정지하고,
         *  플레이어 이동속도가 빨라질수록 애니메이션 속도가 빨라짐
         *  애니메이션 재생속도를 바꾸려면 컴포넌트의 speed 변수값으로 조정
         */
        //m_animatorCat.speed = fPlayerMoveSpeed / 1.0f ;

        if(m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 2.0f;
        }
        else 
        {
            m_animatorCat.speed = 1.0f;
        }

        //최소,최대값을 설정하여 지정한 범위 이외의 값이 되지 않도록 할 때 사용함
        //플레이어가 움직일 수 있는 최소(fMinPositionX)/최대(fMaxPositionX) 범위값을 설정하여 그 범위를 벗어나지 않도록 함.
        fPositionX = Mathf.Clamp(transform.position.x, fminPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

    }


    // 플레이어가 깃발에 닿으면 게임이 종료됨
    // 이 경우 게임 씬에서 클리어 씬으로 전환되어야 함
    //. 플레이어가 깃발에 닿았는지는 OnTriggerEnter2D 메서드로 감지함
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))                       //Goal(깃발) 태그가 닿았을 때
        {
            if (Fruit.fruitCount <= 0)                          //과일의 개수가 0이하이면,
            {
                SceneManager.LoadScene("ClearScene");           //ClearScene 출력
            }
        }
    }


    
}
