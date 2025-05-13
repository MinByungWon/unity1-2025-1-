using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* [교수님이 알려주신 방식]
     * float fMaxPositionX = 10.0f; // 플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 vector3의 최대값을 설정
     * float fMinPositionX = 10.0f; // 플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 vector3의 최소값을 설정
     * float fPositionX = 0.0f; // 플레이어가 좌, 우 이동할 수 있는 x좌표의 값
     */


    //Start 메소드
    //    미리 정의된 특수 이벤트 함수로써, 이특수 함수들을 c# 에서는 함수를 메소드라 함
    //    MonoBehaviour 클래스가 초기화 될 때, 호출 되는 이벤트 함수
    //    프로그램이 시작할 때 한 번만 호출이 되는 함수로 보통 컴포넌트를 받아오거나 업데이트나 다른함수에서 사용하기 위해 초기화 해주는 기능
    //    즉, Start() 메소드는 스크립트 인스턴스가 활성화된 경우에만 첫 번쨰 프레임 업데이트 전에 호출하므로 한번만 실행
    //    씬 에셋에 포함된 모든 오브젝트에 대해 Update 등 이전에 호출된 모든 스크립트를 위한 Start 함수가 호출
    //    따라서 게임 플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않음
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 디바이스 성능에 따른 실행 결과의 차이 없애기, 어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리, 스마트폰은 60, 고속의 pc는 300이 될 수 있는 디바이스 성능에 따라 게임 동작에 영향을 미칠 수 있음
        // 프레임레이트를 60으로 고정
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        // 화면 밖으로 플레이어가 나가지 않도록하는 코드
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // Camera.main : 프로젝트를 처음 생성했을 떄의 메인카메라를 의미, WorldToViewportPoint : 월드 좌표를 뷰포트 좌표로 변환, 뷰포트를 사용하는 이유는 화면 크기에 따라 좌표가 달라지기 때문에 월드 좌표를 정규화하여 사용
        // 뷰 포트의 좌표는    0에서 1 사이의 값을 가지며, 0은 왼쪽 끝, 1은 오른쪽 끝을 의미, 상하도 마찬가지로 0은 아래쪽 끝, 1은 위쪽 끝을 의미
        if (pos.x < 0f) // x좌표가 0보다 작으면
        {
            pos.x = 0f; // x좌표를 0으로
        }

        if (pos.x > 1f) // x좌표가 1보다 크면
        {
            pos.x = 1f; // x좌표를 1로
        }

        if (pos.y < 0f) // y좌표가 0보다 작으면
        {
            pos.y = 0f; // y좌표를 0으로
        }

        if (pos.y > 1f) // y좌표가 1보다 크면
        {
            pos.y = 1f; //  y좌표를 1로
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos); // 뷰포트 좌표를 월드 좌표로 변환

        /* 키가 눌렸는지 검출하기 위해서는 Input 클래스의 GetKeyDown 메소드를 사용함
         * 이 메소드는 매개변수로 전달한 키가 눌리는 순간 true를 한 번 반환
         * GetKeyDown 메소드는 지금까지 사용하던 GetMouseButtonDown 메소드와 비슷하므로 쉽게 이해할 수 있을 것이다.
         * 키를 누른 순간 : GetKeyDown()
         * 키를 누르고 있는 동안 : GetKey()
         * 키를 누르다가 뗀 그 순간 : GetKeyUp()
        */
        //왼쪽 화살표 키가 눌렸을 때 -> GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
            transform.Translate(-3, 0, 0);
        }
        //오른쪽 화살표 키가 눌렸을 때 -> GetKeyDown(KeyCode.RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0);
        }

        /* [교수님이 알려주신 방식]
         * fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX); // Clamp란 최소값과 최대값을 설정하여 그 사이의 값만을 반환하는 메소드
         * 플레이어가 움직일 수 있는 최소/최대 범위값을 설정하여 그 범위를 벗어나지 않도록 한다.  
         * transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
         */
    }
    public void LButtonDown() // 왼쪽 버튼
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButtonDown() // 오른쪽 버튼
    {
        transform.Translate(3, 0, 0);
    }
}
