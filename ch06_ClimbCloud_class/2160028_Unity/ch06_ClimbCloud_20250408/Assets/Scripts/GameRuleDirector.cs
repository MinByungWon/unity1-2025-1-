using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRuleDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ Ŭ���ϰų�, �ƹ� Ű�� ������ ��
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");        //GameScene �� ���
        }
    }
}
