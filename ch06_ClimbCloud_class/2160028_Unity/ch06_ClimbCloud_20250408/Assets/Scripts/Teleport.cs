using UnityEngine;

public class Teleport : MonoBehaviour
{
    // �����̵��� ��ġ ����
    private Vector3 teleportPosition = new Vector3(1.0f, 3.5f, 0.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // �÷��̾ �� ������Ʈ�� �ε�����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))             // �÷��̾ �����ۿ� ����� ��
        {
            other.transform.position = teleportPosition;    // �÷��̾ �����̵��� ��ġ(teleportPosition)�� ��Ŵ
            Destroy(gameObject);                            // �������� ������ �����(�ѹ��� ���� �� �ֵ���)
        }
    }
}
