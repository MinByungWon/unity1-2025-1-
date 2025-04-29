using JetBrains.Annotations;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public StatusPanel Status {  get; set; }    

    private const int _maxCount = 10; // �ִ� Ƚ��
    public int MaxCount { get { return _maxCount; } }
    private int _restCount = 0; // ���� Ƚ��
    public int RestCount { get { return _restCount; } } 

    private int _score = 0;// ����
    public int Score { get { return _score; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanClick = true;
        _restCount = _maxCount;
    }

    //BamsongiController Ŭ����(�̸� ���� �ȵ��.) ���� ����̰� Ÿ�� ������Ʈ�� ���⶧ ȣ��
    public void HitTarget()
    {
        // ī�޶� ��ȯ
        this.CameraControl?.CameraMove();
        _score += 100;// ���� ����
        // ���� UI ����
        Status?.SetScore(_score);
    }

    // ������ ����
    public void SetPowerUI(float currentRate)
    {
        this.GaugeUI?.SetGauge(currentRate);
    }
    // ���콺���� ���� �� ��
    public void CickUp()
    {
        this.GaugeUI?.InActiveGauge();// ������ ��Ȱ��ȭ
        _restCount--; // ī��Ʈ ����
        //ī��Ʈ UI ����
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
