using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    // SubMenu�� ������ ����
    GameObject SubMenu = null;
    GameObject HowPlay = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SubMenu UI�� ������
        SubMenu = GameObject.Find("SubMenu");
        HowPlay = GameObject.Find("HowPlay");

        // ���۽� SubMenu�� ��Ȱ��ȭ ��Ŵ
        SubMenu.SetActive(false);
        HowPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ESC�� ������ SubMenu�� Ȱ��ȭ�ϰ� ȭ���� ���������ʰ� Time�� 0���� ����
        if (Input.GetButton("Cancel"))
        {
            SubMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // SubMenu��ư�� ������ SubMenu�� Ȱ��ȭ�ϰ� ȭ���� ���������ʰ� Time�� 0���� ����
    public void OnSubMenu()
    {
        SubMenu.SetActive(true);
        Debug.Log("�۵�");

        Time.timeScale = 0f;
    }

    // Continue ��ư�� ������ SubMenu�� ��Ȱ��ȭ�ϰ� ȭ���� �����ǰ� Time�� 1�� ����
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
