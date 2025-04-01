using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Physis를 사용하지 않고 화살을 위에서 아래로 떨어트리는 스크립트
public class ArrowController : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        // ArrowGenerator 오브젝에서 게임 재시작 return GetIsRun()함수 호출
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Translate 메서드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메서드
                // Y 좌표에 -0.1f를 지정하면 오브젝트를 조금씩 위에서 아래로 움직인다
                // 프레임마다 등속으로 낙하시킨다.
                transform.Translate(0, -0.1f, 0);

                // 화살이 보이지 않는 곳에서 계속 떨어지면 컴퓨터 역시 계산을 해야하므로 메모리 낭비
                // 화면 밖으로 나오면 오브젝트를 소멸시킨다.
                // Destroy 메서드 : 매개변수로 전달한 오브젝트를 삭제
                // 매개변수로 자신(화살 오브젝트)을 가르키는 gameObject 변수를 전달하므로 화살이
                //       화면 밖으로 나가을 때 자기 자신을 소멸
                if (transform.position.y < -5.0f)
                {
                    Destroy(gameObject);
                }

                // 충돌판정 : 원의 중심 좌표와 반경을 사용한 충돌 판정 알고리즘
                // 화살의 중심(p1)부터 플레이어를 둘러싼 원의 중심(p2)까지 거리(d)를 피타고라스 정리를 이용하여 구한다.
                //   r1 : 화살를 둘러싼 원의 반지름, r2 : 플레이어를 둘러싼 원의 반지름
                //   두 원의 중심간의 거리 d < r1+r1 충돌, 아니면 미충돌
                Vector2 p1 = transform.position; // 화살를 둘러싼 원의 중심 좌표
                Vector2 p2 = this.Player.transform.position; //플레이어를 둘러싼 원의 중심

                //Magnitude는 벡터의 길이(크기)를 반환 해준다. 벡터의 길이는 피타고라스의 정리로 구할 수 있다
                Vector2 dir = p1 - p2;
                float d = dir.magnitude;
                float r1 = 0.5f;
                float r2 = 1.0f;

                // 두 원(화살 vs 플레이)의 중심간의 거리 d < r1+r1 충돌
                if (d < r1 + r2)
                {
                    // 플레이어가 화살에 맞으면 화살 컨트롤로에서 감독 스크립트의 DecreaseHp 메서드를 호출
                    //   즉, ArrowController에서 GameDirector 오브젝트에 있는 DecreaseHp 메서드를 호출하기 때문에
                    //   Find 메서드를 찾아서 GameDirector 오브젝트를 찾는다.            
                    GameObject director = GameObject.Find("GameDirector");

                    // GetComponent 메서드를 사용해 GameDirector 오브젝트의 GameDirector 스크립트를 구하고,
                    // DecreaseHp 메서드를 구출하여, 감독 스크립트에 플레이어와 화살이 충돌했다고 전달
                    director.GetComponent<GameDirector>().DecreaseHp();

                    // 충돌했다면 화살 오브젝트 지운다
                    Destroy(gameObject);
                }
            }
        }
    }
}
