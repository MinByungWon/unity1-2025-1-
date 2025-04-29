using UnityEngine;
using UnityEngine.UI;
public class ClearScore : MonoBehaviour
{
    public Text scoreText; // 스코어 텍스트 public 선언후 드래그로 넣기
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score: " + PlayerController.n_Score.ToString(); // PlayerController의 n_Score를 가져와서 텍스트에 표시
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
