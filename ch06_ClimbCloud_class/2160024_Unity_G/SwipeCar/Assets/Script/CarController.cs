using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float speed = 0;
    //car ������Ʈ�� �ӵ��� �����ϴ� ����
    Vector2 startPos;
    //���������� ���� ������ �����ϴ� ����


    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        //Input.GetMouseButtonDown(0)�� ���콺 ���� ��ư�� ������ �� true�� ��ȯ :: ������ �������� ���� �ǹ���

        {
            this.startPos = Input.mousePosition;
            //Input.mousePosition�� ���콺�� ���� ��ġ�� ��ȯ :: ���콺�� ���� ��ġ�� this.startPos�� ����
        }
        else if(Input.GetMouseButtonUp(0))
        //Input.GetMouseButtonUp(0)�� ���콺 ���� ��ư�� ���� �� true�� ��ȯ :: ��ư�� ���� ���� ���� �ǹ���
        {
            Vector2 endPos = Input.mousePosition;
            //Input.mousePosition�� ���콺�� ���� ��ġ�� ��ȯ :: ���콺�� ���� ��ġ�� endPos�� ����

            float swipeLength = endPos.x - this.startPos.x;
            //�̵� �Ÿ��� ��Ÿ���� swipeLength ������ endPos.x - this.startPos.x�� ���� :: x������ �̵��� �Ÿ��� ��Ÿ��

            this.speed = swipeLength / 500.0f;
            //swipeLength�� 500���� ���� ���� speed�� ���� :: �ӵ��� ��Ÿ��

            GetComponent<AudioSource>().Play();
            //����� �ҽ� �÷��� �ڵ�

        }
            transform.Translate(this.speed, 0, 0);
        //transform.Translate(x, y, z)�� �ش� ������Ʈ�� x, y, z��ŭ �̵���Ű�� �Լ� :: x������ speed��ŭ �̵�

        this.speed *= 0.98f;
        //�ӵ��� ���ӽ�Ű�� �ڵ�
    }
}
