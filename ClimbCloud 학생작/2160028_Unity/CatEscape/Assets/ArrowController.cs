using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrowController : MonoBehaviour
{
    //������� ����
    GameObject gPlayer = null;  // Player Object�� ������ GameObject ����, GameObject ���� �ʱⰪ�� null

    Vector2 vArrowCirclePoint = Vector2.zero;   // ȭ���� �ѷ��� ���� �߽� ��ǥ
    Vector2 vPlayerCirclePoint = Vector2.zero;  //�÷��̾ �ѷ��� ���� �߽�
    Vector2 vArrowPlayerDir = Vector2.zero;     //ȭ�쿡�� �÷��̾������ ���Ͱ�

    float fArrowRadius = 0.5f;                  //ȭ�� ���� ������ 0.5
    float fPlayerRadius = 1.0f;                 //�÷��̾��� ������ 1.0
    float fArrowPlayerDistance = 0.0f;          //ȭ���� �߽ɺ��� �÷��̾ �ѷ��� ���� �߽ɱ����ǰŸ�

    void Start()
    {
        /* �� �ȿ��� ������Ʈ�� ã�� �޼��� : Find
         * Find �޼���� ������Ʈ �̸��� �μ��� �����ϰ� �μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * �÷��̾��� ��ǥ�� ���ϱ� ���ؼ� �÷��̾ �˻��Ͽ� ������Ʈ ������ ����
         * �� ������Ʈ ���ڿ� �����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־����
         */

        gPlayer = GameObject.Find("player");
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� --> transform.Translate()
         * Translate �޼��� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼���
         *  Y ��ǥ�� -0.1f�� �����ϸ� ������Ʈ�� ���ݾ� ������ �Ʒ��� �����δ�
         *  ������ ���� ������� ���Ͻ�Ų��.
         */
        transform.Translate(0, -5.0f * Time.deltaTime, 0);

        /*
         * ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� --> Destory()
         * ȭ�� ������ ���� ȭ�� �Ҹ��Ű��
         *   ȭ���� �������θ� ȭ�� ������ ������ �ǰ�, ���� �������� ������ ��� ������
         *   ȭ���� ������ �ʴ� ������ ��� �������� ��ǻ�� ���� ����� �ؾ��ϹǷ� �޸� ����
         *   �޸𸮰� ������� �ʵ��� ȭ���� ȭ�� ������ ������ ������Ʈ�� �Ҹ���Ѿ���
         *   Destory �޼��� : �Ű������� ������ ������Ʈ�� ����
         *   �Ű������� �ڽ�(ȭ�� ������Ʈ)�� ����Ű�� gameobject ������ �����ϹǷ� ȭ����
         *   ȭ�� ������ ������ �� �ڱ� �ڽ��� �Ҹ��Ŵ
         */
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        /*
         * �浹���� : ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ���� �˰���
         * ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)���� �Ÿ�(fArrowPlayerDistance)�� ��Ÿ��������� �̿��Ͽ� ���Ѵ�.
         * fArrowRadius : ȭ���� �ѷ��� ���� ������, fPlayerRadius : �÷��̾ �ѷ��� ���� ������
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance > fArrowRadius + fPlayerRadius : �浹���� ����
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance < fArrowRadius + fPlayerRadius : �浹��
         */

        vArrowCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vArrowPlayerDir = vArrowCirclePoint - vPlayerCirclePoint;

        /*
         * �� ���� ���� ���̸� ���ϴ� �޼��� : magnitude
         * - �޼��� ���� : public float Magnitude(Vector3 vecter);
         * - ���ʹ� ũ��� ������ ���� ������, ������(Initial Point)�� ����(Terminal Point)���� �����Ǹ�, 
         *      �� �� ������ �Ÿ��� �� ������ ũ�Ⱑ �ȴ�.
         * - �Ϲ������� �������� ������ ����, ������ ������ �Ӹ���� �θ���.
         * - ���ʹ� �������� ������ ��ġ�� ���� ����, �� ������ ũ��� ������ ���ٸ� ���� ���� ���ͷ� ����Ѵ�.
         * - ���ʹ� ���� ��ġ�� ��Ÿ���� ��ġ ����(Position Vector)�� �̿��� ǥ���Ѵ�.
         * - ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)������ �Ÿ�
         */

        fArrowPlayerDistance = vArrowPlayerDir.magnitude;

        /*
         * �÷��̾ ȭ�쿡 �¾Ҵ��� ����, �� ȭ��� �÷��̾��� �浹 ����
         * - ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ����
         * - r1 : ȭ���� �ѷ��� ���� ������, r2 : �÷��̾ �ѷ��� ���� ������, d : ȭ��� �߽ɿ��� �÷��̾� �߽ɱ��� �Ÿ�
         * - �浹 : �� ���� �߽� �� �Ÿ� d�� (r1 + r2)���� ������ �浹 ( d > r1 + r2 )
         * - ���⵹ : �� ���� �߽� �� �Ÿ� d�� (r1 + r2_���� ũ�� �� ���� �浹���� ����(d > r1 + r2)
         * - �浹(fArrowPlayerDistance < fArrowRadius + fPlayerRadius) �̸� ȭ�� ������Ʈ �Ҹ�
         */
        if (fArrowPlayerDistance < fArrowRadius + fPlayerRadius)
        {
            /*
             * �÷��̾ ȭ�쿡 ������ ȭ�� ��Ʈ�ѷ����� ���� ��ũ��Ʈ(GameDirector)�� f_DecreaseHp() �޼��带 ȣ��
             *  ��, ArrowController���� GameDirector ������Ʈ�� �ִ� f_DecreaseHp() �޼��带 ȣ���ϱ� ������
             *  Find �޼��带 ã�Ƽ� GameDirector ������Ʈ�� ã�´�.
             */
            GameObject director = GameObject.Find("GameDirector");

            /*
             * GetComponent �޼��带 ����� GameDirector ������Ʈ�� GameDiretor ��ũ��Ʈ�� ���ϰ�,
             * f_DecreaseHp() �޼��带 �����Ͽ�, ���� ��ũ��Ʈ�� �÷��̿� ȭ���� �浹�ߴٰ� ����
             */
            director.GetComponent<GameDirector>().f_DecreaseHp();

            Destroy(gameObject);        // ȭ��� �÷��̾� �浹, ȭ�� ������Ʈ�� �Ҹ�
        }

        // ȭ���� y���� -4.5�� �������� �� ������ ������Ű�� if��
        if (transform.position.y <= -4.5f)
        {
            GameDirector.instance.IncScore();   // �Լ��� ���� ����
        }
    }
}
