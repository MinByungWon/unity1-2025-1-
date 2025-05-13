/*
 * <사용자가 입력한 대로 플레이어가 좌우로 움직이거나 점프 기능>
 * 키보드에 있는 좌우 화살표 키를 누르면 이동하는 컨트롤러 스크립트 작성
 * 1. 스페이스바 누르면 점프
 * 2. 플레이어 좌우로 움직이기
 */
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬을 전환하기 위해서 씬 매니저를 사용하기 위한 네임스페이스
using UnityEngine.UI;
using TMPro; // TextMeshPro사용하기 때문에 선언
public class PlayerController : MonoBehaviour
{
    // 아이템 오브젝트 
    private GameObject emeraldItem = null;
    private GameObject cherryItem = null;
    private GameObject bombItem = null;
    private GameObject cherryItem1 = null;
    private GameObject emeraldItem1 = null;
    private GameObject emeraldItem2 = null;

    public static int n_Score = 0; // 현재 플레이어 점수, 클리어 씬에서도 호출하기 위해서 public static(정적 변수)로 선언
    private Text m_ScoreText = null; // 점수를 표시할 Text 컴포넌트
    private Text m_heartText = null; // 목숨 수를 표시할 Text 컴포넌트
    Rigidbody2D m_rigid2DCat = null;   // Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버 변수
    Animator m_animCat = null;       // Cat 오브젝트의 Animator 컴포넌트를 갖는 멤버 변수
    float f_jumpForce = 680.0f;
    float f_walkForce = 30.0f;                      // 플레이어가 가할 힘을 저장할 변수
    float f_maxWalkSpeed = 2.0f;                    // 플레이어가 가질 수 있는 최대 속도를 저장할 변수
    int nLeftRightKeyValue = 0;                     // 플레이어 좌우 움직임 키값 : 오른쪽은 1, 왼쪽은 -1, 아무것도 누르지 않으면 0
    float f_playerMoveSpeed = 0.0f;                  // 플레이어의 이동 속도를 저장할 변수
   
    private int n_playerHeart = 3; // 체력을 3으로 설정
    private int n_FallCount = 0;               // 현재 누적 낙사 횟수
    private Vector3 v_startPosition;       // 리스폰 위치 저장
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * 디바이스 성능에 따른 실행 결과 차이 없애기
         * 어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리
         * 스마트폰은 60, 고속의 PC는 300이 될수 있는 디바이스 성능에 따라 게임 동작에 영향을 미칠 수 있음
         * 프레임레이트를 60으로 고정
         */
        Application.targetFrameRate = 60;                                    // 게임의 초당 프레임을 60으로 설정
        /*
         * 특정 오브젝트의 컴포넌트에 접근하기 위해서는 GetComponent<>() 메소드를 사용
         * Rigidbody2D 컴포넌트를 갖는 메소드를 사용하기 때문에 Start 메소드에서 GetComponent<>() 메소드를 사용해서
         * Rigidbody2D 컴포넌트를 구해 멤버 변수에 저장
         */

        // 아이템 게임 오브젝트 불러오기 
        emeraldItem = GameObject.Find("emerald");
        cherryItem = GameObject.Find("cherry");
        bombItem = GameObject.Find("bomb");
        cherryItem1 = GameObject.Find("cherry1");
        emeraldItem1 = GameObject.Find("emerald1");
        emeraldItem2 = GameObject.Find("emerald2");


        m_heartText = GameObject.Find("Lives").GetComponent<Text>(); // 텍스트 UI를 Find 함수를 통해 찾기
        m_ScoreText = GameObject.Find("Score").GetComponent<Text>(); // 텍스트 UI를 Find 함수를 통해 찾기

        m_rigid2DCat = GetComponent<Rigidbody2D>();

        m_animCat = GetComponent<Animator>(); // Cat 오브젝트의 Animator 컴포넌트를 갖는 멤버 변수에 저장

        // 시작 위치를 저장
        v_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int nRemainingHeart = n_playerHeart - n_FallCount; // 남은 목숨 선언
        m_heartText.text = "Life : " + nRemainingHeart.ToString();
        // ToString 메소드 : 값을 문자로 변환
        // 정수형 D[자릿수] : 정수를 표시할 때 사용. 지정한 자릿수를 채우지 못하면 왼쪽에 0이 삽입
        // 예) (456).ToString("D5") -> 00456 Decimal %d

        // 화면 밖으로 플레이어가 나가지 않도록하는 코드
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // Camera.main : 프로젝트를 처음 생성했을 떄의 메인카메라를 의미, WorldToViewportPoint : 월드 좌표를 뷰포트 좌표로 변환, 뷰포트를 사용하는 이유는 화면 크기에 따라 좌표가 달라지기 때문에 월드 좌표를 정규화하여 사용
        // 뷰 포트의 좌표는    0에서 1 사이의 값을 가지며, 0은 왼쪽 끝, 1은 오른쪽 끝을 의미, 상하도 마찬가지로 0은 아래쪽 끝, 1은 위쪽 끝을 의미
        if (pos.x < 0f) // x좌표가 0보다 작으면
        {
            pos.x = 0f; // x좌표를 0으로
        }

        if (pos.x > 1f) // x좌표가 1보다 크면
        {
            pos.x = 1f; // x좌표를 1로
        }

        if (pos.y < 0f) // y좌표가 0보다 작으면
        {
            pos.y = 0f; // y좌표를 0으로
        }

        if (pos.y > 1f) // y좌표가 1보다 크면
        {
            pos.y = 1f; //  y좌표를 1로
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos); // 뷰포트 좌표를 월드 좌표로 변환

