using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ�� ������ �� �ִ� ��Ʈ�ѷ� ��ũ��Ʈ : Ű�� �����ؼ� �÷��̾� �����̱�
//   �߰� --> ��, �÷��̾ ����â�� ����� �ʵ��� Vector �ּڰ�, �ִ� �����ϱ�
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float fMaxPositionX;

    [SerializeField]
    float fMinPositionX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ArrowGenerator ���������� ���� ����� return GetIsRun()�Լ� ȣ��
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // GetKeyDown �޼��� : Ű�� �������� �����ϴ� �޼���
                // ���� ȭ��ǥ Ű�� ������ ��
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
                    transform.Translate(-3, 0, 0);  // �������� '3' �����δ�.
                }

                // ������ ȭ��ǥ Ű�� ������ ��
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Translate(3, 0, 0);  // ���������� '3' �����δ�.
                }

                // Mathf Clamp() : �ּ�/�ִ밪�� �����Ͽ� ������ ���� �̿ܿ� ���� ���� �ʵ��� �� �� ���
                // �÷��̾ ������ �� �ִ� �ּ� / �ִ� �������� �����Ͽ� �� ������ ����� �ʵ����Ѵ�.
                float fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
                transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
            }
        }
    }

    // ���� ��ư�� ������ �� �÷��̾ �������� �̵���Ű�� �޼���
    public void LButtonDown()
    {
        // ArrowGenerator ���������� ���� ����� return GetIsRun()�Լ� ȣ��
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
                transform.Translate(-3, 0, 0);  // �������� '3' �����δ�.
            }
        }
    }

    // ������ ��ư�� ������ �� �÷��̾ ���������� �̵���Ű�� �޼���
    public void RButtonDown()
    {
        // ArrowGenerator ���������� ���� ����� return GetIsRun()�Լ� ȣ��
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                transform.Translate(3, 0, 0);  // ���������� '3' �����δ�.
            }
        }
    }


}
