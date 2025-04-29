using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Score = null;
    [SerializeField]
    Button RetryBtn = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Score != null)
        {
            Score.text = "TotalScore : " + GameManager.Instance.Score.ToString();
        }        

        if(RetryBtn != null)
        {
            // ��ư onClick Action �Լ� ����
            RetryBtn.onClick.AddListener(GameManager.Instance.ReStart);
        }
    }

}
