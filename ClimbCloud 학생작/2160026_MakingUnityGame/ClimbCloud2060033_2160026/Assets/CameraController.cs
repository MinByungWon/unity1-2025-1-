// �÷��̾ ȭ�鿡 ������ �ʴ� �� ���ʱ��� �̵��ϸ� ī�޶� ���� �� �� ���� ������ �߻�
// �� ������ �ذ��ϱ� ���� ī�޶� �÷��̾ ���󰡵��� ����

using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�÷��̾� ������Ʈ�� ã�����ؼ� ��������� ����
    GameObject m_gPlayer = null; // �÷��̾� ������Ʈ�� ������ ��� ����

    // �÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �÷��̾� y��ǥ�� ������ ��� ����
    Vector3 vPlayerPosition = Vector3.zero; // �÷��̾��� y��ǥ�� ������ ��� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_gPlayer = GameObject.Find("cat"); // �÷��̾� ������Ʈ�� ã�� ���� GameObject.Find() �޼ҵ带 ����Ͽ� Cat ������Ʈ�� ã��

    }

    // Update is called once per frame
    void Update()
    {
        vPlayerPosition = m_gPlayer.transform.position; // �÷��̾��� y��ǥ�� ����(vPlayerPosition)�ϱ� ���� Cat ������Ʈ�� transform.position�� ���

        // �÷��̾� �̵��� ī�޶� ���󰡴� ���� y���� ������ ��ȭ �̹Ƿ� ���� y��ǥ�� �ݿ��Ѵ�.
        // x��ǥ�� z��ǥ�� ī�޶��� ��ġ�� �����ϱ� ���ؼ� ���
        transform.position = new Vector3(transform.position.x, vPlayerPosition.y, transform.position.z); // ī�޶��� ��ġ�� �÷��̾��� y��ǥ�� ����
    }
}
