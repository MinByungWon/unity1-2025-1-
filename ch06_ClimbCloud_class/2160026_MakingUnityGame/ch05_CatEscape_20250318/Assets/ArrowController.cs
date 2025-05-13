// ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� --> transform.Translate()
// ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� -> Destroy()

using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // ������� ���� 
    GameObject gPlayer = null; // Player ������Ʈ�� ������ ����

    Vector2 vArrowCirclePoint = Vector2.zero;  // ȭ���� �߽� ��ǥ
    Vector2 vPlayerCirclePoint = Vector2.zero; // �÷��̾��� �߽� ��ǥ
    Vector2 vArrowPlayerDir = Vector2.zero; // ȭ��� �÷��̾��� �߽� �Ÿ�

    float fArrowRadius = 0.5f; // ȭ���� �ݰ�
    float fPlayerRadius = 1.0f; // �÷��̾��� �ݰ�
    float fArrowPlayerDistance = 0.0f; // ȭ��� �÷��̾��� �߽� �Ÿ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * ���ȿ� ������Ʈ�� ã�� �޼ҵ� : Find("������Ʈ �̸�");
         * Find �޼ҵ�� ������Ʈ �̸��� �μ��� �����ϰ� �μ��̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * �÷��̾��� ��ǥ�� ���ϱ� ���ؼ� �÷��̾� ������Ʈ�� ã�ƾ���
         * 
         */
        this.gPlayer = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� --> transform.Translate()
         * Translate �޼ҵ� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼ҵ�
         * Y��ǥ���� -0.1f�� �����ϸ� ������Ʈ�� ���ݾ� ������ �Ʒ��� �����δ�
         * �����Ӹ��� ������� ���Ͻ�Ų��.
         */
        transform.Translate(0, -0.1f, 0);
        /*
         * ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� -> Destroy()
         * ȭ�� ������ ���� ȭ�� �Ҹ��Ű��
         * ȭ���� ������ �θ� ȭ�� ������ ������ �ǰ� ���� �������� ������ ��� ������
         * ȭ���� ������ �ʴ� ������ ��� �������� ��ǻ�� ���� ����� �ؾ��ϹǷ� �޸� ����
         * �޸𸮰� ������� �ʵ��� ȭ���� ȭ�� ������ ������ ������Ʈ�� �Ҹ���Ѿ���
         * Destroy �޼ҵ� : �Ű������� ������ ������Ʈ�� ����
         * �Ű������� �ڽ��� ����Ű�� GameObject ������ �����ϹǷ� ȭ����
         * ȭ�� ������ ���� �� �ڱ� �ڽ��� �Ҹ� ��Ŵ
         */
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }


        //�浹 ���� : ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ���� �˰���
        /*
         * ȭ���� �߽ɺ��� �÷��̾ �ѷ��� ���� �߽ɱ��� �Ÿ��� ��Ÿ��� ������ �̿��Ͽ� ���Ѵ�.
         * fArrowRadius : ȭ���� �ѷ��� ���� ������ fPlayerRadius : �÷��̾ �ѷ��� ���� ������
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance�� �� ���� �������� �պ��� ũ�� �浹���� ������ ����
         * �ο��� �߽ɰ��� �Ÿ� fArrowPlayerDistance�� fArrowRadius+ fPlayerRadius���� ������ �浹�� ������ ����
         */
        vArrowCirclePoint = transform.position;                                   // ȭ���� �߽� ��ǥ
        vPlayerCirclePoint = this.gPlayer.transform.position;                     // �÷��̾��� �߽� ��ǥ
        vArrowPlayerDir = vArrowCirclePoint - vPlayerCirclePoint;

        // �� ���Ͱ��� ���̸� ���ϴ� �޼ҵ� : magnitude
        /*
         * �޼ҵ� ���� : public float magnitude(Vector3 vector);
         * ���ʹ� ũ��� ������ ���� ������, �������� �������� ������.
         * �̵� ������ �Ÿ��� �� ������ ũ�Ⱑ �ȴ�.
         * �Ϲ������� �������� ������ ���� ������ ������ �Ӹ���� �θ���
         * ���ʹ� �������� ������ ��ġ ������� �� ������ ũ��� ������ ���ٸ� ���� ���� ���ͷ� ����Ѵ�.
         * ���ʹ� ���� ��ġ�� ��Ÿ���� ��ġ ���͸� �̿��� ǥ���Ѵ�.
         * ȭ���� �߽ɺ��� �÷��̾ �ѷ��� ���� �߽ɱ����� �Ÿ�
         */
        fArrowPlayerDistance = vArrowPlayerDir.magnitude;                         // �÷��̾�� ȭ���� �Ÿ�

        /*
         * �÷��̾ ȭ�쿡 �¾Ҵ��� ����, �� ȭ��� �÷��̾��� �浹 ����
         * ���� �߽� ��ǥ�� �ݰ��� ����� �浹����
         * r1 :ȭ���� �ѷ��� ���� ������ r2 : �÷��̾ �ѷ��� ���� ������ d : �� ���� �߽ɰ��� �Ÿ�
         * �浹 : �� ���� �߽ɰ� �Ÿ�d�� �� ���� �������� �պ��� ������ �浹�� ������ ����
         * ���浹 : �� ���� �߽ɰ� �Ÿ�d�� �� ���� �������� �պ��� ũ�� �浹���� ���� ������ ����
         * �浹 < fArrowRadius + fPlayerRadius �̸� ȭ�� ������Ʈ �Ҹ�
         */
        if (fArrowPlayerDistance < fArrowRadius + fPlayerRadius)
        {
            // �浹�� ��� ȭ���� �����.
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
            Destroy(gameObject) ;
        }
    }
    
}
