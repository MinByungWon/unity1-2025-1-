
/*
   Subject : 오브젝트를 배치하고 움직이는 방법
    GameAlgorithms : 화면 위에 큰 롤렛이 보이고, 화면을 탭하면 롤렛이 회전한다. 시간이 흐르면 회전 속도가 떨어지면서 롤렛이 멈춘다.
     - 1단계 : 화면에 놓일 오브젝트를 모두 나열
     - 2단계 : 오브젝트를 움직일 수 있는 컨트롤러(Controller) 스크립트 작성
     - 3단계 : 오브젝트를 자동으로 생성할 수 있도록 제너레이터(Generator) 스크립트 작성
     - 4단계 : UI를 갱신할 수 있도록 감독(Director) 스크립트 작성
   ProjectName : Roulette
   SceneName : GameScene
   ClassName : RouletteController
*/

/*
 자료구조란, 데이터를 구조적으로 표현하고, 구현하는 중요한 알고리즘
 컬렉션(Collection)은 C#에서 지원하는 자료구조 클래스
    컬렉션의 종류는 ArrayList, Queue, Stack, Hashtable 등이 있음
    컬렉션은 object 형식을 사용하여 데이터를 관리하기 때문에, 박싱(Boxing)과 언방식(Unboxing)이 발생
    그래서 컬렉션을 많이 사용하게 되면, 프로그램의 성능 저하가 발생
    성능 저하 이유로 현재 C#에서는 컬렉션은 잘 사용하지 않음
    이러한 컬렉션의 성능 저하 문제 때문에 대신 사용하는 것이 제네릭 컬렉션임
    제네릭 컬렉션은 데이터 형식을 일반화하여 사용하기 때문에 컬렉션에 비해 성능의 문제가 적다.
    제네릭 컬렉션은 using 지시문으로  System.Collections.Generic를 선언해 주어야 하는데, 유니티에서 새 C# 스크립트를 만들면, 자동적으로 선언됨
    첫줄의 using 은 다른 lib의 코드를 import하는 기능이며 public class 는 객체 역할을, 마지막으로 void Start() 는 메소드를 의미
    System.Collections와 System.Collections.Generic은 데이터를 저장하는 자료구조형을 제공
*/

using System.Collections;
using System.Collections.Generic;

// UnityEngine은 유니티가 동작하는 데 필요한 기능을 제공
using UnityEngine;

/*
 C#으로 작성한 프로그램은 클래스 단위로 관리
    클래스명 = 스크립트명으로 기억
    모든 스크립트가 상속받는 기본 클래스입니다.
 : MonoBehaviour 상속
     MonoBehaviour 클래스는 기본적으로 모든 Unity 스크립트가 파생되는 기본 클래스
     Unity의 프로젝트 창에서 C# 스크립트를 생성하면 MonoBehaviour에서 자동으로 상속되며, 템플릿 스크립트를 제공
     즉, 유니티에서 게임 오브젝트에 스크립트와 연결을 제공하기 위한 것
          Start( ): 게임 오브젝트가 존재하기 시작할 때 호출
          Update( ): 프레임마다 호출
          FixedUpdate( ): 물리 타임스텝마다 호출
          OnBecameVisible(), OnBecameInvisible() : 게임 오브젝트 렌더러가 카메라의 뷰에 들어오거나 나갈 때 호출
          OnCollisionEnter(), OnTriggerEnter() : 물리 충돌 또는 트리거가 발생할 때 호출
          OnDestroy() : 게임 오브젝트가 파괴될 때 호출
    이벤트 함수 실행 순서(Execution Order of Event Functions)
          https://docs.unity3d.com/kr/530/Manual/ExecutionOrder.html
*/
public class RouletteController : MonoBehaviour
{
    /*
         멤버변수 선언
            헝가리안 표기법(Hungarian Notation) : 프로그래밍 언어에서 변수 및 함수의 이름 앞에 데이터 타입을 명시하는 코딩 규칙이다.
            변수 : pascalCase
               int  --> n           예시) nPascalCase
               double   --> db      예시) dbPascalCase
               float --> f          예시) fPascalCase
               char --> ch          예시) chPascalCase
               string  --> str      예시) strPascalCase
               bool --> is          예시) isPascalCase
    */
    float fRouletteRotationSpeed = 0.0f; //롤렛 회전속도 조절 멤버변수

