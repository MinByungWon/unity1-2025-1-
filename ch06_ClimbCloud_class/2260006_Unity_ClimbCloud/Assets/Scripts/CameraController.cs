/*
 * �÷��̾ ȭ�鿡 ������ �ʴ� �� ���ʱ��� �̵��ϸ�, ī�޶� ���� �� �� ���� ������ �߻�
 * �� �������� �ذ��ϱ� ���ؼ���, ī�޶� �÷��̾ ����ٴϸ� ������ �� �ֵ��� ��ũ��Ʈ �ۼ�
 */
using UnityEditor.VersionControl;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject m_gPlayer = null; //�÷��̾� ������Ʈ ����
    Vector3 vPlayerPos = Vector3.zero; //�÷��̾��� y��ǥ�� �����ϱ� ���� ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_gPlayer = GameObject.Find("player"); //Find �޼ҵ带 ����Ͽ� player ������Ʈ�� ã�ƿ´�.
    }

    // Update is called once per frame
    void Update()
    {
        f_PlayerCamPosSync(); //�÷��̾��� ��ġ�� ���� ī�޶��� ��ġ�� ����ȭ�ϴ� �޼ҵ�
    }

    void f_PlayerCamPosSync()
    {
        /*
         * �÷��̾ ���� �̵��� ������ ī�޶� ����ٴϵ��� �����Ӹ��� �÷��̾� ��ǥ�� ���ؼ� ����
         * �÷��̾� �̵��� ī�޶� ���󰡴� ���� Y�� ����(����)�� ��ȭ�̹Ƿ� ������ ���� Y��ǥ���� �ݿ��Ѵ�.
         * X, Z��ǥ�� ������ �����Ƿ� ī�޶��� ���� ��ǥ�� �״�� ���
         */

        vPlayerPos = m_gPlayer.transform.position; //�÷��̾��� ��ġ�� ���� ������ ����

        transform.position = new Vector3(transform.position.x, vPlayerPos.y, transform.position.z); //���� ī�޶��� y�� ���� �÷��̾� ��ġ�� ����
    }
}
