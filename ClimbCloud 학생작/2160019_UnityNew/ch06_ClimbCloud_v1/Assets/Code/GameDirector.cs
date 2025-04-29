//Text를 불러오기위해 TMPro를 사용
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject gGameScore = null;

    float fPoints = 0.0f;

    void Start()
    { 
        this.gGameScore = GameObject.Find("Score");
    }

    void Update()
    {
        if(fPoints < 0)
        {
            SceneManager.LoadScene("ClearScene");
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
    }

    void ScoreManager()
    {
        TextMeshProUGUI scoreTMP = gGameScore.GetComponent<TextMeshProUGUI>();

        scoreTMP.text = "Score : " + fPoints.ToString();
    }

}
