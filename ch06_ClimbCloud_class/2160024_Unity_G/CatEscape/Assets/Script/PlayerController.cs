using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float fMaxPositionX = 6.0f;
    float fMinPositionX = -6.0f;
    float fPositionX = 0.0f;
    //���� ��,��� �̵��� �� �ִ� �ִ�ġ, ���� ��ġ�� ��Ÿ��

    






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
        //�÷��̾ �׾����� �ʰ�, �¿� ȭ��ǥ�� ������ �� ĳ���͸� �̵���Ŵ

    }
    public void LButtonDown()
    {
        if (!GameDirector.Instance._isDead)
            transform.position += new Vector3(-3, 0, 0);
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
        //�÷��̾ �׾����� �ʰ�, �� ��ư�� ������ �� ĳ���͸� �̵���Ŵ
    }
    public void RButtonDown()
    {
        if (!GameDirector.Instance._isDead)
            transform.position += new Vector3(3, 0, 0);
        fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
        //�÷��̾ �׾����� �ʰ�, �� ��ư�� ������ �� ĳ���͸� �̵���Ŵ
    }


    /*
     Clamp�� ���� ��, �ּҰ�, �ִ밪�� �޾� �ൿ �ݰ��� ������ �� ����
     
     
     */

}
