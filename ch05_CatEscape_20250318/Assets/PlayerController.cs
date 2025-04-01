using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��� ���� ����
    float fMaxPositionX = 10.0f;   // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ� ���� ����
    float fMinPositionX = -10.0f;  // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ּڰ� ���� ����
    float fPositionX = 0.0f;       // �÷��̾ ��, �� �̵��� �� �ִ� X ��ǥ ���� ����

    /*
     * Start �޼ҵ�
     *   �̸� ���ǵ� Ư�� �̺�Ʈ �Լ�(�޼ҵ�)�ν�, �� Ư�� �Լ����� C#������ �Լ��� �޼ҵ��� ��
     *   MonoBehaviour Ŭ������ �ʱ�ȭ �� �� ȣ�� �Ǵ� �̺�Ʈ �Լ�
     *   ���α׷���  ������ �� �� ���� ȣ���� �Ǵ� �Լ��� ���� ������Ʈ�� �޾ƿ��ų� ������Ʈ�� �ٸ� �Լ����� ����ϱ� ���� �ʱ�ȭ ���ִ� ���
     *   ��, Start() �޼���� ��ũ��Ʈ �ν��Ͻ��� Ȱ��ȭ�� ��쿡�� ù ��° ������ ������Ʈ ���� ȣ���ϹǷ� �ѹ��� ����
     *   �� ���¿� ���Ե� ��� ������Ʈ�� ���� Update �� ������ ȣ��� ��� ��ũ��Ʈ�� ���� Start �Լ��� ȣ��
     *   ���� �����÷��� ���� ������Ʈ�� �ν��Ͻ�ȭ�� ���� ������� ����
   */
    void Start()
    {
        /* 
         * ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
         *   � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         *   ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         *   �����ӷ���Ʈ�� 60���� ����
        */
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         *  Ű�� �������� �����ϱ� ���ؼ��� Input Ŭ������ GetKeyDown �޼��带 �����
         *  �� �޼���� �Ű������� ������ Ű�� ������ ���� true�� �� �� ��ȯ
         *  GetKeyDown �޼���� ���ݱ��� ����ϴ� GetMouseButtonDown �޼���� ����ϹǷ� ���� ������ �� ���� ��
         *  Ű�� ���� ���� : GetKeyDown()
         *  Ű�� ������ �ִ� ���� : GetKey()
         *  Ű�� �����ٰ� �� ���� : GetKeyUp()
        */

        // ���� ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
            // ���� -3 �̹Ƿ� �������� '3' ��ŭ �����δ�.
            transform.Translate(-3, 0, 0);
        }

        // ������ ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
            // ���� 3 �̹Ƿ� ���������� '3' ��ŭ �����δ�.
            transform.Translate(3, 0, 0);
        }

        /*
         * Mathf.Clamp(value, min, max) �޼���
         *   Ư�� ���� ��� ������ ���ѽ�Ű���� �� �� ����ϴ� �޼���
         *   value ���� ���� : min <= value <= max
         *   �ּ�/�ִ밪�� �����Ͽ� ������ ���� �̿ܿ� ���� ���� �ʵ��� �� �� ���
         *   �÷��̾ ������ �� �ִ� �ּ�(fMinPositionX)/�ִ�(fMaxPositionX) �������� �����Ͽ� �� ������ ����� �ʵ����Ѵ�.
        */
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

    }
}
