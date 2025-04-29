using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 화면에 보이지 않는 더 위쪽 까지 이동하면, 카메라가 따라 갈 수 없는 문제점 발생
// 이문제점을 해결하기 위해서는, 카메라가 플레이어를 따라다니며 움직일 수 있도록 스크립트 작성

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //플레이어 오브젝트를 찾아서 멤버 변수에 저장
        this.player = GameObject.Find("cat_0");

    }

   
    // Update is called once per frame
    void Update()
    {
        //플레이어가 수직 이동할 때마다 카메라가 따라다니도록 프레임마다 플레이어 좌표를 구해서 저장
        Vector3 playerPos = this.player.transform.position;

        //플레이어 이동에 카메라가 따라가는 것은 Y축 방향(수직 방향)의 변화이므로 위에서 구한 Y좌표값을 반영한다.
        // X좌표와 Z좌표는 변함이 없으므로 카메라의 원해 좌표를 그대로 사용 
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}
