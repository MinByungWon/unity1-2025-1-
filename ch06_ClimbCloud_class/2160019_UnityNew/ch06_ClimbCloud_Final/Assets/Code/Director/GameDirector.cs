//Text�� �ҷ��������� TMPro�� ���
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // ���� ���� UI
    GameObject gGameScore = null;

    // ����
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

        Debug.Log("�ǰ�");
    }

    // 
    void ScoreManager()
    {
        // TextMeshUI�� �����ͼ� scoreTMP�� ����
        TextMeshProUGUI scoreTMP = gGameScore.GetComponent<TextMeshProUGUI>();

        // TextMeshUI�� text�� Score : + ���� ������ ��Ÿ���� ����.
        scoreTMP.text = "Score : " + fPoints.ToString();
    }

}
