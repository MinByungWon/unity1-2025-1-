using UnityEngine;

public class StarController : MonoBehaviour
{
    // 멤버변수 선언
    GameObject gPlayer = null;                  // 플레이어 GameObject
    GameObject gPlayer1 = null;                  // 플레이어1 GameObject


    Vector2 vStarCirclePoint = Vector2.zero;   // 별의 중심 좌표
   
    Vector2 vPlayerCirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vStarPlayerDir = Vector2.zero;     // 별 → 플레이어 방향 벡터

    Vector2 vPlayer1CirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vStarPlayer1Dir = Vector2.zero;     // 별 → 플레이어 방향 벡터
   



    float fStarRadius = 0.1f;                  // 별의 반지름 (충돌 판정용)
    float fStarRadius1 = 0.1f;                  // 별의 반지름 (충돌 판정용)
   

    float fPlayerRadius = 0.5f;                 // 플레이어의 반지름 (충돌 판정용)
    float fStarPlayerDistance = 0.0f;          // 별과 플레이어 중심 간 거리

    float fPlayer1Radius = 0.5f;                 // 플레이어의 반지름 (충돌 판정용)
    float fStarPlayer1Distance = 0.0f;          // 별과 플레이어 중심 간 거리



    public int scoreValue = 0; // ⭐ 별마다 점수 부여


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어 오브젝트 찾기
        this.gPlayer = GameObject.Find("cat_0");

        this.gPlayer1 = GameObject.Find("cat 1_0");

        
    }

    // Update is called once per frame
    void Update()
    {
        // 충돌 판정 처리 ----------------------------

        // 별과 플레이어의 중심 위치 계산
        vStarCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vPlayer1CirclePoint = gPlayer1.transform.position;
      


        // 두 중심 좌표 간의 벡터 구하기
        vStarPlayerDir = vStarCirclePoint - vPlayerCirclePoint;
        vStarPlayer1Dir = vStarCirclePoint - vPlayer1CirclePoint;
        

        // 두 오브젝트 중심 간 거리 계산 (벡터의 크기)
        fStarPlayerDistance = vStarPlayerDir.magnitude;
        fStarPlayer1Distance = vStarPlayer1Dir.magnitude;
       

        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정

        if (fStarPlayerDistance < fStarRadius + fPlayerRadius)
        {
            PlayerController playerController = gPlayer.GetComponent<PlayerController>();

            // 충돌 시 점수 증가
            if (playerController != null)
            {
                playerController.AddScore(scoreValue); 
            }
            // 별 제거
            Destroy(gameObject);
        }

        if (fStarPlayer1Distance < fStarRadius1 + fPlayer1Radius)
        {
            PlayerController2 playerController2 = gPlayer1.GetComponent<PlayerController2>();

            // 충돌 시 점수 증가
            if (playerController2 != null)
            {
                playerController2.AddScore(scoreValue);
            }
            // 별 제거
            Destroy(gameObject);
        }

        


    }

}
