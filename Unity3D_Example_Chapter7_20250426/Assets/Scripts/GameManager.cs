using Unity.Cinemachine;
using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanClick = true;
    }

    //BamsongiController 클래스(이름 맘에 안든다.) 에서 밤송이가 타겟 오브젝트를 맞출때 호출
    public void HitTarget()
    {
        // 카메라 전환
        this.CameraControl?.CameraMove();
    }

    // 게이지 세팅
    public void SetPowerUI(float currentRate)
    {
        this.GaugeUI?.SetGauge(currentRate);
    }
    // 게이지 비활성화
    public void CickUp()
    {
        this.GaugeUI?.InActiveGauge();
    }
}
