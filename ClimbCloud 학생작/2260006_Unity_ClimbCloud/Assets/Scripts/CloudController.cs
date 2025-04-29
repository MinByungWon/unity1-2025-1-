/*
 * 구름이 아래서 위로 올라가는 기능을 수행함(플레이어가 하늘에서 떨어지는 듯한 착시를 유도)
 * 구름이 화면 밖으로 나가면 구름 오브젝트를 소멸시킨다.
 */
using UnityEngine;

public class CloudController : MonoBehaviour
{
    
    [Header("구름 이동 속도")]
    [SerializeField] private float fMoveSpeed = 1.0f;           //구름이 위로 이동하는 속도 (Inspector에서 조절 가능)
    [SerializeField] private float fRandomMoveRange = 0.5f;     //구름의 흔들림을 구현하기 위한 랜덤 움직임 범위 변수
    
    public float moveSpeed
    { get { return fMoveSpeed; } set { fMoveSpeed = value; } }
    

    float fRandXOffset = 0.0f; //구름의 X축 흔들림 오프셋(변위차) 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        /*
         * Mathf.Sin(Time.time)
         * -------------------------------------------------------------
         * - Sin(사인) 함수는 부드럽게 반복되는 곡선형 값을 생성한다.
         * - Time.time을 입력값으로 사용하면, 게임이 실행된 시간에 따라 값이 계속 변화한다.
         * 
         * 예) 시간에 따라 0 → 1 → 0 → -1 → 0을 반복하며
         *     구름이 부드럽게 흔들리게 된다.
         * -------------------------------------------------------------
         * 사인(Sine)파 형태의 값으로 부드러운 구름의 좌우 흔들림을 주기 위해 사용
         */
        fRandXOffset = Mathf.Sin(Time.time) * fRandomMoveRange; 

        transform.Translate(new Vector3(fRandXOffset, fMoveSpeed, 0.0f) * Time.deltaTime);
        //x축: fRandXOffset, y축: fMoveSpeed, z축: 0.0f * 시간
        //즉, 시간에 따라 좌우로 흔들리며 이동 속도 만큼 위로 이동함


        /*
         * Destroy 메소드 : 매개변수로 전달한 오브젝트를 삭제
         * 메모리가 낭비되지 않도록 화살이 화면 밖으로 나가면 오브젝트를 소멸시켜야 함
         */
        if (transform.position.y > 5.1f)
        {
            Destroy(gameObject); //구름이 화면 밖으로 나가면 구름 오브젝트를 소멸시킨다.
        }
    }

}
