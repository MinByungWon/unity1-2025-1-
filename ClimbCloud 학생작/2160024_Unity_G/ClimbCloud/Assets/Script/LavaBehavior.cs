using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class LavaBehavior : MonoBehaviour
{
    float flavaSpeed = 0.02f; // Speed of lava movement
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }






    /// <summary>
    /// �ð��� �������� ���� �ö���� ��ϱ���
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.up * flavaSpeed); // Lava moves downwards

        if (transform.position.y > 50f)
        {
            flavaSpeed = 0.04f;
        }
        else if (transform.position.y > 100f)
        {
            flavaSpeed = 0.06f;
        }
    }
}
