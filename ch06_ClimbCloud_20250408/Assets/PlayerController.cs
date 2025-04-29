/*
 * < ����ڰ� �Է��� ��� �÷��̾ �¿�� �����̰ų� ����(�پ������) ��� >
 *   �����̽��ٸ� ������ �����ϰ�, Ű���忡 �ִ� �¿� ȭ��ǥ Ű(��,��)�� ������ �̵��ϴ� ��Ʈ�ѷ� ��ũ��Ʈ�� �ۼ�
 *   �� �����̽��ٸ� ������ ����
 *   �� �÷��̾ �¿�� �����̱�
 */


using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cat ������Ʈ�� Rigidbody2D ������Ʈ�� ���� �������(m_)
    Rigidbody2D m_rigid2DCat = null;

    // �÷��̾ ���� �� ���� ������ ����
    float fJumpForce = 680.0f;

    // �÷��̾� ��,��� �����̴� ���ӵ�
    float fWalkForce = 20.0f;

    // �÷��̾��� �̵� �ӵ��� ������ �ְ� �ӵ�
    float fMaxWalkSpeed = 2.0f;

    // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű -> 1, ���� ȭ�� Ű -> -1, �������� ���� �� -> 0
    int nLeftRightKeyValue = 0;

    // �÷��̾� �¿� �����̴� �ӵ�
    float fPlayerMoveSpeed = 0.0f;

    // �÷��̾� �̵� �ӵ��� ���� �ִϸ��̼� ��� �ӵ��� ����ϱ� ���� �ִϸ����� ���� ����
    Animator m_animatorCat = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* 
         * ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
         *   � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         *   ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         *   �����ӷ���Ʈ�� 60���� ����
        */
        Application.targetFrameRate = 60;

        /*
         * Ư�� ������Ʈ�� ������Ʈ�� �����ϱ� ���ؼ��� GetComponent �Լ��� ���
         *   Rigidbody2D ������Ʈ�� ���� �޼��带 ����ϱ� ������ Start �޼��忡�� GetComponent �޼��带 ����ؼ�
         *   Rigidbody2D ���۳�Ʈ�� ���� ��� ������ ����
         */
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        // GetComponent �޼��带 ����� Animator ���۳�Ʈ�� ����
        m_animatorCat = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
         *  [ AddForce �޼��� : ������Ʈ�� ������ ���� �־� �̵���Ű�� �� ]
         *     Spacebar Key�� ������(GetKeyDown �޼���) AddForce �޼��带 ����� ���� �������� ������ �÷��̾ ���� ���Ѵ�.
         *     ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
         */
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");

            m_rigid2DCat.AddForce(transform.up * fJumpForce);

        }

        // �÷��̾� ��, �� �̵�
        // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű -> 1, ���� ȭ�� Ű -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }

        /*
         * �÷��̾��� ���ǵ� ����
         *   �����Ӹ��� AddForce �޼��带 ����� �������ϸ� �÷��̾ ��� ������ �Ǵ� ������ �߻�
         *   �׷��� �÷��̾��� �̵� �ӵ��� ������ �ְ�ӵ�(fMaxWalkSpeed) ���� ������ ���� ���ϴ� ���� ���߰� �ӵ��� ����
         *   ����ȭ��ǥ, ������ȭ��ǥ Key�� ������ AddForce �޼��带 ����� ��, �� �������� ������ �÷��̾ ���� ���Ѵ�.
         *   ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
         * Velocity : ���� ���� ���ص� ������ �ӵ��� �޸� �� �ֵ��� ���������� �ڵ����� ��� 
         *   AddForce�� ��� ���������� Ƣ�� ������ ���� �ӵ��� �پ��鼭 �������� ������ ����
         *   Velocity�� ������ �ӵ��� �޸��� ���ʰ��� ĳ���� �̵��� ����
         */
        //m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);
        if (fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        }

        /*
         * �����̴� ���⿡ ���� �÷��̾� �̹����� ����
         *   �÷��̾ ���������� �����̸� ��Ʈ����Ʈ�� ���������� ���ϰ�,
         *   �������� �����̸� �������� �����̵��� �̹����� ������Ű����,
         *   ��������Ʈ�� X�� ��������� -1��� ��
         *   ��������Ʈ�� ������ �ٲٷ��� transform ���۳�Ʈ�� localScale ���� ���� ����
         *   ������ȭ��ǥ�� 1��, ����ȭ��ǥ�� X�� �������� -1��
         */
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        /*
         * �ִϸ��̼� ��� �ӵ��� �÷��̾� �̵� �ӵ��� ����ϵ��� ����
         *   �÷��̾� �̵� �ӵ��� 0�̸� �ִϸ��̼� �̵� �ӵ��� 0���� �����ϰ�,
         *   �÷��̾� �̵� �ӵ��� ���������� �ִϸ��̼� �ӵ��� ������
         *   �ִϸ��̼� ��� �ӵ��� �ٲٷ��� ���۳�Ʈ�� speed ���������� ����
         */
        //m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 2.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }
    }

    // �÷��̾ ��߿� ������ ������ �����
    //   �� ������ ������ Ŭ���� ������ ��ȯ�Ǿ�� ��
    // �÷��̾ ��߿� ��Ҵ����� OnTriggerEnter2D �޼���� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("��!!");

        SceneManager.LoadScene("ClearScene");
    }


}
