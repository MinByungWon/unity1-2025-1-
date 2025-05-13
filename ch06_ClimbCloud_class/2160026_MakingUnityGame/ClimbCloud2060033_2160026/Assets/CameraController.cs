// 플레이어가 화면에 보이지 않는 더 위쪽까지 이동하면 카메라가 따라 갈 수 없는 문제점 발생
// 이 문제를 해결하기 위해 카메라가 플레이어를 따라가도록 설정

using UnityEngine;

public class CameraController : MonoBehaviour
{
    //플레이어 오브젝트를 찾기위해서 멤버변수로 선언
    GameObject m_gPlayer = null; // 플레이어 오브젝트를 저장할 멤버 변수

    // 플레이어가 수직 이동할 때마다 카메라가 따라다니도록 플레이어 y좌표를 저장할 멤버 변수
    Vector3 vPlayerPosition = Vector3.zero; // 플레이어의 y좌표를 저장할 멤버 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_gPlayer = GameObject.Find("cat"); // 플레이어 오브젝트를 찾기 위해 GameObject.Find() 메소드를 사용하여 Cat 오브젝트를 찾음

    }

    // Update is called once per frame
    void Update()
    {
        vPlayerPosition = m_gPlayer.transform.position; // 플레이어의 y좌표를 저장(vPlayerPosition)하기 위해 Cat 오브젝트의 transform.position을 사용

        // 플레이어 이동에 카메라가 따라가는 것은 y축의 방향의 변화 이므로 구한 y좌표를 반영한다.
        // x좌표와 z좌표는 카메라의 위치를 고정하기 위해서 사용
        transform.position = new Vector3(transform.position.x, vPlayerPosition.y, transform.position.z); // 카메라의 위치를 플레이어의 y좌표로 설정
    }
}
