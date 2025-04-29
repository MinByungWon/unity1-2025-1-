using UnityEngine;

public class DiaGenerator : MonoBehaviour
{
    public GameObject gArrowPrefab = null;  // 화살 Prefab을 넣을 빈 오브젝트 상자 
    GameObject gArrowInstance = null;       // 화살 인스턴스 저장 변수

    float fArrowCreateSpan = 3;               // 화살 생성 변수 : 화살을 3초마다 생성 변수
    float fDeltaTime = 0.0f;                // 앞 프레임과 현재 프레임 사이의 시간 차이를 저장하는 변수

    int nArrowPositionRange = 0;            // 화살의 X 좌표 Range 저장 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fDeltaTime += Time.deltaTime;


        if(fDeltaTime > fArrowCreateSpan)
        {
            fDeltaTime = 0.0f;  // 프레임과 프레임 사이의 시간 차이 누적 변수 초기화

            gArrowInstance = Instantiate(gArrowPrefab); // public으로 받은 화살 prefab을 저장


            nArrowPositionRange = Random.Range(-3, 3);    //x축에서 -3부터 3까지 랜덤으로 생성

            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 46, 0);
        }
    }
}
