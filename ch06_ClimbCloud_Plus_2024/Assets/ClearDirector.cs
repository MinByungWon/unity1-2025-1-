using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// LoadScene�� ����ϱ� ���ؼ��� SceneManagement�� �������ؾ��մϴ�.
using UnityEngine.SceneManagement;

public class ClearDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺�� Ŭ���� ���� �����ϸ�, SceneManager Ŭ������ LoadScene �޼��带 ����� ���� ������ ��ȯ
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
