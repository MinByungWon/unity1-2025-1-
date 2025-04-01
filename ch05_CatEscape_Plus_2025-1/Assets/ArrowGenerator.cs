using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화살 Prefab을 바탕으로 화살 인스턴스를 1초마다 한 개씩 생성하는 스크립트
public class ArrowGenerator : MonoBehaviour
{
    // 화살 Prefab을 넣을 빈상자 선언
    // 아울렛 접속을 위해 arrowPrefab 변수에 프리팹 실체를 대입하기 위해 콘센트 구멍을 만들기 위해서 public 접근 수식자
    public GameObject arrowPrefab = null;

    // 화살 생성 변수 : 화살을 1초마다 생성 변수
    [SerializeField]
    float fSpan = 1.0f;

    // 앞 프레임과 현재 프레임 사이의 시간 차 변수
    [SerializeField]
    float fDelta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ArrowGenerator 오브젝에서 게임 재시작 return GetIsRun()함수 호출
        if (FindObjectOfType<GameDirector>() != null)
        {
            if(FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Update 메서드는프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차를 변수에 저장
                // 프레임과 프레임 사이의 시간 차이를 fDelta 변수에 저장
                fDelta += Time.deltaTime;

                // 프레임과 프레임 사이의 시간 차가 1초 이상이면,
                if (fDelta > fSpan)
                {
                    fDelta = 0;  // 대나무통 변수(fDelta) 초깃화

                    // Instantiate 메서드 : 화살 인스턴스 생성
                    //    매개변수로 프리팹을 전달하면, 반환값으로 프리팹 인스턴스를 돌려준다.
                    GameObject go = Instantiate(arrowPrefab);

                    // Random.Range 메서드 : 화살의 X 좌표는 -6 ~ 6 사이에 불규칙하게 위치
                    //    첫 번째 매개변수보다 크거나 같고,두 번째 매개변수보다 작은 범위에서 무작위 수를 정수로 반환
                    int px = Random.Range(-6, 7);
                    go.transform.position = new Vector3(px, 7, 0);
                }
            }
        }

       
        
    }
}
