using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{


    public enum InfoType { Time }
    public InfoType type;
    public Button gameStartButton;

    Text myText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myText = GetComponent<Text>();
    }

    



    void OnGameStartButtonClick()
    {
        GameDirector.Instance.GameStart();
        gameStartButton.gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Time:
                float remainTime = GameDirector.Instance.maxGameTime - GameDirector.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            default:
                break;
        }
    }


}
