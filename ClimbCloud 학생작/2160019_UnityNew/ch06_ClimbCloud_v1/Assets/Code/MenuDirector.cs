using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDirector : MonoBehaviour
{
    public void OnePlayGame1()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void OnePlayGame2()
    {
        SceneManager.LoadScene("GameScene2");
    }
}
