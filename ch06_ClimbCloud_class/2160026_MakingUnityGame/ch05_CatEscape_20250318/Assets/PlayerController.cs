using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* [�������� �˷��ֽ� ���]
     * float fMaxPositionX = 10.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� vector3�� �ִ밪�� ����
     * float fMinPositionX = 10.0f; // �÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� vector3�� �ּҰ��� ����
     * float fPositionX = 0.0f; // �÷��̾ ��, �� �̵��� �� �ִ� x��ǥ�� ��
     */


    //Start �޼ҵ�
    //    �̸� ���ǵ� Ư�� �̺�Ʈ �Լ��ν�, ��Ư�� �Լ����� c# ������ �Լ��� �޼ҵ�� ��
    //    MonoBehaviour Ŭ������ �ʱ�ȭ �� ��, ȣ�� �Ǵ� �̺�Ʈ �Լ�
    //    ���α׷��� ������ �� �� ���� ȣ���� �Ǵ� �Լ��� ���� ������Ʈ�� �޾ƿ��ų� ������Ʈ�� �ٸ��Լ����� ����ϱ� ���� �ʱ�ȭ ���ִ� ���
    //    ��, Start() �޼ҵ�� ��ũ��Ʈ �ν��Ͻ��� Ȱ��ȭ�� ��쿡�� ù ���� ������ ������Ʈ ���� ȣ���ϹǷ� �ѹ��� ����
    //    �� ���¿� ���Ե� ��� ������Ʈ�� ���� Update �� ������ ȣ��� ��� ��ũ��Ʈ�� ���� Start �Լ��� ȣ��
    //    ���� ���� �÷��� ���� ������Ʈ�� �ν��Ͻ�ȭ�� ���� ������� ����
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�, � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��, ����Ʈ���� 60, ����� pc�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
        // �����ӷ���Ʈ�� 60���� ����
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        // ȭ�� ������ �÷��̾ ������ �ʵ����ϴ� �ڵ�
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // Camera.main : ������Ʈ�� ó�� �������� ���� ����ī�޶� �ǹ�, WorldToViewportPoint : ���� ��ǥ�� ����Ʈ ��ǥ�� ��ȯ, ����Ʈ�� ����ϴ� ������ ȭ�� ũ�⿡ ���� ��ǥ�� �޶����� ������ ���� ��ǥ�� ����ȭ�Ͽ� ���
        // �� ��Ʈ�� ��ǥ��    0���� 1 ������ ���� ������, 0�� ���� ��, 1�� ������ ���� �ǹ�, ���ϵ� ���������� 0�� �Ʒ��� ��, 1�� ���� ���� �ǹ�
        if (pos.x < 0f) // x��ǥ�� 0���� ������
        {
            pos.x = 0f; // x��ǥ�� 0����
        }

        if (pos.x > 1f) // x��ǥ�� 1���� ũ��
        {
            pos.x = 1f; // x��ǥ�� 1��
        }

        if (pos.y < 0f) // y��ǥ�� 0���� ������
        {
            pos.y = 0f; // y��ǥ�� 0����
        }

        if (pos.y > 1f) // y��ǥ�� 1���� ũ��
        {
            pos.y = 1f; //  y��ǥ�� 1��
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos); // ����Ʈ ��ǥ�� ���� ��ǥ�� ��ȯ

        /* Ű�� ���ȴ��� �����ϱ� ���ؼ��� Input Ŭ������ GetKeyDown �޼ҵ带 �����
         * �� �޼ҵ�� �Ű������� ������ Ű�� ������ ���� true�� �� �� ��ȯ
         * GetKeyDown �޼ҵ�� ���ݱ��� ����ϴ� GetMouseButtonDown �޼ҵ�� ����ϹǷ� ���� ������ �� ���� ���̴�.
         * Ű�� ���� ���� : GetKeyDown()
         * Ű�� ������ �ִ� ���� : GetKey()
         * Ű�� �����ٰ� �� �� ���� : GetKeyUp()
        */
        //���� ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
            transform.Translate(-3, 0, 0);
        }
        //������ ȭ��ǥ Ű�� ������ �� -> GetKeyDown(KeyCode.RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0);
        }

        /* [�������� �˷��ֽ� ���]
         * fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX); // Clamp�� �ּҰ��� �ִ밪�� �����Ͽ� �� ������ ������ ��ȯ�ϴ� �޼ҵ�
         * �÷��̾ ������ �� �ִ� �ּ�/�ִ� �������� �����Ͽ� �� ������ ����� �ʵ��� �Ѵ�.  
         * transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
         */
    }
    public void LButtonDown() // ���� ��ư
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButtonDown() // ������ ��ư
    {
        transform.Translate(3, 0, 0);
    }
}
