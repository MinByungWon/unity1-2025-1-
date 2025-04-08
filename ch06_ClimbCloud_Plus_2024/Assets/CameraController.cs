using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ȭ�鿡 ������ �ʴ� �� ���ʱ��� �̵��ϸ� ī�޶� ���� �� �� ���� ������ �߻�
//   �� �������� �ذ��ϱ� ���ؼ��� ī�޶� �÷��̾ ����ٴϸ� ������ �� �ֵ��� ��ũ��Ʈ �ۼ�
public class CameraController : MonoBehaviour
{
    // �÷��̾� ������Ʈ�� ã�� ���ؼ� ��� ���� ����
    GameObject m_Player = null;

    // �÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �÷��̾� Y��ǥ ���� ����
    [SerializeField]
    Vector3 vPlayerPositon = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ������Ʈ�� ã�Ƽ� ��� ������ ����
        m_Player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �����Ӹ��� �÷��̾� ��ǥ�� ���ؼ� ����(vPlayerPositon)
        vPlayerPositon = m_Player.transform.position;

        // �÷��̾� �̵��� ī�޶� ���󰡴� ���� Y�� ����(���� ����)�� ��ȭ�̹Ƿ� ������ ���� Y��ǥ���� �ݿ��Ѵ�.
        //   X��ǥ�� Z��ǥ�� ������ �����Ƿ� ī�޶��� ���� ��ǥ�� �״�� ���
        transform.position = new Vector3(transform.position.x, vPlayerPositon.y, transform.position.z);
    }
}
