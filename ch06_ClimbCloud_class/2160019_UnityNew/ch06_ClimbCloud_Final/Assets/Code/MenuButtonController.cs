using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    // SubMenu를 저장할 변수
    GameObject SubMenu = null;
    GameObject HowPlay = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SubMenu UI를 가져옴
        SubMenu = GameObject.Find("SubMenu");
        HowPlay = GameObject.Find("HowPlay");

        // 시작시 SubMenu를 비활성화 시킴
        SubMenu.SetActive(false);
        HowPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 ESC를 누르면 SubMenu를 활성화하고 화살이 생성되지않게 Time을 0으로 맞춤
        if (Input.GetButton("Cancel"))
        {
            SubMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // SubMenu버튼을 누를시 SubMenu를 활성화하고 화살이 생성되지않게 Time을 0으로 맞춤
    public void OnSubMenu()
    {
        SubMenu.SetActive(true);
        Debug.Log("작동");

        Time.timeScale = 0f;
    }

    // Continue 버튼을 누르면 SubMenu를 비활성화하고 화살이 생성되게 Time을 1로 맞춤
    public void OnContinue()
    {
        SubMenu.SetActive(false);
        HowPlay.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnHowPlay()
    {
        HowPlay.SetActive(true);
    }

    public void OnExit()
    {
        SceneManager.LoadScene("MenuScene");

        Time.timeScale = 1f;
    }
}
