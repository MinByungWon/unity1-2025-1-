using TMPro;
using UnityEngine;


public class GameDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameObject car;
    GameObject flag;
    GameObject distance;
    //car, flag, distance ������Ʈ�� �����ϴ� ����
    void Start()
    {
        this.car = GameObject.Find("car_0");
        this.flag = GameObject.Find("flag_0");
        this.distance = GameObject.Find("distance");
        //���� ������Ʈ�� ã�Ƽ� �� ������ ������

    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flag.transform.position.x - this.car.transform.position.x;
        //flag�� car�� x��ǥ ���̸� length�� ���� :: TMPro�� ����� ���� �Ÿ��� ����ֱ� ����

        this.distance.GetComponent<TextMeshProUGUI>().text = "Distance: " + length.ToString("F2") + "m";
        //TMPro�� ����� distance ������Ʈ�� ���� �Ÿ��� ǥ���ϴ� �ڵ�
    }
}
