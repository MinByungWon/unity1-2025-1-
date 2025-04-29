using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float fMaxPositionX = 6.0f;
    float fMinPositionX = -6.0f;
    float fPositionX = 0.0f;
    //각각 좌,우로 이동할 수 있는 최대치, 현재 위치를 나타냄

    






    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)&& !GameDirector.Instance._isDead)
        {
            transform.position += new Vector3(-3,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !GameDirector.Instance._isDead)
        {
            transform.position += new Vector3(3, 0, 0);
        }
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
        //플레이어가 죽어있지 않고, 좌우 화살표를 눌렀을 때 캐릭터를 이동시킴

    }
    public void LButtonDown()
    {
        if (!GameDirector.Instance._isDead)
            transform.position += new Vector3(-3, 0, 0);
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
        //플레이어가 죽어있지 않고, 좌 버튼을 눌렀을 때 캐릭터를 이동시킴
    }
    public void RButtonDown()
    {
        if (!GameDirector.Instance._isDead)
            transform.position += new Vector3(3, 0, 0);
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
        //플레이어가 죽어있지 않고, 우 버튼을 눌렀을 때 캐릭터를 이동시킴
    }


    /*
     Clamp는 현재 값, 최소값, 최대값을 받아 행동 반경을 지정할 수 있음
     
     
     */

}
