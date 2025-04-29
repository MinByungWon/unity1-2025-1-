using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //��� ���� ����
    float fMaxPositionX = 7.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ� ���� ����
    float fminPositionX = -7.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ּڰ� ���� ����
    float fPositionX = 0.0f;    // �÷��̾ ��, �� �̵��� �� �ִ� X ��ǥ ���� ����
    /*
    * Start �޼���
    * �̸� ���ǵ� Ư�� �̺�Ʈ �Լ�(�޼ҵ�)�ν�, �� Ư�� �Լ����� C#������ �Լ��� �޼ҵ��� ��
    * MonoBehaviour Ŭ������ �ʱ�ȭ �� �� ȣ��Ǵ� �̺�Ʈ �Լ�
    * ���α׷��� ������ �� �ѹ��� ȣ���� �Ǵ� �Լ��� ���� ������Ʈ�� �޾ƿ��ų� ������Ʈ�� �ٸ� �Լ����� ����ϱ� ���� �ʱ�ȭ ���ִ� ���
    * ��, Start() �޼���� ��ũ��Ʈ �ν��Ͻ��� Ȱ��ȭ�� ��쿡�� ù ��° ������ ������Ʈ ���� ȣ���ϹǷ� �ѹ��� ����
    * �� ���¿� ���Ե� ��� ������Ʈ�� ���� Update �� ������ ȣ��� ��� ��ũ��Ʈ�� ���� Start �Լ��� ȣ��
    * ���� �����÷��� ���� ������Ʈ�� �ν��Ͻ�ȭ �ɶ��� ������� ����
    */
    void Start()
    {
        /*����̽� ������ ���� ���� ����� ���� ���ֱ�
         * � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         * ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         * �����ӷ���Ʈ�� 60���� ����
         */

        Application.targetFrameRate = 60;
    }
    
    public void LButtonDown()
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButtonDown()
    {
        transform.Translate(3, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        * Ű�� �������� �����ϱ� ���ؼ��� Input Ŭ������ GetKeyDown �޼��带 �����
        * �� �޼���� �Ű������� ������ Ű�� ������ ���� true�� �ѹ� ��ȯ
        * GetKeyDown �޼���� ���ݱ��� ����ϴ� GetMouseButtonDown �޼���� ����ϹǷ� ���� ������ �� ���� ��
        * Ű�� ���� ���� : GetKeyDown()
        * Ű�� ������ �ִ� ���� : GetKey()
        * Ű�� �����ٰ� �� ���� : GetKeyUp()
        */

        // ���� ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
            // ���� -3�̹Ƿ� �������� '3'��ŭ �����δ�.
            transform.Translate(-3, 0, 0);

        }

        // ������ ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
            // ���� 3�̹Ƿ� ���������� '3'��ŭ �����δ�.
            transform.Translate(3, 0, 0);

        }

        /*  Mathf.clamp(value, min, max) �޼���
         *  Ư�� ���� ��� ������ ���ѽ�Ű���� �� �� ����ϴ� �޼���
         *  value ���� ���� : min <= value <= max
         *  �ּ�/�ִ밪�� �����Ͽ� ������ ���� �̿��� ���� ���� �ʵ��� �� �� ���
         *  �÷��̾ ������ �� �ִ� �ּ�(fMinPositionX)/�ִ�(fMaxPositionX) �������� �����Ͽ� �� ������ ����� �ʵ��� �Ѵ�.
         */

        fPositionX = Mathf.Clamp(transform.position.x, fminPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
    }
}
