using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem; //new InputSystem ���

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongiPrefab;

    float _basicPower = 300; // �⺻ ������ ��
    float _currentPower = 0; // �⺻ ������ ����� ���� �� 
    float _multiplier = 1; // �⺻ ���� ���� ��� -> SetSpeed���� ������
    float _maxMultiplier = 10; // �ִ���
    float _maxPower = 0; // �ִ� �Ŀ�

    bool _isClick = false;

    private void Start()
    {
        _maxPower = _basicPower * _maxMultiplier; // �ִ� ���ǵ� ����
    }

    void Update()
    {
        /*
              if (Input.GetMouseButtonDown(0))
         {
             // �̱��� GameManager�� CanClick�� true �̿��߸� �߻� �����ϰ� ����
             if (GameManager.Instance.CanClick)
             {
                 GameObject bamsongi = Instantiate(bamsongiPrefab);

                 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 Vector3 worldDir = ray.direction;
                 // ���翡���� worldDir.nomalized �� �Ͽ� ���⺤�͸� ��������
                 // �̹� ray.direction�� ����ȭ�� ���͸� return�ϱ⿡ ���ʿ��ϴ�.
                 // �Ʒ�ó�� ���� ����� �ִ� ������ ������ ����
                 bamsongi.GetComponent<BamsongiController>().Shoot(worldDir.normalized * 2000);
             }
         }
         */

        // ���콺 Ŭ�� ����
        ClickDetect();
        // Speed ���� 
        SetSpeed();
    }

    // Update�� ���� ���� ���� ��ɺ��� ���ȭ �ϴ� ������ Ű����
    // Click ���� �Լ�
    void ClickDetect()
    {
        // ����Ƽ���� new InputSystem ����� �����ϹǷ� �� ���� InputAction����
        // ���� ���ε� �ϴµ�, �˾ƺ��� ����.
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.CanClick)
        {
            // ���������ӿ��� ���콺�� �������� �� �ѹ�
            // ���� GameManager �ν��Ͻ��� CanClick�� true �� ������
            _isClick = true;
            Debug.Log("Ŭ�� down");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && GameManager.Instance.CanClick)
        {
            // ���� �����ӿ��� ���콺�� ���� ��
            _isClick = false;
            Debug.Log("Ŭ�� up");
            // ����� ����
            GenerateBamsongi();
            GameManager.Instance.CickUp();
        }
    }

    // Ŭ������ �� power ���� �Լ�
    // ������ ���� �� �Դٰ��� �ϸ� power�� �پ����� �þ�� �Ѵ�. 
    // �������� ���������� ���콺�� ���� �ش� currentPower�� �߻� �Ҽ� �ֵ��� Power ���� �Լ�
    void SetSpeed()
    {
        if (_isClick) // Ŭ�� ���� �ÿ��� ����
        {
            int pingPongSpeed = 15;
            // 1~maxPower���� �Դٰ��� �Ѵ�.
            _multiplier = Mathf.PingPong(Time.time * pingPongSpeed, _maxMultiplier); 
            Debug.Log(_multiplier);
            
            //��� ���� �� �ش� ���� basicPower�� ���Ѵ�.
            _currentPower = _multiplier * _basicPower;

            // UI ������ ���� currentPower�� maxPower�� �� �ۼ�Ʈ���� ���
            float powerRate = _currentPower / _maxPower;

            // UI Ȱ��ȭ �� ����
            GameManager.Instance.SetPowerUI(powerRate);
        }
    }

    // ���� ����� ���� �Լ�
    void GenerateBamsongi()
    {
        GameObject bamsongi = Instantiate(bamsongiPrefab);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldDir = ray.direction;
        // ���翡���� worldDir.nomalized �� �Ͽ� ���⺤�͸� ��������
        // �̹� ray.direction�� ����ȭ�� ���͸� return�ϱ⿡ ���ʿ��ϴ�.
        bamsongi.GetComponent<BamsongiController>().Shoot(worldDir.normalized * _currentPower);

        // �߻� �� Ŭ�� �Ͻ��� ����
        // ���� Ÿ�� Ȥ�� ������Ʈ�� ������ true ����
        GameManager.Instance.CanClick = false;
    }
}