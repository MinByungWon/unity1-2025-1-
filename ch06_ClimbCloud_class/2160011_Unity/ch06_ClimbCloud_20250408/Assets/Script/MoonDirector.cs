using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene을 사용하는 데 필요하다.

public class MoonDirector : MonoBehaviour
{
   
    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClick1()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void OnClick2()
    {
        SceneManager.LoadScene("MoonScene");
    }

    public void OnClick3()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClick4()
    {
        SceneManager.LoadScene("OptionScene");
    }
}
