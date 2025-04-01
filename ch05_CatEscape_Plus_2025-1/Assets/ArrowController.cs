using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Physis�� ������� �ʰ� ȭ���� ������ �Ʒ��� ����Ʈ���� ��ũ��Ʈ
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
        // ArrowGenerator ���������� ���� ����� return GetIsRun()�Լ� ȣ��
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
                // Y ��ǥ�� -0.1f�� �����ϸ� ������Ʈ�� ���ݾ� ������ �Ʒ��� �����δ�
                // �����Ӹ��� ������� ���Ͻ�Ų��.
                transform.Translate(0, -0.1f, 0);

                // ȭ���� ������ �ʴ� ������ ��� �������� ��ǻ�� ���� ����� �ؾ��ϹǷ� �޸� ����
                // ȭ�� ������ ������ ������Ʈ�� �Ҹ��Ų��.
                // Destroy �޼��� : �Ű������� ������ ������Ʈ�� ����
                // �Ű������� �ڽ�(ȭ�� ������Ʈ)�� ����Ű�� gameObject ������ �����ϹǷ� ȭ����
                //       ȭ�� ������ ������ �� �ڱ� �ڽ��� �Ҹ�
                if (transform.position.y < -5.0f)
                {
                    Destroy(gameObject);
                }

                // �浹���� : ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ���� �˰���
                // ȭ���� �߽�(p1)���� �÷��̾ �ѷ��� ���� �߽�(p2)���� �Ÿ�(d)�� ��Ÿ��� ������ �̿��Ͽ� ���Ѵ�.
                //   r1 : ȭ�츦 �ѷ��� ���� ������, r2 : �÷��̾ �ѷ��� ���� ������
                //   �� ���� �߽ɰ��� �Ÿ� d < r1+r1 �浹, �ƴϸ� ���浹
                Vector2 p1 = transform.position; // ȭ�츦 �ѷ��� ���� �߽� ��ǥ
                Vector2 p2 = this.Player.transform.position; //�÷��̾ �ѷ��� ���� �߽�

                //Magnitude�� ������ ����(ũ��)�� ��ȯ ���ش�. ������ ���̴� ��Ÿ����� ������ ���� �� �ִ�
                Vector2 dir = p1 - p2;
                float d = dir.magnitude;
                float r1 = 0.5f;
                float r2 = 1.0f;

                // �� ��(ȭ�� vs �÷���)�� �߽ɰ��� �Ÿ� d < r1+r1 �浹
                if (d < r1 + r2)
                {
                    // �÷��̾ ȭ�쿡 ������ ȭ�� ��Ʈ�ѷο��� ���� ��ũ��Ʈ�� DecreaseHp �޼��带 ȣ��
                    //   ��, ArrowController���� GameDirector ������Ʈ�� �ִ� DecreaseHp �޼��带 ȣ���ϱ� ������
                    //   Find �޼��带 ã�Ƽ� GameDirector ������Ʈ�� ã�´�.            
                    GameObject director = GameObject.Find("GameDirector");

                    // GetComponent �޼��带 ����� GameDirector ������Ʈ�� GameDirector ��ũ��Ʈ�� ���ϰ�,
                    // DecreaseHp �޼��带 �����Ͽ�, ���� ��ũ��Ʈ�� �÷��̾�� ȭ���� �浹�ߴٰ� ����
                    director.GetComponent<GameDirector>().DecreaseHp();

                    // �浹�ߴٸ� ȭ�� ������Ʈ �����
                    Destroy(gameObject);
                }
            }
        }
    }
}
