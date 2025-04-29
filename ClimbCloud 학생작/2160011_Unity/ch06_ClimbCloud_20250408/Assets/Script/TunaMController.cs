using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TunaMController : MonoBehaviour
{
    // 멤버변수 선언
    GameObject gPlayer2 = null;                  // 플레이어2 GameObject
    GameObject playerController3;                  //  스크립트 참조

    Vector2 vTunaMCirclePoint = Vector2.zero;   // 참치M의 중심 좌표
    Vector2 vPlayer2CirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vTunaMPlayerDir = Vector2.zero;     // 참치M → 플레이어 방향 벡터

    float fTunaMRadius = 0.3f;                  // 참치M의 반지름 (충돌 판정용)
    float fPlayerRadius2 = 0.3f;                 // 플레이어의 반지름 (충돌 판정용)
    float fTunaMPlayerDistance = 0.0f;          // 참치M과 플레이어 중심 간 거리


    public int scoreTunaMValue = -100; // 행성 마다 점수 부여

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어2 오브젝트 찾기
        gPlayer2 = GameObject.Find("cat 2_0");

        // GameDirector 오브젝트 참조
        playerController3 = GameObject.Find("PlayerController3");

       
    }

    // Update is called once per frame
    void Update()
    {
        // 프레임마다 등속으로 낙하시킨다.
        transform.Translate(0, -0.1f, 0);

        // 화면 밖으로 나오면 오브젝트를 소멸시킨다.
        if (transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }

        // 참치M과 플레이어의 중심 위치 계산
        vTunaMCirclePoint = transform.position;
        vPlayer2CirclePoint = gPlayer2.transform.position;

        // 두 중심 좌표 간의 벡터 구하기
        vTunaMPlayerDir = vTunaMCirclePoint - vPlayer2CirclePoint;

         // 두 오브젝트 중심 간 거리 계산 (벡터의 크기)
        fTunaMPlayerDistance = vTunaMPlayerDir.magnitude;

        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정
        if (fTunaMPlayerDistance < fTunaMRadius + fPlayerRadius2)
        {
            // 참치M 제거
            Destroy(gameObject);
        }

        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정

        if (fTunaMPlayerDistance < fTunaMRadius + fPlayerRadius2)
        {
            PlayerController3 playerController3 = gPlayer2.GetComponent<PlayerController3>();

            // 충돌 시 점수 증가
            if (playerController3 != null)
            {
                playerController3.SubScore(scoreTunaMValue);
            }
            // 참치M 제거
            Destroy(gameObject);
        }
    }

}
