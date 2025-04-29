using JetBrains.Annotations;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 사실상 GameDirector 보다는 Manager라는 이름으로 많이 쓰인다.
/// 해당 클래스는 제네릭 싱글톤 Class(Singleton.cs 참고) 하였기에 자동으로 싱글톤 인스턴스가
/// 적용이 된다.
/// 언제 어디서든 GameManager를 호출할 수 있으며, 씬이 넘어가도 파괴되지 않고 유지된다.
/// </summary>

public class GameManager : Singleton<GameManager>
{
    public bool CanClick { get; set; }

    // CameraController 인스턴스 프로퍼티
    public CameraController CameraControl { get; set; } 

    public GaugePanel GaugeUI { get; set; }

    public StatusPanel Status {  get; set; }    

    private const int _maxCount = 10; // 최대 횟수
    public int MaxCount { get { return _maxCount; } }
    private int _restCount = 0; // 남은 횟수
    public int RestCount { get { return _restCount; } } 

    private int _score = 0;// 점수
    public int Score { get { return _score; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanClick = true;
        _restCount = _maxCount;
    }

    //BamsongiController 클래스(이름 맘에 안든다.) 에서 밤송이가 타겟 오브젝트를 맞출때 호출
    public void HitTarget()
    {
        // 카메라 전환
        this.CameraControl?.CameraMove();
        _score += 100;// 점수 증가
        // 점수 UI 갱신
        Status?.SetScore(_score);
    }

    // 게이지 세팅
    public void SetPowerUI(float currentRate)
    {
        this.GaugeUI?.SetGauge(currentRate);
    }
    // 마우스에서 손을 뗄 시
    public void CickUp()
    {
        this.GaugeUI?.InActiveGauge();// 게이지 비활성화
        _restCount--; // 카운트 감소
        //카운트 UI 갱신
        Status?.SetCount(_restCount);
    }

    public void GameEnd()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Invoke("LoadEndScene", 1);
        }
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void ReStart()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            _restCount = _maxCount;
            _score = 0;
            CanClick = true;
            SceneManager.LoadScene("GameScene");
        }
    }
}
