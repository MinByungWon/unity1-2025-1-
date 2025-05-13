using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    public float speed = 1.0f;  // 구름이 움직이는 속도

    private int direction = 1;  // 1이면 오른쪽 -1이면 왼쪽

    
    
    //게임 시작 후 플레이어가 구름보다  일정 거리 이상 위로 올라갈 시 아래쪽 구름은 삭제함
    void Update()
    {
        //게임 태그가 움직이는 구름만 움직이게 설정
        if (this.gameObject.tag == "MovingCloud")
        {
            Move();
        }

        if (transform.position.y > 20f)
        {
            speed = 2.0f;
        }
        else if (transform.position.y > 40f)
        {
            speed = 3.0f;
        }


    }


    void Move()
    {
        {
            // 구름 움직임
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

            // 일정 범위를 넘어가면 방향전환
            if (transform.position.x >= 2)
            {
                direction = -1;

            }
            else if (transform.position.x <= -2)
            {
                direction = 1;

            }
        }
    }
    


}
