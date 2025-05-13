using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    public float speed = 1.0f;  // ������ �����̴� �ӵ�

    private int direction = 1;  // 1�̸� ������ -1�̸� ����

    
    
    //���� ���� �� �÷��̾ ��������  ���� �Ÿ� �̻� ���� �ö� �� �Ʒ��� ������ ������
    void Update()
    {
        //���� �±װ� �����̴� ������ �����̰� ����
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
            // ���� ������
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

            // ���� ������ �Ѿ�� ������ȯ
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
