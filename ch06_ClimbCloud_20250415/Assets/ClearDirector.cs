// 화면을 클릭하면 ‘클리어 씬’에서 ‘게임 씬’으로 전환하도록 만들겠음

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoadScene을 사용하기 위해서는 SceneManagement를 임포드해야합니다.
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
         * 유니티에서 씬을 로드하는 것은 SceneManager.LoadScene() 메서드를 사용
         *   - 씬 이름이나 빌드 설정 인덱스를 파라미터로 전달하여 특정 씬을 로드할 수 있음
         *   - 씬을 로드하는 동안 다른 씬을 함께 로드하거나, 로드된 씬을 언로드하는 등의 다양한 옵션도 제공됨
         *   - 씬 이름으로 로드: SceneManager.LoadScene("MySceneName"); 
         *   - 빌드 설정 인덱스로 로드: SceneManager.LoadScene(1); (첫 번째 씬을 로드) 
         *   - 마우스가 클릭된 것을 감지하면, SceneManager 클래스의 LoadScene 메서드를 사용해 게임 씬으로 전환
         */
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
