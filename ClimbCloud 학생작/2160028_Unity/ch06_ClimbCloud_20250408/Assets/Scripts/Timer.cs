using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;           // 화면에 시간을 추가할 수 있도록 추가
    public float startTime = 60f;               // 시작 시간을 60초로 설정

    private float currentTime;                  // 현재 카운트 되고 있는 시간
    private bool isGameOver = false;            // 게임이 끝났는지 확인, 처음엔 false

    void Start()
    {
        currentTime = startTime;                // 시작 시간을 StartTime(60초)로 초기화
    }

    void Update()
    {
        if (isGameOver) return;                 // 게임이 끝나면 실행 x

        currentTime -= Time.deltaTime;          // 시간 감소

        if (currentTime <= 0)                    // 시간이 0 이하라면
        {
            currentTime = 0;
            isGameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        timerText.text = "Time : " + currentTime.ToString("F1");
    }
}
