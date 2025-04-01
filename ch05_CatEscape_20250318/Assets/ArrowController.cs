
// ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� --> transform.Translate()
// ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� --> Destroy()

using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // ������� ����
    GameObject gPlayer = null; // Player Object�� ������ GameObject ����, GameObject ������ �ʱ갪�� null

    Vector2 vArrowCirclePoint = Vector2.zero;    // ȭ�츦 �ѷ��� ���� �߽� ��ǥ
    Vector2 vPlayerCirclePoint = Vector2.zero;   // �÷��̾ �ѷ��� ���� �߽�
    Vector2 vArrowPlayerDir = Vector2.zero;      // ȭ�쿡�� �÷��̾������ ���Ͱ�

    float fArrowRadius = 0.5f;          // ȭ�� ���� ������ 0.5
    float fPlayerRadius = 1.0f;         // �÷��̾� ���� ������ 1.0
    float fArrowPlayerDistance = 0.0f;  // ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)���� �Ÿ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * �� �ȿ��� ������Ʈ�� ã�� �޼��� : Find
         *    Find �޼���� ������Ʈ �̸��� �μ��� �����ϰ�,�μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         *    �÷��̾��� ��ǥ�� ���ϱ� ���ؼ� �÷��̾ �˻��Ͽ� ������Ʈ ������ ����
         *    �� ������Ʈ ���ڿ� �����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־�� ��
        */
        gPlayer = GameObject.Find("player");

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� --> transform.Translate()
         * Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
         *    Y ��ǥ�� -0.1f�� �����ϸ� ������Ʈ�� ���ݾ� ������ �Ʒ��� �����δ�
         *    �����Ӹ��� ������� ���Ͻ�Ų��.
        */
        transform.Translate(0, -0.1f, 0);


        /*
         * ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� --> Destroy()
         * ȭ�� ������ ���� ȭ�� �Ҹ��Ű��
         *   ȭ���� ������ �θ� ȭ�� ������ ������ �ǰ�, ���� �������� ������ ��� ������
         *   ȭ���� ������ �ʴ� ������ ��� �������� ��ǻ�� ���� ����� �ؾ��ϹǷ� �޸� ����
         *   �޸𸮰� ������� �ʵ��� ȭ���� ȭ�� ������ ������ ������Ʈ�� �Ҹ���Ѿ� ��
         *   Destroy �޼��� : �Ű������� ������ ������Ʈ�� ����
         *   �Ű������� �ڽ�(ȭ�� ������Ʈ)�� ����Ű�� gameObject ������ �����ϹǷ� ȭ����
         *   ȭ�� ������ ������ �� �ڱ� �ڽ��� �Ҹ��Ŵ
        */
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        /*
         * �浹���� : ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ���� �˰���
         * ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)���� �Ÿ�(fArrowPlayerDistance)�� ��Ÿ��� ������ �̿��Ͽ� ���Ѵ�.
         * fArrowRadius : ȭ�츦 �ѷ��� ���� ������, fPlayerRadius : �÷��̾ �ѷ��� ���� ������
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance > fArrowRadius + fPlayerRadius : �浹���� ����
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance < fArrowRadius + fPlayerRadius : �浹��
        */
        vArrowCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vArrowPlayerDir = vArrowCirclePoint - vPlayerCirclePoint;

        /*
         * �� ���Ͱ��� ���̸� ���ϴ� �޼��� : magnitude
         *  - �޼��� ���� : public float Magnitude(Vector3 vector);
         *  - ���ʹ� ũ��� ������ ���� ������, ������(Initial Point)�� ����(Terminal Point)���� �����Ǹ�, 
         *      �� �� ������ �Ÿ��� �� ������ ũ�Ⱑ �ȴ�.
         *  - �Ϲ������� �������� ������ ����, ������ ������ �Ӹ���� �θ���.
         *  - ���ʹ� �������� ������ ��ġ�� ���� ����, �� ������ ũ��� ������ ���ٸ� ���� ���� ���ͷ� ����Ѵ�.
         *  - ���ʹ� ���� ��ġ�� ��Ÿ���� ��ġ ����(Position Vector)�� �̿��� ǥ���Ѵ�.
         *  - ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)������ �Ÿ�  
        */
        fArrowPlayerDistance = vArrowPlayerDir.magnitude;

        /*
         * �÷��̾ ȭ�쿡 �¾Ҵ��� ����, �� ȭ��� �÷��̾��� �浿 ����
         *  - ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ����
         *  - r1 : ȭ���� �ѷ��� ���� ������,  r2 : �÷��̾ �ѷ��� ���� ������, d : ȭ��� �߽ɿ��� �÷��̾�� �߽ɱ��� �Ÿ�
         *  - �浹 : �� ���� �߽� �� �Ÿ� d�� (r1 + r2)���� ������ �浹(d < r1+r2)
         *  - ���浹 : �� ���� �߽� �� �Ÿ� d�� (r1 + r2)���� ũ�� �� ���� �浹���� ����(d > r1+r2)
         *  - �浹(fArrowPlayerDistance < ( fArrowRadius + fPlayerRadius)) �̸� ȭ�� ������Ʈ �Ҹ�
        */
        if (fArrowPlayerDistance < fArrowRadius + fPlayerRadius)
        {
            /*
             * �÷��̾ ȭ�쿡 ������ ȭ�� ��Ʈ�ѷο��� ���� ��ũ��Ʈ(GameDirector)�� f_DecreaseHp() �޼��带 ȣ��
             *   ��, ArrowController���� GameDirector ������Ʈ�� �ִ� f_DecreaseHp() �޼��带 ȣ���ϱ� ������
             *   Find �޼��带 ã�Ƽ� GameDirector ������Ʈ�� ã�´�.   
            */
            GameObject gDirector = GameObject.Find("GameDirector");

            /*
             * GetComponent �޼��带 ����� GameDirector ������Ʈ�� GameDirector ��ũ��Ʈ�� ���ϰ�,
             *   f_DecreaseHp() �޼��带 �����Ͽ�, ���� ��ũ��Ʈ�� �÷��̾�� ȭ���� �浹�ߴٰ� ����
            */
            gDirector.GetComponent<GameDirector>().f_DecreaseHp();

            Destroy(gameObject);  // ȭ��� �÷��̾� �浹, ȭ�� ������Ʈ�� �Ҹ�
        }

    }
}
