/*
 * 일정 시간마다 구름을 생성하는 스크립트
 */
using System.Transactions;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gCloudPrefab = null; //구름 Prefab을 넣을 빈오브젝트
    GameObject gCloudInstance = null; //구름 인스턴스 저장 변수

    float fCloudCreateSpan = 2.0f;  //구름 생성 간격
    float fCreateYPos = -6.0f;      //구름 생성 Y값
    float fCreateTimer = 0.0f;      //구름 생성을 위한 타이머
    float fCloudPosRange = 0.0f; //구름의 X축 범위 저장 변수
    float fRandCloudScale = 0.0f;   //구름 랜덤 크기 변수

    float fMinXRange = -2.5f;       //X축 왼쪽 화면 끝
    float fMaxXRange = 2.5f;        //X축 오른쪽 화면 끝

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update 메소드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차에는 Time.deltaTime에 대입됨
         * Time.deltaTime은 한 프레임 당 실행하는 시간을 뜻하는데, 값을 float 형태로 반환하고 단위는 초를 사용함
         * 즉, 프레임과 프레임 사이의 시간 차이를 fDeltaTime 변수에 누적
         */
        fCreateTimer += Time.deltaTime;

        if(fCreateTimer >= fCloudCreateSpan)
        {
            fCreateTimer = 0.0f; //프레임과 프레임 사이의 시간 차이 누적 변수 초기화
            f_CreateCloud(); //구름 생성 메소드 호출

            //Debug.Log("구름생성 메소드 호출됨");
        }
    }

    void f_CreateCloud()
    {
        gCloudInstance = Instantiate(gCloudPrefab); //프리팹을 통한 인스턴스 생성
        
        fCloudPosRange = Random.Range(fMinXRange, fMaxXRange);  //랜덤 위치 발생

        gCloudInstance.transform.position = new Vector3(fCloudPosRange, fCreateYPos, 0.0f);     //랜덤 위치로 이동
        gCloudInstance.GetComponent<CloudController>().moveSpeed = Random.Range(0.5f, 2.0f);    //이동 속도 랜덤으로 변경

        fRandCloudScale = Random.Range(0.7f, 1.4f); //랜덤 크기 발생
        gCloudInstance.transform.localScale = Vector3.one * fRandCloudScale; //랜덤 크기로 변경
    }
}
