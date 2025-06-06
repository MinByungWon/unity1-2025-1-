using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject m_player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어의 포지션 벡터값을 받아옴
        Vector3 vPlayerPos = m_player.transform.position;

        //x와 z의 값은 그대로 한채로 플레이어의 Vector값을 새로운 포지션으로 지정
        transform.position = new Vector3(transform.position.x, vPlayerPos.y, transform.position.z);
    }
}