    /*
     Start is called before the first frame update
     Start 메서드
        미리 정의된 특수 이벤트 함수로써, 이 특수 함수들을 C#에서는 함수를 메소드라고 함
        MonoBehaviour 클래스가 초기화 될 때 호출 되는 이벤트 함수
        프로그램이  시작할 때 한 번만 호출이 되는 함수로 보통 컴포넌트를 받아오거나 업데이트나 다른 함수에서 사용하기 위해 초기화해 주는 기능
        즉, Start() 메서드는 스크립트 인스턴스가 활성화된 경우에만 첫 번째 프레임 업데이트 전에 호출하므로 한번만 실행
        씬 에셋에 포함된 모든 오브젝트에 대해 Update 등 이전에 호출된 모든 스크립트를 위한 Start 함수가 호출
        따라서 게임플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않음
    */
    void Start()
    {
        // 디바이스 성능에 따른 실행 결과의 차이 없애기
        //    어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리
        //    스마트폰은 60, 고속의 PC는 300이 될 수 있는 디바이스 성능에 따라 게임 동작에 영향을 미칠 수 있음
        //    프레임레이트를 60으로 고정
        Application.targetFrameRate = 60;

    }

    /*
     Update is called once per frame
     Update 메서드
       매 프레임 마다 호출이 되는 함수로 자신의 컴퓨터가 60 프레임이라면 초당 60번 실행이 되는 함수
       게임 실행에 필요한 함수들은 update() 함수에서 실행하도록 코딩 함
       프레임당 1회 호출함
       불규칙적으로 실행함(물리엔진 충돌검사 등이 제대로 안될 수 있음)
       주로 단순한 타이머, 키 입력을 받을 때 사용함
    */
    void Update()
    {
        /*
        클릭되었는지 확인하는 메서드 --> Input.GetMouseButtonDown
            검색창 --> 유니티 마우스 입력 받기
            마우스로 클릭값 입력받는 메서드
            0:마우스 왼쪽 버튼 클릭, 1:마우스 오른쪽 버튼 클릭, 2:마우스 휠 클릭

            GetMouseButtonDown 메서드 : 마우스 클릭 순간값
            if (Input.GetMouseButtonDown(0)) 
            { 
                 // 마우스 왼쪽 버튼을 눌렀을 때의 처리문 
            }

            GetMouseButtonUp 메서드 : 마우스 왼쪽 버튼에서 손가락을 뗀 순간값
            if (Input.GetMouseButtonUp(0))
            { 
                    // 마우스 왼쪽 버튼을 뗄 때의 처리문
            }

            GetMouseButton 메서드 : 마우스 왼쪽 버튼을 손가락으로 누르고 있는 동안값
            if (Input.GetMouseButton(0)) 
            { 
                    // 마우스 왼쪽 버튼을 누르고 있는 도중의 처리문 
            }
        */

        if (Input.GetMouseButtonDown(0))
        {
            // 왼쪽 마우스를 클릭하면 룰렛은 매 프레임 10도씩 회전
            //    클릭하면 회전 속도를 멤버변수 fRouletteRotationSpeed에 설정한다.
            fRouletteRotationSpeed = 10;

            // 효과음을 재생하려면 AudioSource 컴포넌트의 Play 메서드를 호출
            GetComponent<AudioSource>().Play();

            // 롤렛이 회전할 때 확 퍼지는 이펙트(파티클) 표시를 위해서 ParticleSystem 컴퍼넌트의 Play 메서드를 호출
            GetComponentInParent<ParticleSystem>().Play();

        }

        /*
           오브젝트를 회전하는 메서드 --> Rotate
           Rotate 메서드는 게임 오브젝트를 현재 각도에서 인수 값만큼 회전함
             그러므로 회전 속도만큼 롤렛을 회전시킨다.
           Rotate 메서드에 전달되는 인수는 차례대로 X축, Y축, Z축이며,
             롤렛게임에서는 Z축(화면안쪽으로 향하는 축)을 중심으로 회전시켜야 하므로 회전 값을 세 번째 인수로 지정
             인수에 전달하는 회전 값이 양수이면 시계 반대 방향, 음수이면 시계 방향으로 회전
        */
        transform.Rotate(0, 0, fRouletteRotationSpeed);

        /*
           롤렛을 회전 감속시키기 : 감쇠계수 활용
           감쇠 계수는 값을 변경하는 것만으로도 감속 크기를 쉽게 바꿀 수 있어
           공기 저항에 따른 감속이나 스프링 진동 감쇠 등 다양한 장면에서 사용됨
           10 -> 10*0.98 = 9.8 -> 9.8*0.98 = 9.6 -> 9.6*0.98 = 9.4 .......
        */

        // fRouletteRotationSpeed = fRouletteRotationSpeed * 0.98f;
        fRouletteRotationSpeed *= 0.98f;

    }
}
