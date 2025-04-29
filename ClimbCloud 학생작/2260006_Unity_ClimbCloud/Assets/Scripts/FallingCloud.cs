/*
 * FallingCloud
 * Ư�� ������ ������ �Ʒ��� ��������.
 * �ð��� ������ �����ǰ� ���� ��ġ�� �ٽ� ������ �����.
 */
using System.Collections;
using UnityEngine;

public class FallingCloud : MonoBehaviour
{
    /*
     * [Unity Documentation]
     * ��������Ʈ ������(Sprite Renderer) ������Ʈ�� ��������Ʈ �� �������ϰ� ��������Ʈ�� 2D �� 3D ������Ʈ�� ���� �ð������� ǥ�õǴ� ����� �����մϴ�.
     * Colliders 2D ������Ʈ�� ������ �浹�� ���� 2D ���� ������Ʈ�� ����� �����մϴ�.
     */

    Vector3 vCloudOriginPos = Vector3.zero; //������ ���� ��ǥ���� �����ϴ� ���� ����

    //���� ������Ʈ�� �����ϴ� ������Ʈ ����� ����ϱ� ���� ���� �ʱ�ȭ [Sprite Renderer, Box Collider 2D, Rigidbody 2D]
    Rigidbody2D m_rigid2DCloud = null;      //Cloud ������Ʈ�� Rigidbody2D ������Ʈ�� ���� ��� ����(m_)
    SpriteRenderer m_spriteRenderer = null; //Cloud ������Ʈ�� Sprite Renderer ������Ʈ�� ���� ��� ����
    Collider2D m_collider2d = null;         //Cloud ������Ʈ�� Box Collider 2D ������Ʈ�� ���� ��� ����
                                            //Collider2D�� ������ ������ Box Collider 2D�� Collider2D�� ��ӹ޴� Ŭ�����̹Ƿ� ��� 2D �浹ü�� ȣ�� ����

    //���� ���̼��� �츮�� ���� �ӽ÷� SerializeField ����
    [SerializeField] float fFallDelay = 0.5f;       //���� �����ð� ����
    [SerializeField] float fFallSpeed = 2.0f;       //���� �ӵ� ����
    [SerializeField] float fDisappearDelay = 1.5f;  //���� �� ������������ �ð� ����
    [SerializeField] float fAppearDelay = 3.0f;     //�ٽ� ��Ÿ�� �����ð� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vCloudOriginPos = transform.position; //������ ���� ��ǥ���� ����

        //Ư�� ������Ʈ�� ������Ʈ�� �����ϱ� ���ؼ��� GetComponent �Լ��� ���
        m_rigid2DCloud = GetComponent<Rigidbody2D>();       //Rigidbody2D ������Ʈ �ҷ�����
        m_spriteRenderer = GetComponent<SpriteRenderer>();  //SpriteRenderer ������Ʈ �ҷ�����
        m_collider2d = GetComponent<Collider2D>();          //Box Collider 2D ������Ʈ �ҷ�����

        /*
         * Rigidbody2D�� BodyType ������ ������
         * Dynamic : �Ϲ����� ���� ������Ʈ (�߷�, �浹 ���� ����)
         * Kinematic : ������ ���������� �浹 ���� ����
         * Static : ���� ����, ���� �������� ����
         * FallingCloud�� ó������ �������� �ʰ� ���߿� �� �־�� �ϹǷ� Kinematic
         * �÷��̾�� �浹�ϸ� Dynamic���� �����Ͽ� �߷¿� ������ �޵��� ����
         */
        m_rigid2DCloud.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�÷��̾ ������ �浹�� ��� ȣ���� �޼ҵ�
    //PlayerController���� ȣ�� �����̹Ƿ� public ��������
    public void f_ActiveFallingCloud()
    {
        StartCoroutine(Interface_FallingRoutine()); //IEnumerator(������ �������̽�) ���� ���ǵ� ��ƾ�� ������
    }

