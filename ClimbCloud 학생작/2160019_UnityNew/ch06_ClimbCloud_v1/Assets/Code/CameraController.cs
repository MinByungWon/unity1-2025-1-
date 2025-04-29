using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject m_player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.m_player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾��� ������ ���Ͱ��� �޾ƿ�
        Vector3 vPlayerPos = this.m_player.transform.position;

        //x�� z�� ���� �״�� ��ä�� �÷��̾��� Vector���� ���ο� ���������� ����
        transform.position = new Vector3(transform.position.x, vPlayerPos.y, transform.position.z);
    }
}
