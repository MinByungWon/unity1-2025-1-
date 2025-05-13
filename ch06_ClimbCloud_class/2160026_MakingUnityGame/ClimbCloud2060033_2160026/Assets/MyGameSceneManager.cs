using UnityEngine;
using UnityEngine.SceneManagement; // scenemanager ����ϱ� ���� using���� ����
using UnityEngine.UI; // UI�� ����ϱ� ������ ����
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
        SceneManager.LoadScene("StartScene"); // ��ŸƮ ������ �ε� ��
    }

    public void GameStartButton()
    {
        SceneManager.LoadScene("GameScene"); // public���� �Լ��� ������ �������Ʈ(GameManager)�� ������ ��Ŭ���� �������Ʈ�� �ְ� �Լ��� �ҷ���.
    }

    public void ExitGameButton()
    {
        // ������ �÷��� ��� ����
#if UNITY_EDITOR                  // ���Ǻ� ������ �����ڷ�  �� �ڵ�� ������ �ڵ带 Unity ������ �ȿ��� ������ ������ �������ϰ� ����
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditor ���ӽ����̽� �ȿ� �ִ� EditorApplication Ŭ������ isPlaying ������Ƽ�� ����
#else
        // ����� ���ø����̼��� �ִٸ� Application.Quit();���� �����ϴ� ���� ������ ������ ��忡���� ���� ���� ����ؾ� ������ ������ �� �ִ�.
        Application.Quit();
#endif
    }
}
