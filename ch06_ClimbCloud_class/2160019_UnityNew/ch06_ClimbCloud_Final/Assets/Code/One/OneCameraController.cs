using UnityEngine;

public class OneCameraController : MonoBehaviour
{
    GameObject m_player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.m_player = GameObject.Find("Ycat");
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어의 포지션 벡터값을 받아옴
        Vector3 vPlayerPos = this.m_player.transform.position;

        //x와 z의 값은 그대로 한채로 플레이어의 Vector값을 새로운 포지션으로 지정
        transform.position = new Vector3(transform.position.x, vPlayerPos.y, transform.position.z);
    }
}
