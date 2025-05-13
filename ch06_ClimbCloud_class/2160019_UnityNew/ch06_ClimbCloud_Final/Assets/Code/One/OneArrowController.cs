using UnityEngine;

public class OneArrowController : MonoBehaviour
{
    GameObject m_Cat = null;

    Vector2 vArrow = Vector2.zero;
    Vector2 vPlayer1 = Vector2.zero;
    Vector2 vDir1 = Vector2.zero;

    float fPlayerArrowDir = 0.0f;
    float fArrowRadius = 0.0f;
    float fPlayerRadius = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Cat = GameObject.Find("Ycat");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        Vector2 vArrow = transform.position;                        // ȭ�� ������
        Vector2 vPlayer1 = m_Cat.transform.position;                // ����� ������
        Vector2 vDir1 = vArrow - vPlayer1;                          // ȭ�� ����� �밢�� ��

        float fPlayerArrowDir1 = vDir1.magnitude;                     // �밢�� ����
        float fArrowRadius = 0.3f;                                    // ȭ�� ������
        float fPlayerRadius = 0.5f;                                   // ����� ������

        if (fPlayerArrowDir1 < fArrowRadius + fPlayerRadius)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().fDcreasePointsArrow();     //GameDirector�� �ִ� ��� �� �ϳ��� fCreasePointApple�� �ҷ��ͼ� ���� �ø�

            Destroy(gameObject);

            //cat�� ȭ�쿡 ������ knockBack�ǰ� ����
            m_Cat.GetComponent<OneCatController>().YNnockBack();
        }
    }
}
