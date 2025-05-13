using UnityEngine;

public class CloudMove : MonoBehaviour
{
    int nCloudMove = 1;     // 구름의 이동 방향을 표시
    float moveSpeed = 2.0f;// 이동 속도
    private Vector3 startPos;
    private bool movingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 구름의 이동 명령
        transform.Translate(nCloudMove * moveSpeed * Time.deltaTime, 0, 0);

        // x 포지션이 1.7이상, -1.7이하가 되면 반대방향으로 바꾸게 변경
        if (transform.position.x > 1.6 || transform.position.x < -1.6)
        {
            nCloudMove *= -1;
        }
    }
}