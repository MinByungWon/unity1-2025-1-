using UnityEngine;
using UnityEngine.SceneManagement; // 씬을 바꾸기 위해서는 using으로 scenemanager를 넣어줘야한다.
using UnityEngine.UI; // UI 오브젝트를 스크립트에서 조작하기 위해서 임포트함

public class GameManager : MonoBehaviour
{
    public void PlaySceneChange() // 게임을 플레이 하는 씬으로 바꾸기 위해 public으로 먼저 선언
    {
        SceneManager.LoadScene("GameScene"); // SampleScene을 불러오는 기능
    }

    public void ExitGame() // 게임 종료를 위해 함수 선언
    {
        Application.Quit(); // 어플리케이션 종료
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameExit()
    {
#if UNITY_EDITOR                                         // #if UNITY_EDITOR는 유니티 에디터에서 실행할 때만 코드가 동작하도록 하는 
                                                         //전처리기 지시문
        UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터에서 실행 중인 경우 게임 종료
#else                                                    // 유니티 에디터가 아닌 경우
        Application.Quit();                              // 게임 종료
#endif
    }
}
