
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    float fMaxPositionX = 4.0f;
    float fMinPositionX = -4.0f;
    float fPositionX = 0.0f;

    public Transform player; // 최대 높이를 체크하기위한 플레이어 오브젝트 설정
    public TextMeshProUGUI resultText; // 게임오버시 최종높이를 보여주는 텍스트 오브젝트
    public TextMeshProUGUI curHeight;   // 현재 위치저장 텍스트
    private float maxHeight = 0f; // 최고 높이저장하는 변수


    //Cat 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버변수(m_)
    Rigidbody2D m_rigid2DCat = null;
    Animator m_animatorCat = null;
    //플레이어에 가할 힘 값을 저장할 변수
    float fjumpForce = 380.0f;
    //플레이어 좌, 우로 움직이는 가속도
    float fwalkForce = 20.0f;
    //플레이어의 이동속도가 지정한 최고 속도
    float fmaxWalkSpeed = 2.0f;
    //플레이아 좌우 움직임 키 값: 오른쪽 화살 키 -> 1,왼쪽 화살 키 -> 1, 움직이지 않을  -> 0 
    int nleftRightKeyValue = 0;

    void Start()
    {
        
        curHeight.gameObject.SetActive(true);
        Application.targetFrameRate = 60;
        m_rigid2DCat = GetComponent<Rigidbody2D>();
        m_animatorCat = GetComponent<Animator>();
        resultText.gameObject.SetActive(false); // 결과를 보여주는 텍스트 비활성화
    }

    void Update()
    {
        //플레이어 이동
        Move();
        // 점프
        Jump();





        // 플레이어가 최고 높이에서 15 이상 내려가면 게임오버
        if (transform.position.y < maxHeight - 15)
        {
            GameOver(); // 게임오버 호출
        }


    }

    void Move()
    {
        // 좌우이동
        // 플레이어를 오른쪽으로 이동시키는 코드
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nleftRightKeyValue = 1;
        }
        // 플레이어를 왼쪽으로 이동시키는 코드
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            nleftRightKeyValue = -1;
        }
        else
        {
            nleftRightKeyValue = 0;
        }
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);


        // 플레이어 스피드
        float speedx = Mathf.Abs(m_rigid2DCat.linearVelocity.x);

        // 스피드 제한
        if (speedx < fmaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fwalkForce * nleftRightKeyValue);
        }

        // 움직이는 방향에 따라 반전한다.
        if (nleftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nleftRightKeyValue, 1, 1);
        }
        // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = speedx / 2.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }
        if (player.position.y > maxHeight) // 플레이어가 지난번 최고 높이보다 높이 올라가면 최고기록 갱신
        {
            maxHeight = player.position.y;
        }

    }

    void Jump()
    {

        //스페이스바 누를 시 점프 실행
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");

            m_rigid2DCat.AddForce(transform.up * fjumpForce);

        }
    }



    void GameOver()
    {
        
        curHeight.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true); //결과 텍스트를 보이게 함
        resultText.text = "Best Height: " + maxHeight.ToString("F1") + "m"; // TMP 텍스트에 최종 높이를 소수점 1자리로 표시한다.
        Invoke("LoadClearScene", 2f);// 2초 뒤에 ClearScene으로 씬 전환
        
    }

    // 5초뒤에 ClearScene으로 전환하기 위한 코드
    void LoadClearScene()
    {
        SceneManager.LoadScene("ClearScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            GameOver(); // 게임오버 호출
            //Debug.Log("용암 닿음");
        }
    }



    
}


