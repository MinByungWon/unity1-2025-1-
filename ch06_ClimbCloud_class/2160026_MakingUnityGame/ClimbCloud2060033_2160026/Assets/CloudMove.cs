using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float f_moveSpeed = 1.0f;    // 구름 이동 속도
    private float f_moveRange = 3.0f;     // 이동 범위

    private Vector3 v_startPosition;    // 시작 위치 저장
    private int n_direction = 1;         // 이동 방향 (1=오른쪽, -1=왼쪽)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        v_startPosition = transform.position; // 처음 위치 저장
    }

    // Update is called once per frame
    void Update()
    {
        MoveCloud(); // 구름 이동함수 호출
    }

    public void MoveCloud()
    {
        // 이동
        transform.Translate(Vector3.right * n_direction * f_moveSpeed * Time.deltaTime);

        // 일정 거리 벗어나면 방향 전환
        if (Mathf.Abs(transform.position.x - v_startPosition.x) > f_moveRange)
        {
            n_direction *= -1; // 방향 반전
        }
    }
}
