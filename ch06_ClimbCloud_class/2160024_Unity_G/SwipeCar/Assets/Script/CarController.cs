using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float speed = 0;
    //car 오브젝트의 속도를 저장하는 변수
    Vector2 startPos;
    //스와이프의 시작 지점을 저장하는 변수


    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        //Input.GetMouseButtonDown(0)은 마우스 왼쪽 버튼을 눌렀을 때 true를 반환 :: 누르기 시작했을 때를 의미함

        {
            this.startPos = Input.mousePosition;
            //Input.mousePosition은 마우스의 현재 위치를 반환 :: 마우스의 현재 위치를 this.startPos에 저장
        }
        else if(Input.GetMouseButtonUp(0))
        //Input.GetMouseButtonUp(0)은 마우스 왼쪽 버튼을 뗐을 때 true를 반환 :: 버튼에 손을 뗏을 때를 의미함
        {
            Vector2 endPos = Input.mousePosition;
            //Input.mousePosition은 마우스의 현재 위치를 반환 :: 마우스의 현재 위치를 endPos에 저장

            float swipeLength = endPos.x - this.startPos.x;
            //이동 거리를 나타내는 swipeLength 변수에 endPos.x - this.startPos.x를 저장 :: x축으로 이동한 거리를 나타냄

            this.speed = swipeLength / 500.0f;
            //swipeLength를 500으로 나눈 값을 speed에 저장 :: 속도를 나타냄

            GetComponent<AudioSource>().Play();
            //오디오 소스 플레이 코드

        }
            transform.Translate(this.speed, 0, 0);
        //transform.Translate(x, y, z)는 해당 오브젝트를 x, y, z만큼 이동시키는 함수 :: x축으로 speed만큼 이동

        this.speed *= 0.98f;
        //속도를 감속시키는 코드
    }
}
