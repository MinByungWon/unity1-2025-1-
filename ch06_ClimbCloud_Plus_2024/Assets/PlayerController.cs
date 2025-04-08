using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene�� ����ϱ� ���ؼ��� SceneManagement�� �������ؾ��մϴ�.
using UnityEngine.UI;               // UI ������Ʈ�� �����ϱ� ���ؼ��� UnityEngine.UI�� ����Ʈ�ؾ� �մϴ�.

public class PlayerController : MonoBehaviour
{
    // Cat ������Ʈ�� Rigidbody2D ������Ʈ�� ���� �������(m_)
    [SerializeField]
    Rigidbody2D m_rigid2DCat;

    // �÷��̾� �̵� �ӵ��� ���� �ִϸ��̼� ��� �ӵ��� ����ϱ� ���� �ִϸ����� ���� ����
    [SerializeField]
    Animator m_animatorCat;

    // �÷��̾ ���� �� ���� ������ ����
    [SerializeField]
    float fJumpForce = 680.0f;

    // �÷��̾� ��,��� �����̴� ���ӵ�
    [SerializeField]
    float fWalkForce = 20.0f;

    // �÷��̾� ��,��� �����̴� �ӵ�
    [SerializeField]
    float fPlayerMoveSpeed = 0.0f;

    // �÷��̾��� �̵� �ӵ��� ������ �ְ� �ӵ�
    [SerializeField]
    float fMaxWalkSpeed = 1.0f;

    // �÷��̾� �¿� ������ Ű �� : ������ ȭ�� Ű -> 1, ���� ȭ�� Ű -> -1, �������� ���� �� -> 0
    [SerializeField]
    int nLeftRightKeyValue = 0;

    Vector2 vecPreviousPosition = Vector2.zero;

    // Score ����� ���� ��� ����
    [SerializeField]
    int m_nScore = 0;

    // Score�� ���� �ֱ� ���� text 
    Text m_textScore = null;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ������Ʈ�� ���� �޼��带 ����ϱ� ������ Start �޼��忡�� GetComponent �޼��带 �����
        //   Rigidbody2D ���۳�Ʈ�� ���� ��� ������ ����
        m_rigid2DCat = GetComponent<Rigidbody2D>();

        // GetComponent �޼��带 ����� Animator ���۳�Ʈ�� ����
        m_animatorCat = GetComponent<Animator>();

