using UnityEngine;

public class RouletteController : MonoBehaviour
{

    float fRouletteRotationSpeed = 0.0f; //롤렛 회전속도 조절 멤버변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 디바이스 성능에 따른 실행 결과의 차이 없애기
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        // 왼쪽 마우스를 클릭하면 룰렛은 매 프레임 10도씩 회전
        //    클릭하면 회전 속도를 멤버변수 fRouletteRotationSpeed에 설정한다.
        if (Input.GetMouseButtonDown(0))
        {
            fRouletteRotationSpeed = 10;
        }

        // 롤렛 오브젝트를 현재 각도에서 인수 값만큼 회전함
        transform.Rotate(0, 0, fRouletteRotationSpeed);

        // 롤렛 회전 감속시키기
        fRouletteRotationSpeed *= 0.98f;

    }
}
