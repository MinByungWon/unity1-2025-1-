using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public GameObject gPlanetPrefab = null; // 행성 Prefab을 넣을 빈오브젝트 상자 선언(중요)

    GameObject gPlanetInstance = null; //행성 인스턴스 저장 변수

    float fPlanetCreateSpan = 1.5f;// 행성 생성 변수 : 행성을 1초마다 생성 변수 
    float fDeltaTime = 0.0f;    //앞 프레임과 현재 프레임 사이의 시간 차이를 저장하는 변수


    int nPlanetPositionRange = 0; // 행성의 X 좌표 Range 저장 변수 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Updte 메서드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 차이는 Time.deltaTime에 대입됨
        // Time.deltaTime은 한 프레임 당 실행하는 시간 을 뜻하는데 값을 float 형태로 반환하고, 단위는 초를 사용함
        // 즉, 프레임과 프레임 사이의 시간 차이를 fDeltaTime 변수에 누적
        fDeltaTime += Time.deltaTime;

        // 행성을 1초(fArrowCreateSpan = 1.0f)마다 한 개씩 생성
        // 프레임당 누적 시간이 1초가 넘으면, 행성 생산
        if (fDeltaTime > fPlanetCreateSpan)
        {
            fDeltaTime = 0.0f;  // 프레임과 프레임 사이의 시간 차이 누적 변수 초기화

            /*
             * Instantiate 메서드 : 행성 프리팹을 이용하여, 행성 인스턴스를 생성하는 메서드 
             * - 매개변수로 프리팹을 전달하면, 반환값으로 프리팹 인스턴스를 돌려준다
             * - Instantiate 메서드를 사용하면 게임을 실행하는 도중에 게임오브젝트를 생성할 수 있음
             * - RPG 게임이라면 수많은 아이템, 캐릭터, 배경 등 모든것들을 어떻게 미리 만들어놓을 수 있을까?
             *      그러므로 게임오브젝트의 복제본을 생성
             * - Instantiate(GameObject original, Vector3 position, Quaternion rotation)
             *  -- GameObject original : 생성하고자 하는 게임오브젝트명, 현재 씬에 있는 게임오브젝트나 Prefab으로 선언된 객체를 의미함
             *  -- Vector3 position : Vector3으로 생성될 위치를 설정함
             *  -- Quaternion rotation : 생성될 게임오브젝트의 회전값을 지정
             */

            gPlanetInstance = Instantiate(gPlanetPrefab);

            /*
             * Random 클래스는 흔히 요구되는 다양한 타입의 램덤 값을 쉽게 생성할 수 있는 방법을 제공
             * Random.Range 메서드 : 사용자가 제공한 최솟값과 최갯값 사이의 임의의 숫자를 제공함
             * - 제공된 최솟값과 최대값이 정수인지 실수인지에 따라 정수 또는 실수를 반환함
             * - 첫 번째 매개변수보다 크거나 같고, 두 번째 매개변수보다 작은 범위에서 무작위 수를 정수로 반환
             * - 화살의 X 좌표는 -6 ~ 7 사이에 불규칙하게 위치
             */

            nPlanetPositionRange = Random.Range(-3, 3);

            gPlanetInstance.transform.position = new Vector3(nPlanetPositionRange, 20, 0);

        }
    }
}
