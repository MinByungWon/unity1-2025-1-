using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene을 사용하기 위해서는 SceneManagement를 임포드해야합니다.
using UnityEngine.UI;               // UI 컴포넌트에 접근하기 위해서는 UnityEngine.UI를 임포트해야 합니다.

public class PlayerController : MonoBehaviour
{
    // Cat 오브잭트의 Rigidbody2D 컴포넌트를 갖는 멤버변수(m_)
    [SerializeField]
    Rigidbody2D m_rigid2DCat;

    // 플레이어 이동 속도에 맞춰 애니메이션 재생 속도를 재생하기 위해 애니메이터 변수 선언
    [SerializeField]
    Animator m_animatorCat;

    // 플레이어에 가할 힘 값을 저장할 변수
    [SerializeField]
    float fJumpForce = 680.0f;

    // 플레이어 좌,우로 움직이는 가속도
    [SerializeField]
    float fWalkForce = 20.0f;

    // 플레이어 좌,우로 움직이는 속도
    [SerializeField]
    float fPlayerMoveSpeed = 0.0f;

    // 플레이어의 이동 속도가 지정한 최고 속도
    [SerializeField]
    float fMaxWalkSpeed = 1.0f;

    // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1, 움직이지 않을 때 -> 0
    [SerializeField]
    int nLeftRightKeyValue = 0;

    Vector2 vecPreviousPosition = Vector2.zero;

    // Score 계산을 우한 멤버 변수
    [SerializeField]
    int m_nScore = 0;

    // Score를 보여 주기 위한 text 
    Text m_textScore = null;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D 컴포넌트를 갖는 메서드를 사용하기 때문에 Start 메서드에서 GetComponent 메서드를 사용해
        //   Rigidbody2D 컴퍼넌트를 구해 멤버 변수에 저장
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        // GetComponent 메서드를 사용해 Animator 컴퍼넌트를 구함
        m_animatorCat = GetComponent<Animator>();

        // GetComponent 메서드를 사용해 Text 컴퍼넌트를 구함
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreObject != null)
        {
            m_textScore = scoreObject.GetComponent<Text>();
            m_textScore.text = string.Format("Score : {0}", m_nScore.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spacebar Key가 눌리면 AddForce 메서드를 사용해 위쪽 방향으로 가도록 플레이어에 힘을 가한다.
        //   즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
        //   플레이어가 점프하는 도중에 점프하는 것을 방지하기 위해서 m_rigid2DCat.velocity.y == 0 추가
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.velocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");

            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }

        // 플레이어 좌, 우 이동
        // 플레이어 좌우 움직임 키 값 : 오른쪽 화살 키 -> 1, 왼쪽 화살 키 -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }
        else
        {
            nLeftRightKeyValue = 0;
        }

        // 플레이어의 이동 속도
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.velocity.x);

        // 플레이어의 스피드 제한
        //   프레임맏 AddForce 메서드를 사용해 힘을가하면 플레이어가 계속 가속이 되는 문제점 발생
        //   그래서 플레이어의 이동 속도가 지정한 최고속도(fMaxWalkSpeed) 보다 빠르면 힘을 가하는 것을 멈추고 속도를 조절
        // 왼쪽화살표, 오른쪽화살표 Key가 눌리면 AddForce 메서드를 사용해 좌, 우 방향으로 가도록 플레이어에 힘을 가한다.
        // 즉, 플레이어에 힘을 가하려면 Rigidbody2D 컴포넌트가 가진 AddForce 메서드를 사용한다.
        if (fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * nLeftRightKeyValue * fWalkForce);
        }

        // 움직이는 방향에 따라 플레이어 이미지를 반전
        //   플레이어가 오른쪽으로 움직이면 스트라이트도 오른쪽으로 향하고,
        //   왼쪽으로 움직이면 왼쪽으로 움직이도록 이미지를 반전싴려면,
        //   스프라이트의 X축 방향배율을 -1배로 함
        //   스프라이트의 배율을 바꾸려면 transform 컴퍼넌트의 localScale 변수 값을 변경
        //   오른쪽화살표는 1배, 왼쪽화살표는 X축 방향으로 -1배
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        // 애니메이션 재생 속도가 플레이어 이동 속도에 비례
        //   플레이어 이동 속도가 0이면 애니메이션 이동 속도도 0으로 정지하고,
        //   플레이어 이동 속도가 빨라질수록 애니메이션 속도가 빨라짐
        // 애니메이션 재생 속도를 바꾸려면 컴퍼넌트의 speed 변수값으로 조정
        if (m_rigid2DCat.velocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }


        // 플레이어가 화면 밖으로 나가도 끝없이 떨어지는 버그 수정
        //   플레이어의 Y 좌표가 -10 미만이면 씬의 처음으로 돌아가도록 GameScene을 로드
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

    }

    // 프레임마다 호출되는 함수 : LateUpdate()
    private void LateUpdate()
    {
        //Vector.Distance(a, b)를 이용하여 거리를 구할 수 있습니다. 이때, a와 b 사이의 거리를 구하는 메서드
        float fMoveResult = Vector2.Distance(vecPreviousPosition, transform.position);

        if(fMoveResult < 0.001f && nLeftRightKeyValue != 0)
        {
            Vector2 vecMoveTargetPosition = transform.position + (transform.right * nLeftRightKeyValue * 0.1f);

            RaycastHit2D hit = Physics2D.Raycast(vecMoveTargetPosition, Vector2.down);
            if(hit.collider != null)
            {
                transform.position = vecMoveTargetPosition;
            }
        }

        // (1안)플레이어 화면 밖으로 나가는 문제점 해결
        //      Clamp(클램프) : 최소 / 최대값 을 설정하여 float 값이 범위 이외의 값을 넘지 않도록 합니다.
        Vector2 vecMoveResultPosition = transform.position;
        vecMoveResultPosition.x = Mathf.Clamp(vecMoveResultPosition.x, -2.7f, 2.7f);
        transform.position = vecMoveResultPosition;

        // (2안)플레이어 화면 밖으로 나가는 문제점 해결
        //      Clamp(클램프) : 최소 / 최대값 을 설정하여 float 값이 범위 이외의 값을 넘지 않도록 합니다.
        //float fPositionX = transform.position.x;
        //fPositionX = Mathf.Clamp(fPositionX, -2.7f, 2.7f);
        //transform.position = new Vector2(fPositionX, transform.position.y);

        vecPreviousPosition = transform.position;
    }

    // 플레이어가 깃발에 닿으면 게임이 종료됨
    //   이 경우게임 씬에서 클리어 씬으로 전환되어야 함
    //   플레이어가 깃발에 닿았는지는 OnTriggerEnter2D 메서드로 감지함
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("flag"))
        {
            // Debug.Log("골!!");
            SceneManager.LoadScene("ClearScene");
        }
        else if(collision.CompareTag("Item"))
        {

            if(collision.GetComponent<Item>() != null)
            {
                //점수 획득
                //Debug.Log("아이템 획득 " + collision.GetComponent<Item>().GetScore() + " 점");
                m_nScore += collision.GetComponent<Item>().GetScore();
                m_textScore.text = string.Format("Score : {0}", m_nScore.ToString());
            }

            Destroy(collision.gameObject);
        }
        else
        {

        }
    }
}
