using UnityEngine;
using UnityEngine.SceneManagement; // ���� �ٲٱ� ���ؼ��� using���� scenemanager�� �־�����Ѵ�.
using UnityEngine.UI; // UI ������Ʈ�� ��ũ��Ʈ���� �����ϱ� ���ؼ� ����Ʈ��

public class GameManager : MonoBehaviour
{
    public void PlaySceneChange() // ������ �÷��� �ϴ� ������ �ٲٱ� ���� public���� ���� ����
    {
        SceneManager.LoadScene("GameScene"); // SampleScene�� �ҷ����� ���
    }

    public void ExitGame() // ���� ���Ḧ ���� �Լ� ����
    {
        Application.Quit(); // ���ø����̼� ����
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
#if UNITY_EDITOR                                         // #if UNITY_EDITOR�� ����Ƽ �����Ϳ��� ������ ���� �ڵ尡 �����ϵ��� �ϴ� 
                                                         //��ó���� ���ù�
        UnityEditor.EditorApplication.isPlaying = false; // ����Ƽ �����Ϳ��� ���� ���� ��� ���� ����
#else                                                    // ����Ƽ �����Ͱ� �ƴ� ���
        Application.Quit();                              // ���� ����
#endif
    }
}
