using UnityEngine;

public class TunaController : MonoBehaviour
{
    // 멤버변수 선언
    GameObject gPlayer2 = null;                  // 플레이어2 GameObject

    Vector2 vTunaCirclePoint = Vector2.zero;   // 참치의 중심 좌표

    Vector2 vPlayer2CirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vTunaPlayer2Dir = Vector2.zero;     // 참치 → 플레이어 방향 벡터

    float fTunaRadius2 = 0.1f;                  // 참치의 반지름 (충돌 판정용)

    float fPlayer2Radius = 0.5f;                 // 플레이어의 반지름 (충돌 판정용)
    float fTunaPlayer2Distance = 0.0f;          // 참치과 플레이어 중심 간 거리

    public int scoreValue = 0; //참치마다 점수 부여

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어 오브젝트 찾기
        this.gPlayer2 = GameObject.Find("cat 2_0");

    }

    // Update is called once per frame
    void Update()
    {
        // 충돌 판정 처리 ----------------------------

        // 참치와 플레이어의 중심 위치 계산
        vTunaCirclePoint = transform.position;
        vPlayer2CirclePoint = gPlayer2.transform.position;
   
        // 두 중심 좌표 간의 벡터 구하기
        vTunaPlayer2Dir = vTunaCirclePoint - vPlayer2CirclePoint;

        // 두 오브젝트 중심 간 거리 계산 (벡터의 크기)
        fTunaPlayer2Distance = vTunaPlayer2Dir.magnitude;

        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정

        if (fTunaPlayer2Distance < fTunaRadius2 + fPlayer2Radius)
        {
            PlayerController3 playerController3 = gPlayer2.GetComponent<PlayerController3>();

            // 충돌 시 점수 증가
            if (playerController3 != null)
            {
                playerController3.AddScore(scoreValue);
            }
            // 참치 제거
            Destroy(gameObject);
        }

    }

}
