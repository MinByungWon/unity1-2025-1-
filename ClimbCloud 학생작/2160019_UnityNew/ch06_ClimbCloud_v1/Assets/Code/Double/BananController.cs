using UnityEngine;

public class BananController : MonoBehaviour
{
    GameObject m_Cat = null;
    GameObject m_Bcat = null;

    Vector2 vBanana = Vector2.zero;
    Vector2 vPlayer1 = Vector2.zero;
    Vector2 vPlayer2 = Vector2.zero;
    Vector2 vDir1 = Vector2.zero;
    Vector2 vDir2 = Vector2.zero;

    float fPlayerBnanaDir = 0.0f;
    float fBananaRadius = 0.0f;
    float fPlayerRadius = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Cat = GameObject.Find("cat");
        m_Bcat = GameObject.Find("Bcat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vBanana = transform.position;                      // ��� ������
        Vector2 vPlayer1 = m_Cat.transform.position;          // ����� ������
        Vector2 vPlayer2 = m_Bcat.transform.position;
        Vector2 vDir1 = vBanana - vPlayer1;                          // ��� ����� �밢�� ��
        Vector2 vDir2 = vBanana - vPlayer2;

        float fPlayerBnanaDir1 = vDir1.magnitude;                       // �밢�� ����
        float fPlayerBnanaDir2 = vDir2.magnitude;
        float fBananaRadius = 0.3f;                                    // ��� ������
        float fPlayerRadius = 0.5f;                                   // ����� ������

        if (fPlayerBnanaDir1 < fBananaRadius + fPlayerRadius || fPlayerBnanaDir2 < fBananaRadius + fPlayerRadius)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().fCreasePointsBanana();     //GameDirector�� �ִ� ��� �� �ϳ��� fCreasePointApple�� �ҷ��ͼ� ���� �ø�

            //�԰� �ı�
            Destroy(gameObject);
        }
    }
}
