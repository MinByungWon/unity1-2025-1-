using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiController : MonoBehaviour
{
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();
        // 만약 과녁에 맞추었다면, GameManager에게 알려 카메라 전환
        // 태그도 중요하지만, 하나더 중요한 것은 프리펩에 Bamsongi 레이어를 지정해주고
        // Layer Collision Martix 에서 Bamsongi 레이어끼리 충돌안되게 설정
        // 이유는 표적위 밤송이가 붙어있을때, 그 밤송이 위에 맞추면 아래 판정이 안나온다.
        // 즉 밤송이 끼리 충동은 무시하고 타겟위치면 타겟에 맞을수 있게 설정
  
        if (collision.gameObject.CompareTag("Target"))
        {
            GameManager.Instance.HitTarget();
        }
        else
        {
            if (GameManager.Instance.RestCount > 0)
            {
                // 다시 발사 할 수 있게 설정
                // Target에 맞을 시에는 따로 카메라 전환후 CameraController.cs에서 설정해준다.
                GameManager.Instance.CanClick = true;
            }
            else
            {
                // 타겟 맞을 시 GameEnd 호출은 CameraController.cs에서 한다.
                GameManager.Instance.GameEnd();
            }
            
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        // Shoot(new Vector3(0, 200, 2000));
    }
}