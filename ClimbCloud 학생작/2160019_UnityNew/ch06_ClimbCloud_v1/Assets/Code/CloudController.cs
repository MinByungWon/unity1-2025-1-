using UnityEngine;

public class CloudController : MonoBehaviour
{
    // ������ �̵� ������ ǥ��
    int nCloudMove = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ������ �̵� ���
        transform.Translate(nCloudMove * Time.deltaTime, 0, 0);

        // x �������� 1.7�̻�, -1.7���ϰ� �Ǹ� �ݴ�������� �ٲٰ� ����
        if (transform.position.x > 1.7 || transform.position.x < -1.7)
        {
            nCloudMove *= -1;
        }
    }

}
