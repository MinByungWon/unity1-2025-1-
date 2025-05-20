using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem; //new InputSystem 사용

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongiPrefab;

    float _basicPower = 300; // 기본 던지는 힘
    float _currentPower = 0; // 기본 힘에서 배수를 곱한 값 
    float _multiplier = 1; // 기본 힘에 곱할 배수 -> SetSpeed에서 정해짐
    float _maxMultiplier = 10; // 최대배수
    float _maxPower = 0; // 최대 파워

    bool _isClick = false;

    private void Start()
    {
        _maxPower = _basicPower * _maxMultiplier; // 최대 스피드 설정
    }

    void Update()
    {
        /*
              if (Input.GetMouseButtonDown(0))
         {
             // 싱글톤 GameManager의 CanClick이 true 이여야만 발사 가능하게 설정
             if (GameManager.Instance.CanClick)
             {
                 GameObject bamsongi = Instantiate(bamsongiPrefab);

                 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 Vector3 worldDir = ray.direction;
                 // 교재에서는 worldDir.nomalized 를 하여 방향벡터를 구하지만
                 // 이미 ray.direction은 정규화된 벡터를 return하기에 불필요하다.
                 // 아래처럼 직접 상수를 넣는 버릇은 들이지 말자
                 bamsongi.GetComponent<BamsongiController>().Shoot(worldDir.normalized * 2000);
             }
         }
         */

        // 마우스 클릭 감지
        ClickDetect();
        // Speed 설정 
        SetSpeed();
    }

    // Update에 로직 적지 말고 기능별로 모듈화 하는 버릇을 키우자
    // Click 감지 함수
    void ClickDetect()
    {
        // 유니티에서 new InputSystem 사용을 지향하므로 한 번씩 InputAction으로
        // 직접 바인딩 하는등, 알아보면 좋다.
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.CanClick)
        {
            if (GameManager.Instance.RestCount <= 0)
            {
                _isClick = false;

                return;
            }
            // 현재프레임에서 마우스가 눌러졌을 시 한번
            // 또한 GameManager 인스턴스의 CanClick이 true 일 때에만
            _isClick = true;
            Debug.Log("클릭 down");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && GameManager.Instance.CanClick)
        {
            // 현재 프레임에서 마우스를 뗏을 시
            _isClick = false;

            Debug.Log("클릭 up");

            // 밤송이 생성
            GenerateBamsongi();

            GameManager.Instance.CickUp();
        }
    }

    // 클릭중일 시 power 설정 함수
    // 누르고 있을 시 왔다갔다 하며 power가 줄어들었다 늘어났다 한다. 
    // 게이지가 높아졌을시 마우스를 떼어 해당 currentPower로 발사 할수 있도록 Power 설정 함수
    void SetSpeed()
    {
        if (_isClick) // 클릭 중일 시에만 설정
        {
            int pingPongSpeed = 15;
            // 1~maxPower까지 왔다갔다 한다.
            _multiplier = Mathf.PingPong(Time.time * pingPongSpeed, _maxMultiplier); 
            Debug.Log(_multiplier);
            
            //배수 설정 시 해당 값을 basicPower에 곱한다.
            _currentPower = _multiplier * _basicPower;

            // UI 설정을 위해 currentPower가 maxPower의 몇 퍼센트인지 계산
            float powerRate = _currentPower / _maxPower;

            // UI 활성화 및 설정
            GameManager.Instance.SetPowerUI(powerRate);
        }
    }

    // 최종 밤송이 생성 함수
    void GenerateBamsongi()
    {
        GameObject bamsongi = Instantiate(bamsongiPrefab);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldDir = ray.direction;
        // 교재에서는 worldDir.nomalized 를 하여 방향벡터를 구하지만
        // 이미 ray.direction은 정규화된 벡터를 return하기에 불필요하다.
        bamsongi.GetComponent<BamsongiController>().Shoot(worldDir.normalized * _currentPower);

        // 발사 후 클릭 일시적 금지
        // 추후 타겟 혹은 오브젝트에 맞을시 true 설정
        GameManager.Instance.CanClick = false;
    }
}