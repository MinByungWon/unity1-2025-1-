using UnityEngine;

public class OneAppleController : MonoBehaviour
{
    GameObject m_Cat = null;

    Vector2 vApple = Vector2.zero;
    Vector2 vPlayer1 = Vector2.zero;
    Vector2 vPlayer2 = Vector2.zero;
    Vector2 vDir1 = Vector2.zero;

    float fPlayerAppleDir = 0.0f;
    float fAppleRadius = 0.0f;
    float fPlayerRadius = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Cat = GameObject.Find("Ycat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vApple = transform.position;                        // ��� ������
        Vector2 vPlayer1 = m_Cat.transform.position;                // ����� ������
        Vector2 vDir1 = vApple - vPlayer1;                          // ��� ����� �밢�� ��

        float fPlayerAppleDir1 = vDir1.magnitude;                     // �밢�� ����
        float fAppleRadius = 0.1f;                                    // ��� ������
        float fPlayerRadius = 0.5f;                                   // ����� ������

        if (fPlayerAppleDir1 < fAppleRadius + fPlayerRadius)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().fCreasePointsApple();     //GameDirector�� �ִ� ��� �� �ϳ��� fCreasePointApple�� �ҷ��ͼ� ���� �ø�

            //�԰� �ı�
            Destroy(gameObject);
        }
    }
}
