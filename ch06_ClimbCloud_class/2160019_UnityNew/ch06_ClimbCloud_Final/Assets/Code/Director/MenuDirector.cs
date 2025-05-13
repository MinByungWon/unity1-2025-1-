using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDirector : MonoBehaviour
{
    GameObject HowPlay = null;

    void Start()
    {
        HowPlay = GameObject.Find("HowPlay");

        // HowPlay�޴� ��Ȱ��ȭ
        HowPlay.SetActive(false);
    }
    // 1�� �÷��� ����
    public void OnePlayGame1()
    {
        SceneManager.LoadScene("GameScene1");
    }

    // 2�� �÷��� ����
    public void OnePlayGame2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void OnHowPlay()
    {
        // HowPlay�޴� Ȱ��ȭ
        HowPlay.SetActive(true);
    }

    public void OnHowExit()
    {
        // HowPlay�޴� ��Ȱ��ȭ
        HowPlay.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
        Debug.Log("���� ����");
    }
}
