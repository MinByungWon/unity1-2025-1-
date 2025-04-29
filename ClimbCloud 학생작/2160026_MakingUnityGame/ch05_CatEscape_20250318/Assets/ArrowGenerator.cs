/*
 * 화살 오브젝트를 1초에 한 개씩 생성하는 알고리즘
 * Update 메소드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차이의 Timedelta를 인수로 받는다.
 * 프레임과 프레임 사이의 시간 차이를 delta에 모으고 1초 이상이 되면 delta 변수를 비움
 * 변수를 비우는 시점인 1초에 한번씩 화살이 생성
 * instantiate 메소드 : 인수로 전달한 오브젝트를 생성하는 메소드
 * 게임을 실행하는 도중에 게임 오브젝트를 생성할 수 있음
 * 화살 프리팹을 이용하여 화살 인스턴스를 생성하는 메소드
 * Random.Range() : 인수로 전달한 두 정수 사이의 난수를 생성하는 메소드
 * 사용자가 제공한 최솟값과 최댓값 사이의 임의의 숫자를 제공
 * 첫번째 매개변수보다 크거나 같고 두번째 매개변수보다 작은 범위에서 무작위 수를 랜덤하게 반환
 */
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    // public으로 준 이유는 인스펙터 창에서 값을 변경할 수 있도록 하기 위함
    /*
     * arrowPrefab 변수에 프리팹 실체를 대입하기 위해서 public  접근 수식자
     * 멤버변수 선언시 public 접근 수식자를 사용하면 인스펙터 창에서 Prefab설계도 대입할 수 있도록 함
     * 화살 대량 생성을 위해서 양산 기계에 넘겨 줄 Prefab 설계도를 넘겨 주어야 함
     */
    public GameObject gArrowPrefab = null;     // 화살 Prefab을 넣을 빈오브젝트 상자 선언
   
    GameObject gArrowInstance = null;          // 화살 인스턴스 저장 변수
    float fArrowCreateSpan = 1.0f;
    float fDeltaTime = 0;
    int nArrowPositionRange = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update 메소드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차이는 Time.deltaTime을 인수로 받는다.
         * Time.deltaTime은 "한 프레임 당 실행하는 시간"을 의미, 값 float 형식으로 반환하고 단위는 초를 사용
         * 즉, 프레임과 프레임 사이의 시간 차이를 fDeltaTime에 모음
        */
        this.fDeltaTime += Time.deltaTime;

        // 화살을 1초(fArrowCreateSpan)마다 한 개씩 생성
        // 프레임당 시간차이가 1초를 넘어가면 화살을 생성
        if (this.fDeltaTime > this.fArrowCreateSpan)
        {
            this.fDeltaTime = 0.0f;

            //intantiante 메소드 : 화살 프리팹을 이용하여, 화살 인스턴스를 생성하는 메소드
            // 매개변수로 프리팹을 전달하면 반환값으로 프리팹 인스턴스를 돌려준다
            // instantiate 메소드를 사용하면 게임을 실행하는 도중에 게임 오브젝트를 생성할 수 있음
            // RPG게임이라면 수많은 아이템, 캐릭터, 배경등 모든 것들은 어떻게 미리 만들어 놓을수 있을까?
            // 게임 오브젝트의 복제본을 생성
            // instantiate(GameObject original, vector3 position, quaternion rotation) 
            // gameobject original : 복제할 오브젝트
            // vector3 position : 복제할 오브젝트의 위치
            // quaternion rotation : 복제할 오브젝트의 회전
            gArrowInstance = Instantiate( gArrowPrefab );

            // Random.Range() : 인수로 전달한 두 정수 사이의 난수를 생성하는 메소드
            // 제공된 최솟값과 최댓값이 정수인지 실수인지에 따라 정수 또는 실수를 반환함
            // 첫번째 매개변수보다 크거나 같고 두번째 매개변수보다 작은 범위에서 무작위 수를 정수로 반환
            // 화살의 x좌표는 -6~6 사이에 불규칙하게 생성
            nArrowPositionRange = Random.Range(-6, 7);
            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 7, 0);
        }
    }
}
