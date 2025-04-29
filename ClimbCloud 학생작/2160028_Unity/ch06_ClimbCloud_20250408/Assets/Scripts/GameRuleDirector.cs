using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRuleDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽을 클릭하거나, 아무 키나 눌렀을 때
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");        //GameScene 씬 출력
        }
    }
}
