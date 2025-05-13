/*
 * < ����ڰ� �Է��� ��� �÷��̾ �¿�� �����̰ų� ����(�پ������) ��� >
 * �����̽��ٸ� ������ �����ϰ�, Ű���忡 �ִ� �¿� ȭ��ǥ Ű(<-, ->)�� ������ �̵��ϴ� ��Ʈ�ѷ� ��ũ��Ʈ�� �ۼ�
 * 1. �����̽��ٸ� ������ ����
 * 2. �÷��̾ �¿�� �����̱�
 */


using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cat ������Ʈ�� Rigidbody2D ������Ʈ�� ���� �������(m_)
    Rigidbody2D m_rigid2DCat = null;

    // �÷��̾ ���� �� ���� ������ ����
    float fJumpForce = 680.0f;

    // �÷��̾� ��, ��� �����̴� ���ӵ�
    float fWalkForce = 30.0f;

    // �÷��̾��� �̵� �ӵ��� ������ �ְ� �ӵ�
    float fMaxWalkSpeed = 2.0f;

    // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű -> 1, ���� ȭ�� Ű -> -1, �������� ���� �� -> 0
    int nLeftRightKeyValue = 0;

    // �÷��̾� �¿� �����̴� �ӵ�
    float fPlayerMoveSpeed = 0.0f;

    // �÷��̾� �̵� �ӵ��� ���� �ִϸ��̼� ��� �ӵ��� ����ϱ� ���� �ִϸ����� ���� ����
    Animator m_animatorCat = null;

    //�÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ� ���� ����
    float fMaxPositionX = 2.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ� ���� ����
    float fminPositionX = -2.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ּڰ� ���� ����
    float fPositionX = 0.0f;    // �÷��̾ ��, �� �̵��� �� �ִ� X ��ǥ ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
         * � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         * ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         * �����ӷ���Ʈ�� 60���� ����
         */
        Application.targetFrameRate = 60;

        /*
         * Ư�� ������Ʈ�� ������Ʈ�� �����ϱ� ���ؼ��� GetComponent �Լ��� ���
         * Rigidbody2D ������Ʈ�� ���� �޼��带 ����ϱ� ������ Start �޼��忡�� GetComponent �޼��带 ����ؼ�
         * Rigidbody2D ������Ʈ�� ���� ��� ���� ����
         */
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        //GetComponent �޼��带 ����� Animator ������Ʈ�� ����
        m_animatorCat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * [AddForce �޼��� : ������Ʈ�� ������ ���� �־� �̵���Ű�� ��]
         * Spacebar Key�� ������(GetKeyDown �޼���) AddForce �޼��带 ����� ���� �������� ������ �÷��̾ ���� ���Ѵ�.
         * ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
         */
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.SetTrigger("JumpTrigger");
            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }

        //Ű ������ �ʾ��� �� ����
        nLeftRightKeyValue = 0;

        // �÷��̾� ��, �� �̵�
        // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű ->, ���� ȭ�� Ű -> -1
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
         *  �����Ӹ��� AddForce �޼��带 ����� ���� ���ϸ� �÷��̾ ��� ������ �Ǵ� ������ �߻�
         *  �׷��� �÷��̾��� �̵� �ӵ��� ������ �ְ�ӵ�(fMaxWalkSpeed) ���� ������ ���� ���ϴ� ���� ���߰� �ӵ��� ����
         *  ����ȭ��ǥ, ������ȭ��ǥ Key�� ������ AddForce �޼��带 ����� ��, �� �������� ������ �÷��̾ ���� ���Ѵ�.
         *  ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
         * Velocity : ���� ���� ���ص� ������ �ӵ��� �޸� �� �ֵ��� ���������� �ڵ����� ���
         *  AddForce�� ��� ���������� Ƣ�� ������ ���� �ӵ��� �پ��鼭 �������� ������ ����
         *  Velocity�� ������ �ӵ��� �޸��� ���ʰ��� ĳ���� �̵��� ����
         */
        //m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);
        if(fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * fWalkForce * nLeftRightKeyValue);
        }

        /*
         * �����̴� ���⿡ ���� �÷��̾� �̹����� ����
         * �÷��̾ ���������� �����̸� ��������Ʈ�� ���������� ���ϰ�,
         * �������� �����̸� �������� �����̵��� �̹����� ������Ű����,
         * ��������Ʈ�� x�� ��������� -1��� ��
         * ��������Ʈ�� ������ �ٲٷ��� tranform ������Ʈ�� localScale ���� ���� ����
         * ������ȭ��ǥ�� 1��, ����ȭ��ǥ�� x�� �������� -1��
         */
        if(nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        //�÷��̾ ȭ�� ������ �����ٸ� ó������
        if(transform.position.y < -10)
        {
            // ���� ������ �ʱ�ȭ
            Fruit.ResetFruitCount();  // ���� ���� �ʱ�ȭ
            SceneManager.LoadScene("GameScene");
        }

        /*
         * �ִϸ��̼� ����ӵ��� �÷��̾� �̵� �ӵ��� ����ϵ��� ����
         *  �÷��̾� �̵��ӵ��� 0�̸� �ִϸ��̼� �̵��ӵ��� 0���� �����ϰ�,
         *  �÷��̾� �̵��ӵ��� ���������� �ִϸ��̼� �ӵ��� ������
         *  �ִϸ��̼� ����ӵ��� �ٲٷ��� ������Ʈ�� speed ���������� ����
         */
        //m_animatorCat.speed = fPlayerMoveSpeed / 1.0f ;

        if(m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 2.0f;
        }
        else 
        {
            m_animatorCat.speed = 1.0f;
        }

        //�ּ�,�ִ밪�� �����Ͽ� ������ ���� �̿��� ���� ���� �ʵ��� �� �� �����
        //�÷��̾ ������ �� �ִ� �ּ�(fMinPositionX)/�ִ�(fMaxPositionX) �������� �����Ͽ� �� ������ ����� �ʵ��� ��.
        fPositionX = Mathf.Clamp(transform.position.x, fminPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

    }


    // �÷��̾ ��߿� ������ ������ �����
    // �� ��� ���� ������ Ŭ���� ������ ��ȯ�Ǿ�� ��
    //. �÷��̾ ��߿� ��Ҵ����� OnTriggerEnter2D �޼���� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))                       //Goal(���) �±װ� ����� ��
        {
            if (Fruit.fruitCount <= 0)                          //������ ������ 0�����̸�,
            {
                SceneManager.LoadScene("ClearScene");           //ClearScene ���
            }
        }
    }


    
}
