using UnityEngine;
using UnityEngine.UI;
public class ClearScore : MonoBehaviour
{
    public Text scoreText; // ���ھ� �ؽ�Ʈ public ������ �巡�׷� �ֱ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score: " + PlayerController.n_Score.ToString(); // PlayerController�� n_Score�� �����ͼ� �ؽ�Ʈ�� ǥ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
