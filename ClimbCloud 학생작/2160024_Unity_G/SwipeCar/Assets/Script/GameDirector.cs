using TMPro;
using UnityEngine;


public class GameDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameObject car;
    GameObject flag;
    GameObject distance;
    //car, flag, distance 오브젝트를 저장하는 변수
    void Start()
    {
        this.car = GameObject.Find("car_0");
        this.flag = GameObject.Find("flag_0");
        this.distance = GameObject.Find("distance");
        //게임 오브젝트를 찾아서 각 변수에 저장함

    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flag.transform.position.x - this.car.transform.position.x;
        //flag와 car의 x좌표 차이를 length에 저장 :: TMPro를 사용해 남은 거리를 띄워주기 위함

        this.distance.GetComponent<TextMeshProUGUI>().text = "Distance: " + length.ToString("F2") + "m";
        //TMPro를 사용해 distance 오브젝트에 남은 거리를 표시하는 코드
    }
}
