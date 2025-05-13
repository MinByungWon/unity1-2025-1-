//Text를 불러오기위해 TMPro를 사용
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // 점수 담을 UI
    GameObject gGameScore = null;

    // 점수
    float fPoints = 0.0f;

    void Start()
    { 
        this.gGameScore = GameObject.Find("Score");
    }

    void Update()
    {
        if(fPoints < 0)
        {
            SceneManager.LoadScene("OverScene");
        }    
    }
    public void fCreasePointsBanana()
    {
        fPoints += 100;

        ScoreManager();
    }

    public void fCreasePointsApple()
    {
        fPoints += 300;

        ScoreManager();
    }

    public void fDcreasePointsArrow()
    {
        fPoints -= 75;
        ScoreManager();

        Debug.Log("피격");
    }

    // 
    void ScoreManager()
    {
        // TextMeshUI를 가져와서 scoreTMP에 저장
        TextMeshProUGUI scoreTMP = gGameScore.GetComponent<TextMeshProUGUI>();

        // TextMeshUI의 text를 Score : + 얻은 점수로 나타내게 만듬.
        scoreTMP.text = "Score : " + fPoints.ToString();
    }

}
