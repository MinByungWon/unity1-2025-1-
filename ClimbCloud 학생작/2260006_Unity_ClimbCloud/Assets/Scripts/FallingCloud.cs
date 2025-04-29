/*
 * FallingCloud
 * 특정 구름은 밟으면 아래로 내려간다.
 * 시간이 지나면 삭제되고 원래 위치에 다시 구름이 생긴다.
 */
using System.Collections;
using UnityEngine;

public class FallingCloud : MonoBehaviour
{
    /*
     * [Unity Documentation]
     * 스프라이트 렌더러(Sprite Renderer) 컴포넌트는 스프라이트 를 렌더링하고 스프라이트가 2D 및 3D 프로젝트의 씬에 시각적으로 표시되는 방식을 제어합니다.
     * Colliders 2D 컴포넌트는 물리적 충돌을 위한 2D 게임 오브젝트의 모양을 정의합니다.
     */

    Vector3 vCloudOriginPos = Vector3.zero; //구름의 원래 좌표값을 저장하는 벡터 변수

    //구름 오브젝트에 존재하는 컴포넌트 기능을 사용하기 위해 변수 초기화 [Sprite Renderer, Box Collider 2D, Rigidbody 2D]
    Rigidbody2D m_rigid2DCloud = null;      //Cloud 오브젝트의 Rigidbody2D 컴포넌트를 갖는 멤버 변수(m_)
    SpriteRenderer m_spriteRenderer = null; //Cloud 오브젝트의 Sprite Renderer 컴포넌트를 갖는 멤버 변수
    Collider2D m_collider2d = null;         //Cloud 오브젝트의 Box Collider 2D 컴포넌트를 갖는 멤버 변수
                                            //Collider2D로 선언한 이유는 Box Collider 2D는 Collider2D를 상속받는 클래스이므로 모든 2D 충돌체를 호출 가능

    //편집 용이성을 살리기 위해 임시로 SerializeField 선언
    [SerializeField] float fFallDelay = 0.5f;       //낙하 지연시간 변수
    [SerializeField] float fFallSpeed = 2.0f;       //낙하 속도 변수
    [SerializeField] float fDisappearDelay = 1.5f;  //낙하 후 사라지기까지의 시간 변수
    [SerializeField] float fAppearDelay = 3.0f;     //다시 나타낼 지연시간 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vCloudOriginPos = transform.position; //구름의 원래 좌표값을 저장

        //특정 오브젝트의 컴포넌트에 접근하기 위해서는 GetComponent 함수를 사용
        m_rigid2DCloud = GetComponent<Rigidbody2D>();       //Rigidbody2D 컴포넌트 불러오기
        m_spriteRenderer = GetComponent<SpriteRenderer>();  //SpriteRenderer 컴포넌트 불러오기
        m_collider2d = GetComponent<Collider2D>();          //Box Collider 2D 컴포넌트 불러오기

        /*
         * Rigidbody2D의 BodyType 종류와 차이점
         * Dynamic : 일반적인 물리 오브젝트 (중력, 충돌 감지 포함)
         * Kinematic : 물리는 무시하지만 충돌 감지 가능
         * Static : 완전 고정, 절대 움직이지 않음
         * FallingCloud는 처음부터 떨어지지 않고 공중에 떠 있어야 하므로 Kinematic
         * 플레이어와 충돌하면 Dynamic으로 변경하여 중력에 영향을 받도록 설계
         */
        m_rigid2DCloud.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //플레이어가 구름에 충돌할 경우 호출할 메소드
    //PlayerController에서 호출 예정이므로 public 접근제어
    public void f_ActiveFallingCloud()
    {
        StartCoroutine(Interface_FallingRoutine()); //IEnumerator(열거자 인터페이스) 내에 정의된 루틴을 시작함
    }

    /* ---------------------------------------------------------------------------------------------------
     * [코드 작성시 참고한 자료]
     * [Unity Documentation]
     * 코루틴(Coroutines)을 사용하면 작업을 다수의 프레임에 분산할 수 있습니다. 
     * Unity에서 코루틴은 실행을 일시 정지하고 제어를 Unity에 반환하지만 중단한 부분에서 다음 프레임을 계속할 수 있는 메서드입니다.
     * 코루틴은 Ienumerator 반환 타입과 바디 어딘가에 포함된 yield 반환문으로 선언하는 메서드입니다. 
     * yield return null 라인은 실행이 일시 정지되고 다음 프레임에서 다시 시작되는 지점입니다.
     * 코루틴 실행을 설정하려면 다음과 같이 StartCoroutine 함수를 사용해야 합니다.
       
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(Fade());
        }

     * Update(), Coroutine() 모두 반복작업을 수행할 수 있으나, Update()는 매 프레임마다 반복되어 프레임마다 실행이 필요한 코드가 아닌경우
     * 비효율적인 작동을 하게된다. Coroutine은 특정 코드가 특정 시간에 반복되는 것이 필요할 경우 사용되며, 필요하지 않을 경우 코드를 종료할 수 있다.

     * 기본적으로 Unity는 yield 문 다음에 프레임에 코루틴을 다시 시작합니다.
     * 열거자는 한 개 이상의 yield 반환문을 포함해야 한다. yield로 반환하는 것은 '일시적으로 CPU 권한을 다른 함수에 위임한다'라는 뜻이다.
     * '위임한다'는 말이 중요한데, 일반적인 함수는 반환하는 즉시 함수를 완전히 끝내는 것인데, 
     * 열거자는 권한을 잠시 위임하는 것이기 때문에 다른 함수로 권한을 넘기더라도 자신이 실행하고 있던 상태를 기억하고 있다.
     * 일반적인 함수라면 아무리 호출하더라도 return 이후의 코드는 실행될 수가 없지만, 
     * 열거자는 호출할 때마다 이전에 권한을 위임한 시점부터 다시 코드를 실행한다.
     * ---------------------------------------------------------------------------------------------------
     * 유니티에서 IEnumerator는 작업을 분할하여 수행하는 메소드이다.
     */

    IEnumerator Interface_FallingRoutine()
    {
        yield return new WaitForSeconds(fFallDelay); //fFallDelay만큼 대기

        m_rigid2DCloud.bodyType = RigidbodyType2D.Dynamic;
        m_rigid2DCloud.linearVelocity = new Vector2(0.0f, -fFallSpeed);

        yield return new WaitForSeconds(fDisappearDelay);

        //SetActive(false)는 오브젝트 전체를 비활성화
        //컴포넌트의 .enabled = false는 특정 기능만 비활성화, 오브젝트 전체를 비활성화 할 필요는 없기에 사용
        m_spriteRenderer.enabled = false;
        m_collider2d.enabled = false;

        yield return new WaitForSeconds(fAppearDelay);

        m_rigid2DCloud.bodyType = RigidbodyType2D.Kinematic;    //중력 영향을 받지 않게 변경
        m_rigid2DCloud.linearVelocity = Vector2.zero;           //추락중이었으므로 변수값 가속도를 0으로 초기화

        transform.position = vCloudOriginPos; //구름의 원래 좌표값으로 구름의 위치를 변경


        m_spriteRenderer.enabled = true;
        m_collider2d.enabled = true;
    }
}
