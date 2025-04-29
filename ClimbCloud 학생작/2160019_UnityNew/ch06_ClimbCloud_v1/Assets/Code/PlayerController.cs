using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cat ������Ʈ�� Rigidbody2D ������Ʈ�� ���� �������(m_)
    Rigidbody2D m_rigid2DCat = null;

    Animator m_animator = null;

    GameObject m_cat = null;

    //Restart���� �� üũ ����Ʈ ������ ����
    Vector2 currentCheckPoint = new Vector3(0, 0);

    // �÷��̾ ���� �� ���� ������ ����
    float fJumpForce = 680.0f;

    // �÷��̾� ��,��� �����̴� ���ӵ�
    float fwalkForce = 30.0f;

    // �÷��̾��� �̵� �ӵ��� ������ �ְ� �ӵ�
    float fmaxWalkSpeed = 2.0f; 
    float fthreshold = 0.2f;

    // ��� ���� ����
    float fMaxPositionX = 2.0f; //�÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ밪 ���� ���� 
    float fMinPositionX = -2.0f; //�÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ּҰ� ���� ���� 
    float fPositionX = 0.0f;    //�÷��̾ ��, �� �̵��� �� �ִ� X ��ǥ ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;    
        m_rigid2DCat = GetComponent<Rigidbody2D>();
        this.m_animator = GetComponent<Animator>();

        this.m_cat = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        /* [ AddForce �޼��� : ������Ʈ�� ������ ���� �־� �̵���Ű�� �� ]
         * Spacebar Key�� ������(GetKeyDown �޼���) AddForce �޼��带 ����� ���� �������� ������ �÷��̾ ���� ���Ѵ�.
         * ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
         */

        //üũ����Ʈ�� �̵�
        CheckPoint();

        // �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2DCat.linearVelocity.y == 0)
        {
            this.m_animator.SetTrigger("JumpTrigger");
            m_rigid2DCat.AddForce(transform.up * fJumpForce);
        }

        if (Input.GetMouseButtonDown(0) && m_rigid2DCat.linearVelocity.y == 0)
        {
            m_rigid2DCat.AddForce(transform.up * this.fJumpForce);
        }

        // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű -> 1, ���� ȭ�� Ű -> -1, �������� ���� �� -> 0
        int nLeftRightKeyValue = 0;

        // �÷��̾� ��,�� �̵�
        // �÷��̾� �¿� ������ Ű �� : RightArrow -> 1, LeftArrow -> -1
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nLeftRightKeyValue = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }

        if(Input.acceleration.x > this.fthreshold)
        {
            nLeftRightKeyValue = 1;
        }
        if(Input.acceleration.x < -this.fthreshold)
        {
            nLeftRightKeyValue = -1;
        }

        // �÷��̾��� x���� �ִ�� �ּҸ� �����ʰ� ����.
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, 0);

        //m_rigid2DCat.AddForce(transform.right = fWalkForce + nLefrRightKeyValue);
        // �÷��̾� �¿� �����̴� �ӵ�
        float fPlayerMoveSpeed = Mathf.Abs(this.m_rigid2DCat.linearVelocity.x);

        if (fPlayerMoveSpeed < fmaxWalkSpeed) { 
            this.m_rigid2DCat.AddForce(transform.right * nLeftRightKeyValue * this.fwalkForce);
        }

        // �����̴� ���⿡ ���� �����Ѵ�.
        if(nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        /*
         * �ִϸ��̼� ��� �ӵ��� �÷��̾� �̵� �ӵ��� ����ϵ��� ����
         * �÷��̾� �̵� �ӵ��� ���������� �ִϸ��̼� �ӵ��� ������
         * �ִϸ��̼� ��� �ӵ��� �ٲٷ��� ���۳�Ʈ�� speed ���������� ����
         */

        //m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;

        //�÷��̾� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�.
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            this.m_animator.speed = fPlayerMoveSpeed / 2.0f;
        }
        else
        {
            this.m_animator.speed = 1.0f;
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene1");
        }
    }

    // �� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        // tag�� 1�� ��߿� ����
        if (other.tag == "flag1")
        {
            // ���� üũ����Ʈ ��ġ�� 1, 18, 0���� ��ȯ
            currentCheckPoint = new Vector2(1, 18);
        }
        // tag�� 2�� ��߿� ����
        else if (other.tag == "flag2")
        {
            currentCheckPoint = new Vector2(0, 0);
            Debug.Log("��");
            SceneManager.LoadScene("ClearScene");
        }
    }

    void CheckPoint()
    {
        // R�� ������ cat�� ��ġ�� üũ����Ʈ�� �̵�
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_cat.transform.position = currentCheckPoint;
            Debug.Log("�̵�");
        }
    }
}
