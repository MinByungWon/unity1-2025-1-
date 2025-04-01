using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 오브젝트를 스크립트에서 조작하기 위해서 임포트

// 플레이어가 화살에 맞으면 이를 감지해 HP 게이지의 표시를 갱신하는 스크립트
public class GameDirector : MonoBehaviour
{
    // HP 게이지를갱신하려면 감독 스크립트가 HP 게이지의 실체를 조작하기 위해서 GameObject 변수 선언
    Image hpGauge;

    public GameObject txtGameOverMessage = null;  // UI(Text) : txtGameOverMessage GameObject 변수

    public GameObject btnReStart = null; // UI(Button) : btnReStart GameObject 변수

    bool isRun = false;  // 게임 재시작 저장 변수

    // Start is called before the first frame update
    void Start()
    {
        // Find 메서드를 사용 해 씬 중에서 HP 게이지의 오브젝트를 찾고 hpGauge 변수에 대입
        hpGauge = GameObject.Find("hpGauge").GetComponent<Image>();

        // Game Over Text
        txtGameOverMessage = GameObject.Find("txtGameOverMessage");
        txtGameOverMessage.gameObject.SetActive(false);

        // 게임 재시작 Button
        btnReStart = GameObject.Find("btnReStart");
        btnReStart.gameObject.SetActive(false);

        // 게임 재시작 : true
        isRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 화살 컨트롤러에서 HP 게이지 표시를 줄이는 처리를 호출하기 위해서 public 메서드로 작성
    public void DecreaseHp()
    {
        // 화살과 플레이어가 충돌했을 때 화살 컨트롤러가 Image 오브젝트(hpGauge)의 fillAmount를 줄여 HP 게이지를 표시
        hpGauge.fillAmount -= 0.1f;

        // 게임 오버!!
        if (hpGauge.fillAmount <= 0)
        {
            // 게임종료 Text 활성화
            txtGameOverMessage.gameObject.SetActive(true);

            // 게임재시작 Text 활성화
            btnReStart.gameObject.SetActive(true);

            // 게임 재시작 : false
            isRun = false;
        }
    }

    public void GameInitialized()
    {
        // 게이지 초기화
        hpGauge.fillAmount = 1;
        // 텍스트 초기화
        txtGameOverMessage.gameObject.SetActive(false);
        // 버튼 초기화
        btnReStart.SetActive(false);

        // 게임 재시작 : true
        isRun = true;

        ArrowController[] arrowControllers = FindObjectsOfType<ArrowController>();
        for (int i = arrowControllers.Length -1 ; i >= 0; i++)
        {
            Destroy(arrowControllers[i].gameObject);
        }
    }

    // 게임 재시작 return 함수
    public bool GetIsRun()
    {
        return isRun;
    }
}
