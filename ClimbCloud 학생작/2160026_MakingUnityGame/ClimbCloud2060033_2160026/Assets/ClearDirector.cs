// ȭ���� Ŭ���ϸ� ���� ������ �Ѿ�� ��ũ��Ʈ
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R)) // ���콺 Ŭ�� �� �Ǵ� RŰ�� ���� ��
        {
            SceneManager.LoadScene("GameScene"); // GameScene���� �̵�
        }
        else if(Input.GetKey(KeyCode.Space)) // �����̽��� ���� ��
        {
            SceneManager.LoadScene("StartScene"); // StartScene���� �̵�
        }
    }
}
