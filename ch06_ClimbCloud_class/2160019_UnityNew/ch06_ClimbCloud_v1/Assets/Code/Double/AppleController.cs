using UnityEngine;

public class AppleController : MonoBehaviour
{
    GameObject m_Cat = null;
    GameObject m_Bcat = null;

    Vector2 vApple = Vector2.zero;
    Vector2 vPlayer1 = Vector2.zero;
    Vector2 vPlayer2 = Vector2.zero;
    Vector2 vDir1 = Vector2.zero;
    Vector2 vDir2 = Vector2.zero;

    float fPlayerAppleDir = 0.0f;
    float fAppleRadius = 0.0f;
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
        Vector2 vApple = transform.position;                      // 사과 포지션
        Vector2 vPlayer1 = m_Cat.transform.position;          // 고양이 포지션
        Vector2 vPlayer2 = m_Bcat.transform.position;
        Vector2 vDir1 = vApple - vPlayer1;                          // 사과 고양이 대각선 값
        Vector2 vDir2 = vApple - vPlayer2;

        float fPlayerAppleDir1 = vDir1.magnitude;                       // 대각선 길이
        float fPlayerAppleDir2 = vDir2.magnitude;
        float fAppleRadius = 0.1f;                                    // 사과 반지름
        float fPlayerRadius = 0.5f;                                   // 고양이 반지름

        if (fPlayerAppleDir1 < fAppleRadius + fPlayerRadius || fPlayerAppleDir2 < fAppleRadius + fPlayerRadius)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().fCreasePointsApple();     //GameDirector에 있는 요소 중 하나인 fCreasePointApple을 불러와서 값을 올림

            //먹고 파괴
            Destroy(gameObject);
        }
    }
}
