using UnityEngine;

public class Teleport : MonoBehaviour
{
    // 순간이동할 위치 설정
    private Vector3 teleportPosition = new Vector3(1.0f, 3.5f, 0.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어가 이 오브젝트와 부딪히면
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))             // 플레이어가 아이템에 닿았을 때
        {
            other.transform.position = teleportPosition;    // 플레이어를 순간이동할 위치(teleportPosition)로 시킴
            Destroy(gameObject);                            // 아이템을 먹으면 사라짐(한번만 먹을 수 있도록)
        }
    }
}
