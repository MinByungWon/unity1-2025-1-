using UnityEngine;

public class PlanetController : MonoBehaviour
{
    // 멤버변수 선언
    GameObject gPlayer = null;                  // 플레이어 GameObject
    GameObject playerController;                  //  스크립트 참조

    GameObject gPlayer1 = null;                  // 플레이어1 GameObject
    GameObject playerController2;                  // 스크립트 참조


    Vector2 vPlanetCirclePoint = Vector2.zero;   // 행성 중심 좌표
    Vector2 vPlayerCirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vPlanetPlayerDir = Vector2.zero;     // 행성 → 플레이어 방향 벡터

    Vector2 vPlayer1CirclePoint = Vector2.zero;  // 플레이어의 중심 좌표
    Vector2 vPlanetPlayer1Dir = Vector2.zero;     // 화살 → 플레이어 방향 벡터

  

    float fPlanetRadius = 0.3f;                  // 행성 반지름 (충돌 판정용)
    float fPlayerRadius = 0.3f;                 // 플레이어의 반지름 (충돌 판정용)
    float fPlanetPlayerDistance = 0.0f;          // 행성과 플레이어 중심 간 거리
    float fPlayer1Radius = 0.3f;                 // 플레이어의 반지름 (충돌 판정용)
    float fPlanetPlayer1Distance = 0.0f;          // 행성과 플레이어 중심 간 거리


    public int scoreplanetValue = -100; // 행성 마다 점수 부여

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어 오브젝트 찾기
        gPlayer = GameObject.Find("cat_0");

        // GameDirector 오브젝트 참조
        playerController = GameObject.Find("PlayerController");

        // 플레이어 오브젝트 찾기
        gPlayer1 = GameObject.Find("cat 1_0");

        // GameDirector 오브젝트 참조
        playerController = GameObject.Find("PlayerController2");

      
    }

    // Update is called once per frame
    void Update()
    {
        /*
       * 행성이 위에서 아래로 1초에 하나씩 떨어지는 기능 --> transform.Translate()
       * Translate 메서드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메서드
       *    Y 좌표에 -0.1f를 지정하면 오브젝트를 조금씩 위에서 아래로 움직인다
       *    프레임마다 등속으로 낙하시킨다.
       */



        // 프레임마다 등속으로 낙하시킨다.
        transform.Translate(0, -0.1f, 0);

        /*
        *   행성이 게임화면 밖으로 나오면 행성 오브젝트를 소멸시키는 기능 --> Destroy()
        *   화면 밖으로 나온 행성 소멸시키기
        *   행성을 내버려 두면 화면 밖으로 나가게 되고, 눈에 보이지는 않지만 계속 떨어짐
        *   행성이 보이지 않는 곳에서 계속 떨어지면 컴퓨터 역시 계산을 해야하므로 메모리 낭비
        *   메모리가 낭비되지 않도록 행성이 화면 밖으로 나가면 오브젝트를 소멸시켜야 함
        *   Destroy 메서드 : 매개변수로 전달한 오브젝트를 삭제
        *   매개변수로 자신(행성 오브젝트)을 가르키는 gameObject 변수를 전달하므로 행성이
        *   화면 밖으로 나가을 때 자기 자신을 소멸시킴
        */

        // 화면 밖으로 나오면 오브젝트를 소멸시킨다.
        if (transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }
        // 충돌 판정 처리 ----------------------------

        // 행성과 플레이어의 중심 위치 계산
        vPlanetCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vPlayer1CirclePoint = gPlayer1.transform.position;
   

        // 두 중심 좌표 간의 벡터 구하기
        vPlanetPlayerDir = vPlanetCirclePoint - vPlayerCirclePoint;
        vPlanetPlayer1Dir = vPlanetCirclePoint - vPlayer1CirclePoint;
       

        // 두 오브젝트 중심 간 거리 계산 (벡터의 크기)
        fPlanetPlayerDistance = vPlanetPlayerDir.magnitude;
        fPlanetPlayer1Distance = vPlanetPlayer1Dir.magnitude;
    

        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정
        if (fPlanetPlayerDistance < fPlanetRadius + fPlayerRadius)
        {
            // 행성 제거
            Destroy(gameObject);
        }

        if (fPlanetPlayer1Distance < fPlanetRadius + fPlayer1Radius)
        {
            // 행성 제거
            Destroy(gameObject);
        }


        // 충돌 조건: 거리 < 두 반지름의 합 → 원형 충돌 판정

        if (fPlanetPlayerDistance < fPlanetRadius + fPlayerRadius)
        {
            PlayerController playerController = gPlayer.GetComponent<PlayerController>();

            // 충돌 시 점수 증가
            if (playerController != null)
            {
                playerController.SubScore(scoreplanetValue);
            }
            // 별 제거
            Destroy(gameObject);
        }

        if (fPlanetPlayer1Distance < fPlanetRadius + fPlayer1Radius)
        {
            PlayerController2 playerController2 = gPlayer1.GetComponent<PlayerController2>();

            // 충돌 시 점수 증가
            if (playerController2 != null)
            {
                playerController2.SubScore(scoreplanetValue);
            }
            // 별 제거
            Destroy(gameObject);
        }

      
    }
}