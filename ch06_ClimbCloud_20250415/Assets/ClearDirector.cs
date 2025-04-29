// ȭ���� Ŭ���ϸ� ��Ŭ���� �������� ������ �������� ��ȯ�ϵ��� �������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoadScene�� ����ϱ� ���ؼ��� SceneManagement�� �������ؾ��մϴ�.
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
        /*
         * ����Ƽ���� ���� �ε��ϴ� ���� SceneManager.LoadScene() �޼��带 ���
         *   - �� �̸��̳� ���� ���� �ε����� �Ķ���ͷ� �����Ͽ� Ư�� ���� �ε��� �� ����
         *   - ���� �ε��ϴ� ���� �ٸ� ���� �Բ� �ε��ϰų�, �ε�� ���� ��ε��ϴ� ���� �پ��� �ɼǵ� ������
         *   - �� �̸����� �ε�: SceneManager.LoadScene("MySceneName"); 
         *   - ���� ���� �ε����� �ε�: SceneManager.LoadScene(1); (ù ��° ���� �ε�) 
         *   - ���콺�� Ŭ���� ���� �����ϸ�, SceneManager Ŭ������ LoadScene �޼��带 ����� ���� ������ ��ȯ
         */
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
