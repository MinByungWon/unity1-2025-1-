using UnityEngine;
using UnityEngine.SceneManagement;     //LoadScene을 사용하는데 사용

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