        // GetComponent �޼��带 ����� Text ���۳�Ʈ�� ����
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreObject != null)
        {
            m_textScore = scoreObject.GetComponent<Text>();
            m_textScore.text = string.Format("Score : {0}", m_nScore.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spacebar Key�� ������ AddForce �޼��带 ����� ���� �������� ������ �÷��̾ ���� ���Ѵ�.
        //   ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
        //   �÷��̾ �����ϴ� ���߿� �����ϴ� ���� �����ϱ� ���ؼ� m_rigid2DCat.velocity.y == 0 �߰�
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
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            nLeftRightKeyValue = -1;
        }
        else
        {
            nLeftRightKeyValue = 0;
        }

        // �÷��̾��� �̵� �ӵ�
        fPlayerMoveSpeed = Mathf.Abs(m_rigid2DCat.linearVelocity.x);

        // �÷��̾��� ���ǵ� ����
        //   �����Ӹ� AddForce �޼��带 ����� �������ϸ� �÷��̾ ��� ������ �Ǵ� ������ �߻�
        //   �׷��� �÷��̾��� �̵� �ӵ��� ������ �ְ��ӵ�(fMaxWalkSpeed) ���� ������ ���� ���ϴ� ���� ���߰� �ӵ��� ����
        // ����ȭ��ǥ, ������ȭ��ǥ Key�� ������ AddForce �޼��带 ����� ��, �� �������� ������ �÷��̾ ���� ���Ѵ�.
        // ��, �÷��̾ ���� ���Ϸ��� Rigidbody2D ������Ʈ�� ���� AddForce �޼��带 ����Ѵ�.
        if (fPlayerMoveSpeed < fMaxWalkSpeed)
        {
            m_rigid2DCat.AddForce(transform.right * nLeftRightKeyValue * fWalkForce);
        }

        // �����̴� ���⿡ ���� �÷��̾� �̹����� ����
        //   �÷��̾ ���������� �����̸� ��Ʈ����Ʈ�� ���������� ���ϰ�,
        //   �������� �����̸� �������� �����̵��� �̹����� ����������,
        //   ��������Ʈ�� X�� ��������� -1��� ��
        //   ��������Ʈ�� ������ �ٲٷ��� transform ���۳�Ʈ�� localScale ���� ���� ����
        //   ������ȭ��ǥ�� 1��, ����ȭ��ǥ�� X�� �������� -1��
        if (nLeftRightKeyValue != 0)
        {
            transform.localScale = new Vector3(nLeftRightKeyValue, 1, 1);
        }

        // �ִϸ��̼� ��� �ӵ��� �÷��̾� �̵� �ӵ��� ���
        //   �÷��̾� �̵� �ӵ��� 0�̸� �ִϸ��̼� �̵� �ӵ��� 0���� �����ϰ�,
        //   �÷��̾� �̵� �ӵ��� ���������� �ִϸ��̼� �ӵ��� ������
        // �ִϸ��̼� ��� �ӵ��� �ٲٷ��� ���۳�Ʈ�� speed ���������� ����
        if (m_rigid2DCat.linearVelocity.y == 0)
        {
            m_animatorCat.speed = fPlayerMoveSpeed / 1.0f;
        }
        else
        {
            m_animatorCat.speed = 1.0f;
        }


        // �÷��̾ ȭ�� ������ ������ ������ �������� ���� ����
        //   �÷��̾��� Y ��ǥ�� -10 �̸��̸� ���� ó������ ���ư����� GameScene�� �ε�
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

    }

    // �����Ӹ��� ȣ��Ǵ� �Լ� : LateUpdate()
    private void LateUpdate()
    {
        //Vector.Distance(a, b)�� �̿��Ͽ� �Ÿ��� ���� �� �ֽ��ϴ�. �̶�, a�� b ������ �Ÿ��� ���ϴ� �޼���
        float fMoveResult = Vector2.Distance(vecPreviousPosition, transform.position);

        if(fMoveResult < 0.001f && nLeftRightKeyValue != 0)
        {
            Vector2 vecMoveTargetPosition = transform.position + (transform.right * nLeftRightKeyValue * 0.1f);

            RaycastHit2D hit = Physics2D.Raycast(vecMoveTargetPosition, Vector2.down);
            if(hit.collider != null)
            {
                transform.position = vecMoveTargetPosition;
            }
        }

        // (1��)�÷��̾� ȭ�� ������ ������ ������ �ذ�
        //      Clamp(Ŭ����) : �ּ� / �ִ밪 �� �����Ͽ� float ���� ���� �̿��� ���� ���� �ʵ��� �մϴ�.
        Vector2 vecMoveResultPosition = transform.position;
        vecMoveResultPosition.x = Mathf.Clamp(vecMoveResultPosition.x, -2.7f, 2.7f);
        transform.position = vecMoveResultPosition;

        // (2��)�÷��̾� ȭ�� ������ ������ ������ �ذ�
        //      Clamp(Ŭ����) : �ּ� / �ִ밪 �� �����Ͽ� float ���� ���� �̿��� ���� ���� �ʵ��� �մϴ�.
        //float fPositionX = transform.position.x;
        //fPositionX = Mathf.Clamp(fPositionX, -2.7f, 2.7f);
        //transform.position = new Vector2(fPositionX, transform.position.y);

        vecPreviousPosition = transform.position;
    }

    // �÷��̾ ��߿� ������ ������ �����
    //   �� ������ ������ Ŭ���� ������ ��ȯ�Ǿ�� ��
    //   �÷��̾ ��߿� ��Ҵ����� OnTriggerEnter2D �޼���� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("flag"))
        {
            // Debug.Log("��!!");
            SceneManager.LoadScene("ClearScene");
        }
        else if(collision.CompareTag("Item"))
        {

            if(collision.GetComponent<Item>() != null)
            {
                //���� ȹ��
                //Debug.Log("������ ȹ�� " + collision.GetComponent<Item>().GetScore() + " ��");
                m_nScore += collision.GetComponent<Item>().GetScore();
                m_textScore.text = string.Format("Score : {0}", m_nScore.ToString());
            }

            Destroy(collision.gameObject);
        }
        else
        {

        }
    }
}
