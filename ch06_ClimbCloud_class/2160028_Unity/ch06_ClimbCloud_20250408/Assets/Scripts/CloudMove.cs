using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed = 1f;

    private float minx = -1.5f;
    private float maxx = 1.5f;

    private float moveRange;
    private float offsety;
    private float offsetz;

    public bool Move = false;

    void Start()
    {
        // 태그가 "MoveCloud"인 경우에만 움직임
        if (CompareTag("MoveCloud"))
        {
            Move = true;        //움직임 활성화
        }

        if (Move)
        {
            // 현재 x의 좌표가 -1.5 ~ 1.5 범위 안에 있는지 확인, 제한
            float startX = Mathf.Clamp(transform.position.x, minx, maxx);

            // 이동 가능한 거리 계산
            moveRange = (maxx - minx);

            // y,z 좌표는 고정(값 저장)
            offsety = transform.position.y;
            offsetz = transform.position.z;

            // x좌표를 minx, maxx 사이에 설정
            transform.position = new Vector3(startX, offsety, offsetz);
        }
    }

    void Update()
    {
        if (Move)
        {
            // Mathf.PingPong 함수를 사용해 minx, maxx의 범위 안에서 반복
            float x = Mathf.PingPong(Time.time * speed, moveRange) + minx;

            // x 값은 변화, y,z는 값 변화 x
            transform.position = new Vector3(x, offsety, offsetz);
        }
    }
}
