using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float f_moveSpeed = 1.0f;    // ���� �̵� �ӵ�
    private float f_moveRange = 3.0f;     // �̵� ����

    private Vector3 v_startPosition;    // ���� ��ġ ����
    private int n_direction = 1;         // �̵� ���� (1=������, -1=����)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        v_startPosition = transform.position; // ó�� ��ġ ����
    }

    // Update is called once per frame
    void Update()
    {
        MoveCloud(); // ���� �̵��Լ� ȣ��
    }

    public void MoveCloud()
    {
        // �̵�
        transform.Translate(Vector3.right * n_direction * f_moveSpeed * Time.deltaTime);

        // ���� �Ÿ� ����� ���� ��ȯ
        if (Mathf.Abs(transform.position.x - v_startPosition.x) > f_moveRange)
        {
            n_direction *= -1; // ���� ����
        }
    }
}
