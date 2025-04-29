using UnityEngine;
using UnityEngine.SceneManagement;     //LoadScene을 사용하는데 사용

public class ClearDirector : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        //마우스 좌클릭시 처음 화면 불러오기
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
