using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// ��ǻ� GameDirector ���ٴ� Manager��� �̸����� ���� ���δ�.
/// �ش� Ŭ������ ���׸� �̱��� Class(Singleton.cs ����) �Ͽ��⿡ �ڵ����� �̱��� �ν��Ͻ���
/// ������ �ȴ�.
/// ���� ��𼭵� GameManager�� ȣ���� �� ������, ���� �Ѿ�� �ı����� �ʰ� �����ȴ�.
/// </summary>

public class GameManager : Singleton<GameManager>
{
    public bool CanClick { get; set; }

    // CameraController �ν��Ͻ� ������Ƽ
    public CameraController CameraControl { get; set; } 

    public GaugePanel GaugeUI { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanClick = true;
    }

    //BamsongiController Ŭ����(�̸� ���� �ȵ��.) ���� ����̰� Ÿ�� ������Ʈ�� ���⶧ ȣ��
    public void HitTarget()
    {
        // ī�޶� ��ȯ
        this.CameraControl?.CameraMove();
    }

    // ������ ����
    public void SetPowerUI(float currentRate)
    {
        this.GaugeUI?.SetGauge(currentRate);
    }
    // ������ ��Ȱ��ȭ
    public void CickUp()
    {
        this.GaugeUI?.InActiveGauge();
    }
}