    /* ---------------------------------------------------------------------------------------------------
     * [�ڵ� �ۼ��� ������ �ڷ�]
     * [Unity Documentation]
     * �ڷ�ƾ(Coroutines)�� ����ϸ� �۾��� �ټ��� �����ӿ� �л��� �� �ֽ��ϴ�. 
     * Unity���� �ڷ�ƾ�� ������ �Ͻ� �����ϰ� ��� Unity�� ��ȯ������ �ߴ��� �κп��� ���� �������� ����� �� �ִ� �޼����Դϴ�.
     * �ڷ�ƾ�� Ienumerator ��ȯ Ÿ�԰� �ٵ� ��򰡿� ���Ե� yield ��ȯ������ �����ϴ� �޼����Դϴ�. 
     * yield return null ������ ������ �Ͻ� �����ǰ� ���� �����ӿ��� �ٽ� ���۵Ǵ� �����Դϴ�.
     * �ڷ�ƾ ������ �����Ϸ��� ������ ���� StartCoroutine �Լ��� ����ؾ� �մϴ�.
       
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(Fade());
        }

     * Update(), Coroutine() ��� �ݺ��۾��� ������ �� ������, Update()�� �� �����Ӹ��� �ݺ��Ǿ� �����Ӹ��� ������ �ʿ��� �ڵ尡 �ƴѰ��
     * ��ȿ������ �۵��� �ϰԵȴ�. Coroutine�� Ư�� �ڵ尡 Ư�� �ð��� �ݺ��Ǵ� ���� �ʿ��� ��� ���Ǹ�, �ʿ����� ���� ��� �ڵ带 ������ �� �ִ�.

     * �⺻������ Unity�� yield �� ������ �����ӿ� �ڷ�ƾ�� �ٽ� �����մϴ�.
     * �����ڴ� �� �� �̻��� yield ��ȯ���� �����ؾ� �Ѵ�. yield�� ��ȯ�ϴ� ���� '�Ͻ������� CPU ������ �ٸ� �Լ��� �����Ѵ�'��� ���̴�.
     * '�����Ѵ�'�� ���� �߿��ѵ�, �Ϲ����� �Լ��� ��ȯ�ϴ� ��� �Լ��� ������ ������ ���ε�, 
     * �����ڴ� ������ ��� �����ϴ� ���̱� ������ �ٸ� �Լ��� ������ �ѱ���� �ڽ��� �����ϰ� �ִ� ���¸� ����ϰ� �ִ�.
     * �Ϲ����� �Լ���� �ƹ��� ȣ���ϴ��� return ������ �ڵ�� ����� ���� ������, 
     * �����ڴ� ȣ���� ������ ������ ������ ������ �������� �ٽ� �ڵ带 �����Ѵ�.
     * ---------------------------------------------------------------------------------------------------
     * ����Ƽ���� IEnumerator�� �۾��� �����Ͽ� �����ϴ� �޼ҵ��̴�.
     */

    IEnumerator Interface_FallingRoutine()
    {
        yield return new WaitForSeconds(fFallDelay); //fFallDelay��ŭ ���

        m_rigid2DCloud.bodyType = RigidbodyType2D.Dynamic;
        m_rigid2DCloud.linearVelocity = new Vector2(0.0f, -fFallSpeed);

        yield return new WaitForSeconds(fDisappearDelay);

        //SetActive(false)�� ������Ʈ ��ü�� ��Ȱ��ȭ
        //������Ʈ�� .enabled = false�� Ư�� ��ɸ� ��Ȱ��ȭ, ������Ʈ ��ü�� ��Ȱ��ȭ �� �ʿ�� ���⿡ ���
        m_spriteRenderer.enabled = false;
        m_collider2d.enabled = false;

        yield return new WaitForSeconds(fAppearDelay);

        m_rigid2DCloud.bodyType = RigidbodyType2D.Kinematic;    //�߷� ������ ���� �ʰ� ����
        m_rigid2DCloud.linearVelocity = Vector2.zero;           //�߶����̾����Ƿ� ������ ���ӵ��� 0���� �ʱ�ȭ

        transform.position = vCloudOriginPos; //������ ���� ��ǥ������ ������ ��ġ�� ����


        m_spriteRenderer.enabled = true;
        m_collider2d.enabled = true;
    }
}
