
// 화살이 위에서 아래로 1초에 하나씩 떨어지는 기능 --> transform.Translate()
// 화살이 게임화면 밖으로 나오면 화살 오브젝트를 소멸시키는 기능 --> Destroy()

using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // 멘버변수 선언
    GameObject gPlayer = null; // Player Object를 저장할 GameObject 변수, GameObject 변수의 초깃값은 null

    Vector2 vArrowCirclePoint = Vector2.zero;    // 화살를 둘러싼 원의 중심 좌표
    Vector2 vPlayerCirclePoint = Vector2.zero;   // 플레이어를 둘러싼 원의 중심
    Vector2 vArrowPlayerDir = Vector2.zero;      // 화살에서 플레이어까지의 벡터값

    float fArrowRadius = 0.5f;          // 화살 원의 반지름 0.5
    float fPlayerRadius = 1.0f;         // 플레이어 원의 반지름 1.0
    float fArrowPlayerDistance = 0.0f;  // 화살의 중심(vArrowCirclePoint)부터 플레이어를 둘러싼 원의 중심(vPlayerCirclePoint)까지 거리

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * 씬 안에서 오브젝트를 찾는 메서드 : Find
         *    Find 메서드는 오브젝트 이름을 인수로 전달하고,인수 이름이 씬에 존재하면 해당 오브젝트를 반환
         *    플레이어의 좌표를 구하기 위해서 플레이어를 검색하여 오브젝트 변수에 저장
         *    각 오브젝트 상자에 대응하는 오브젝트를 씬 안에서 찾아 넣어야 함
        */
        gPlayer = GameObject.Find("player");

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 화살이 위에서 아래로 1초에 하나씩 떨어지는 기능 --> transform.Translate()
         * Translate 메서드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메서드
         *    Y 좌표에 -0.1f를 지정하면 오브젝트를 조금씩 위에서 아래로 움직인다
         *    프레임마다 등속으로 낙하시킨다.
        */
        transform.Translate(0, -0.1f, 0);


        /*
         * 화살이 게임화면 밖으로 나오면 화살 오브젝트를 소멸시키는 기능 --> Destroy()
         * 화면 밖으로 나온 화살 소멸시키기
         *   화살을 내버려 두면 화면 밖으로 나가게 되고, 눈에 보이지는 않지만 계속 떨어짐
         *   화살이 보이지 않는 곳에서 계속 떨어지면 컴퓨터 역시 계산을 해야하므로 메모리 낭비
         *   메모리가 낭비되지 않도록 화살이 화면 밖으로 나가면 오브젝트를 소멸시켜야 함
         *   Destroy 메서드 : 매개변수로 전달한 오브젝트를 삭제
         *   매개변수로 자신(화살 오브젝트)을 가르키는 gameObject 변수를 전달하므로 화살이
         *   화면 밖으로 나가을 때 자기 자신을 소멸시킴
        */
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        /*
         * 충돌판정 : 원의 중심 좌표와 반경을 사용한 충돌 판정 알고리즘
         * 화살의 중심(vArrowCirclePoint)부터 플레이어를 둘러싼 원의 중심(vPlayerCirclePoint)까지 거리(fArrowPlayerDistance)를 피타고라스 정리를 이용하여 구한다.
         * fArrowRadius : 화살를 둘러싼 원의 반지름, fPlayerRadius : 플레이어를 둘러싼 원의 반지름
         * 두 원의 중심간의 거리 fArrowPlayerDistance > fArrowRadius + fPlayerRadius : 충돌하지 않음
         * 두 원의 중심간의 거리 fArrowPlayerDistance < fArrowRadius + fPlayerRadius : 충돌함
        */
        vArrowCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vArrowPlayerDir = vArrowCirclePoint - vPlayerCirclePoint;

        /*
         * 두 벡터간의 길이를 구하는 메서드 : magnitude
         *  - 메서드 정의 : public float Magnitude(Vector3 vector);
         *  - 벡터는 크기와 방향을 갖기 때문에, 시작점(Initial Point)와 종점(Terminal Point)으로 구성되며, 
         *      이 둘 사이의 거리가 곧 벡터의 크기가 된다.
         *  - 일반적으로 시작점을 벡터의 꼬리, 끝점을 벡터의 머리라고 부른다.
         *  - 벡터는 시작점과 종점의 위치에 관계 없이, 두 벡터의 크기와 방향이 같다면 서로 같은 벡터로 취급한다.
         *  - 벡터는 점의 위치를 나타내는 위치 벡터(Position Vector)를 이용해 표기한다.
         *  - 화살의 중심(vArrowCirclePoint)부터 플레이어를 둘러싼 원의 중심(vPlayerCirclePoint)까지의 거리  
        */
        fArrowPlayerDistance = vArrowPlayerDir.magnitude;

        /*
         * 플레이어가 화살에 맞았는지 감지, 즉 화살과 플레이어의 충동 판정
         *  - 원의 중심 좌표와 반경을 사용해 충돌 판정
         *  - r1 : 화살을 둘러싼 원의 반지름,  r2 : 플레이어를 둘러싼 원의 반지름, d : 화살원 중심에서 플레이어원 중심까지 거리
         *  - 충돌 : 두 원의 중심 간 거리 d가 (r1 + r2)보다 작으면 충돌(d < r1+r2)
         *  - 미충돌 : 두 원의 중심 간 거리 d가 (r1 + r2)보다 크면 두 원은 충돌하지 않음(d > r1+r2)
         *  - 충돌(fArrowPlayerDistance < ( fArrowRadius + fPlayerRadius)) 이면 화살 오브젝트 소멸
        */
        if (fArrowPlayerDistance < fArrowRadius + fPlayerRadius)
        {
            /*
             * 플레이어가 화살에 맞으면 화살 컨트롤로에서 감독 스크립트(GameDirector)의 f_DecreaseHp() 메서드를 호출
             *   즉, ArrowController에서 GameDirector 오브젝트에 있는 f_DecreaseHp() 메서드를 호출하기 때문에
             *   Find 메서드를 찾아서 GameDirector 오브젝트를 찾는다.   
            */
            GameObject gDirector = GameObject.Find("GameDirector");

            /*
             * GetComponent 메서드를 사용해 GameDirector 오브젝트의 GameDirector 스크립트를 구하고,
             *   f_DecreaseHp() 메서드를 구출하여, 감독 스크립트에 플레이어와 화살이 충돌했다고 전달
            */
            gDirector.GetComponent<GameDirector>().f_DecreaseHp();

            Destroy(gameObject);  // 화살과 플레이어 충돌, 화살 오브젝트를 소멸
        }

    }
}
