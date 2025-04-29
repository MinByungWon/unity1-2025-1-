using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;           // ȭ�鿡 �ð��� �߰��� �� �ֵ��� �߰�
    public float startTime = 60f;               // ���� �ð��� 60�ʷ� ����

    private float currentTime;                  // ���� ī��Ʈ �ǰ� �ִ� �ð�
    private bool isGameOver = false;            // ������ �������� Ȯ��, ó���� false

    void Start()
    {
        currentTime = startTime;                // ���� �ð��� StartTime(60��)�� �ʱ�ȭ
    }

    void Update()
    {
        if (isGameOver) return;                 // ������ ������ ���� x

        currentTime -= Time.deltaTime;          // �ð� ����

        if (currentTime <= 0)                    // �ð��� 0 ���϶��
        {
            currentTime = 0;
            isGameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        timerText.text = "Time : " + currentTime.ToString("F1");
    }
}