        /*
         * AddForce() 메소드를 사용하여 힘을 추가
         * Spacebar key를 누르면 GetKeyDown() 메소드를 사용하여 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
         * 즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce() 메소드를 사용
         */

        // 플레이어가 키를 받을때만 움직이도록 설정, 계속적인 움직임을 막기위해서 키값을 0으로 설정
        nLeftRightKeyValue = 0;

        // 점프 연속 방지

        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)                                 // 스페이스 바 키를 입력 받았을 때
        {
            m_animCat.SetTrigger("JumpTrigger"); // 점프 애니메이션 실행
            m_rigid2DCat.AddForce(transform.up * f_jumpForce);  // 리지디 바디의 힘에 점프력을 추가
        }



        // 플레이어 좌우 이동
        // 플레이어 좌우 움직임 키값 : 오른쪽은 1, 왼쪽은 -1, 아무것도 누르지 않으면 0

        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }

        /*
         * 플레이어 스피드 제한
         * 프레임마다 AddForce 메소드를 사용해 힘을 가하면 플레이어가 계속 가속이 된느 문제점 발생
         * 그래서 플레이어의 이동속도가 지정한 최고 속도 보다 빠르면 힘을 가하는 것을 멈추고 속도를 조절
         * 왼쪽 화살표, 오른쪽 화살표 key가 눌리면 AddForce 메소드를 사용해 좌 우 방향으로 가도록 플레이어에 힘을 가한다
         * 즉, 플레이어에 힘을 가하려면 Rgidbody2D 컴포넌트가 가진 AddForce() 메소드를 사용
         * velocity : 같은 힘을 가해도 동일한 속도로 달릴수 있도록 물리엔진이 자동으로 계산
         * AddForce의 경우 순간적으로 튀어 오르고 점차 속도가 줄어들면서 떨어지는 점프에 적합
         * linearVelocity는 순간적으로 힘을 가해도 같은 속도로 달릴수 있도록 물리엔진이 자동으로 계산
         */
        f_playerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x); // 플레이어 속도
        if (f_playerMoveSpeed < f_maxWalkSpeed) // 플레이어 속도가 최대 속도보다 작을 때
        {
            m_rigid2DCat.AddForce(transform.right * nLeftRightKeyValue * f_walkForce); // 힘을 추가
        }
        /*
         * 움직이는 방향에 따라 플레이어 이미지 반전
         * 플레이어가 오른쪽으로 움직이면 스트라이트도 오른쪽으로 향하고
         * 왼쪽으로 움직이면 왼쪽으로 움직이도록 이미지를 반전시키려면
         * 스프라이트의 x축 방향배율을 -1배로 설정
         * 스프라이트의 배율을 바꾸려면 transform.localScale 속성을 사용
         * 오른쪽 화살표는 1대 왼쪽 화살표는 -1로 설정
         */
        // 움직이는 방향에 따라 반전
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        /*
         * 애니메이션 재생 속도가 플레이어 이동 속도에 비례하도록 수정
         * 플레이어 이동속도가 0이되면 애니메이션 이동 속도도 0으로 정지하고
         * 플레이어 이동 속도가 빨라질수록 애니메이션 속도도 빨라지도록 설정
         * 애니메이션 재생속도를 바꾸려면 컴포넌트의 Animator 속성의 speed를 사용
         */
        m_animCat.speed = f_playerMoveSpeed / 1.0f; // 애니메이션 속도 조절

        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animCat.speed = f_playerMoveSpeed / 2.0f; // 애니메이션 속도 조절
        }
        else
        {
            m_animCat.speed = 1.0f; // 애니메이션 속도 조절
        }
        
        // 플레이어가 화면 밖으로 나갔다면 처음부터
        if (transform.position.y < -10)
        {
            n_FallCount++; // 낙사 횟수 1회 추가
            

            if (n_FallCount >= n_playerHeart) // 낙사 횟수가 플레이어 체력보다 크거나 같으면
            {
                // 최대 낙사 횟수 초과 시 게임 오버 씬 로드
                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                // 리스폰 위치로 되돌리기
                m_rigid2DCat.linearVelocity = Vector2.zero;
                // 플레이어가 움직이지 않도록 설정
                nLeftRightKeyValue = 0;

                // 처음 저장한 위치로 트랜스폼 하도록 함.
                transform.position = v_startPosition;
            }
        }

        if (n_playerHeart <1)
        {
            SceneManager.LoadScene("GameOverScene");
        }

    }
    // 플레이어가 깃발에 닿으면 게임이 종료됨
    // 이 경우 게임 씬에서 클리어 씬으로 전환 되어야함
    // 플레이어가 깃발에 닿았는지는 OnTriggerEnter2D() 메소드에서 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (collision.gameObject == emeraldItem || collision.gameObject == emeraldItem1 || collision.gameObject == emeraldItem2)
            {
                n_Score += 100; // 에메랄드는 100점
            }
            else if (collision.gameObject == cherryItem || collision.gameObject == cherryItem1)
            {
                n_Score += 50; // 체리는 50점
            }
            else if (collision.gameObject == bombItem)
            {
                n_Score -= 30; // 폭탄은 30점 깎기
                if (n_Score < 0) n_Score = 0; // 점수 0점 밑으로 못 떨어지게
            }
            m_ScoreText.text = "Score : " + n_Score.ToString(); // 점수 표시

            Destroy(collision.gameObject); // 아이템 없애기
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("끝!!");
            SceneManager.LoadScene("ClearScene");
        }
    }
}