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
        // ���� ���ῡ ���߾��ٸ�, GameManager���� �˷� ī�޶� ��ȯ
        // �±׵� �߿�������, �ϳ��� �߿��� ���� �����鿡 Bamsongi ���̾ �������ְ�
        // Layer Collision Martix ���� Bamsongi ���̾�� �浹�ȵǰ� ����
        // ������ ǥ���� ����̰� �پ�������, �� ����� ���� ���߸� �Ʒ� ������ �ȳ��´�.
        // �� ����� ���� �浿�� �����ϰ� Ÿ����ġ�� Ÿ�ٿ� ������ �ְ� ����
  
        if (collision.gameObject.CompareTag("Target"))
        {
            GameManager.Instance.HitTarget();
        }
        else
        {
            if (GameManager.Instance.RestCount > 0)
            {
                // �ٽ� �߻� �� �� �ְ� ����
                // Target�� ���� �ÿ��� ���� ī�޶� ��ȯ�� CameraController.cs���� �������ش�.
                GameManager.Instance.CanClick = true;
            }
            else
            {
                // Ÿ�� ���� �� GameEnd ȣ���� CameraController.cs���� �Ѵ�.
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