using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;   // UI를 사용하므로 추가
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour
{
    GameObject hpGauge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.hpGauge = GameObject.Find("hpGauge");
    }



    public void DecreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
