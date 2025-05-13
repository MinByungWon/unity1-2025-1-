// 화면을 클릭하면 다음 씬으로 넘어가는 스크립트
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R)) // 마우스 클릭 시 또는 R키를 누를 시
        {
            SceneManager.LoadScene("GameScene"); // GameScene으로 이동
        }
        else if(Input.GetKey(KeyCode.Space)) // 스페이스바 누를 시
        {
            SceneManager.LoadScene("StartScene"); // StartScene으로 이동
        }
    }
}
