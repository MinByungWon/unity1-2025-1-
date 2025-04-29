using UnityEngine;
using TMPro;


public class StatusPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Score;
    [SerializeField]
    TextMeshProUGUI Count;

    int _maxCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.Status = this;
        _maxCount = GameManager.Instance.MaxCount;
        SetScore(GameManager.Instance.Score);
        SetCount(_maxCount);
    }

    public void SetScore(int score)
    {
        Score.text = $"Score : {score}";
    }

    public void SetCount(int restCount)
    {
        Count.text = $"{restCount} / {_maxCount}";  
    }
}
