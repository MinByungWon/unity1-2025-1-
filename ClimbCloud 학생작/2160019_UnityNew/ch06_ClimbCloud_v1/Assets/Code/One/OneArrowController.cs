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
        m_Cat = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        Vector2 vArrow = transform.position;                      // 사과 포지션
        Vector2 vPlayer1 = m_Cat.transform.position;          // 고양이 포지션
        Vector2 vDir1 = vArrow - vPlayer1;                          // 사과 고양이 대각선 값

        float fPlayerArrowDir1 = vDir1.magnitude;                       // 대각선 길이
        float fArrowRadius = 0.3f;                                    // 사과 반지름
        float fPlayerRadius = 0.5f;                                   // 고양이 반지름

        if (fPlayerArrowDir1 < fArrowRadius + fPlayerRadius)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().fDcreasePointsArrow();     //GameDirector에 있는 요소 중 하나인 fCreasePointApple을 불러와서 값을 올림

            //먹고 파괴
            Destroy(gameObject);
        }
    }
}
