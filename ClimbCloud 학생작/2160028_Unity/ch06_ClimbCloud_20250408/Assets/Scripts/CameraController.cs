
// �÷��̾ ȭ�鿡 ������ �ʴ� �� ���ʱ��� �̵��ϸ�, ī�޶� ���� �� �� ���� ������ �߻�
// �� �������� �ذ��ϱ� ���ؼ���, ī�޶� �÷��̾ ����ٴϸ� ������ �� �ֵ��� ��ũ��Ʈ �ۼ�

using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �÷��̾� ������Ʈ�� ã�� ���ؼ� ��� ���� ����
    GameObject m_gPlayer = null;

    //�÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �÷��̾� Y��ǥ ���� ����
    Vector3 vPlayerPosition = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�÷��̾� ������Ʈ�� ã�Ƽ� ��� ������ ����
        m_gPlayer = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �����Ӹ��� �÷��̾� ��ǥ�� ���ؼ� ����(vPlayerPosition)
        vPlayerPosition = m_gPlayer.transform.position;

        // �÷��̾� �̵��� ī�޶� ���󰡴� ���� Y�� ����(���� ����)�� ��ȭ�̹Ƿ� ������ ���� Y��ǥ���� �ݿ��Ѵ�.
        // X��ǥ�� Z��ǥ�� ������ �����Ƿ� ī�޶��� ���� ��ǥ�� �״�� ���
        transform.position = new Vector3(transform.position.x, vPlayerPosition.y, transform.position.z);
    }
}
