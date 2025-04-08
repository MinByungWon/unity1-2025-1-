using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// LoadScene을 사용하기 위해서는 SceneManagement를 임포드해야합니다.
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
        // 마우스가 클릭된 것을 감지하면, SceneManager 클래스의 LoadScene 메서드를 사용해 게임 씬으로 전환
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
