using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed = 1f;

    private float minx = -1.5f;
    private float maxx = 1.5f;

    private float moveRange;
    private float offsety;
    private float offsetz;

    public bool Move = false;

    void Start()
    {
        // �±װ� "MoveCloud"�� ��쿡�� ������
        if (CompareTag("MoveCloud"))
        {
            Move = true;        //������ Ȱ��ȭ
        }

        if (Move)
        {
            // ���� x�� ��ǥ�� -1.5 ~ 1.5 ���� �ȿ� �ִ��� Ȯ��, ����
            float startX = Mathf.Clamp(transform.position.x, minx, maxx);

            // �̵� ������ �Ÿ� ���
            moveRange = (maxx - minx);

            // y,z ��ǥ�� ����(�� ����)
            offsety = transform.position.y;
            offsetz = transform.position.z;

            // x��ǥ�� minx, maxx ���̿� ����
            transform.position = new Vector3(startX, offsety, offsetz);
        }
    }

    void Update()
    {
        if (Move)
        {
            // Mathf.PingPong �Լ��� ����� minx, maxx�� ���� �ȿ��� �ݺ�
            float x = Mathf.PingPong(Time.time * speed, moveRange) + minx;

            // x ���� ��ȭ, y,z�� �� ��ȭ x
            transform.position = new Vector3(x, offsety, offsetz);
        }
    }
}
