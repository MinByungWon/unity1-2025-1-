using UnityEngine;
using UnityEngine.SceneManagement; // scenemanager 사용하기 위해 using으로 선언
using UnityEngine.UI; // UI를 사용하기 때문에 선언
public class MyGameSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("StartScene"); // 스타트 씬으로 로드 씬
    }

    public void GameStartButton()
    {
        SceneManager.LoadScene("GameScene"); // public으로 함수를 선언후 빈오브젝트(GameManager)에 연결후 온클릭에 빈오브젝트를 넣고 함수를 불러옴.
    }

    public void ExitGameButton()
    {
        // 에디터 플레이 모드 중지
#if UNITY_EDITOR                  // 조건부 컴파일 지시자로  이 코드는 “현재 코드를 Unity 에디터 안에서 실행할 때만” 컴파일하고 포함
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditor 네임스페이스 안에 있는 EditorApplication 클래스의 isPlaying 프로퍼티를 조작
#else
        // 빌드된 애플리케이션이 있다면 Application.Quit();으로 종료하는 것이 맞지만 에디터 모드에서는 위와 같이 사용해야 게임을 종료할 수 있다.
        Application.Quit();
#endif
    }
}
