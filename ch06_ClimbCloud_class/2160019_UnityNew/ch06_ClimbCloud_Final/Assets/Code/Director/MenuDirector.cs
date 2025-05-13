using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDirector : MonoBehaviour
{
    GameObject HowPlay = null;

    void Start()
    {
        HowPlay = GameObject.Find("HowPlay");

        // HowPlay메뉴 비활성화
        HowPlay.SetActive(false);
    }
    // 1인 플레이 선택
    public void OnePlayGame1()
    {
        SceneManager.LoadScene("GameScene1");
    }

    // 2인 플레이 선택
    public void OnePlayGame2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void OnHowPlay()
    {
        // HowPlay메뉴 활성화
        HowPlay.SetActive(true);
    }

    public void OnHowExit()
    {
        // HowPlay메뉴 비활성화
        HowPlay.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
