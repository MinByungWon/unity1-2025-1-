using UnityEngine;
using UnityEngine.SceneManagement;     //LoadScene�� ����ϴµ� ���

public class ClearDirector : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        //���콺 ��Ŭ���� ó�� ȭ�� �ҷ�����
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
