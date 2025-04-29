/*
 * ������ �Ʒ��� ���� �ö󰡴� ����� ������(�÷��̾ �ϴÿ��� �������� ���� ���ø� ����)
 * ������ ȭ�� ������ ������ ���� ������Ʈ�� �Ҹ��Ų��.
 */
using UnityEngine;

public class CloudController : MonoBehaviour
{
    
    [Header("���� �̵� �ӵ�")]
    [SerializeField] private float fMoveSpeed = 1.0f;           //������ ���� �̵��ϴ� �ӵ� (Inspector���� ���� ����)
    [SerializeField] private float fRandomMoveRange = 0.5f;     //������ ��鸲�� �����ϱ� ���� ���� ������ ���� ����
    
    public float moveSpeed
    { get { return fMoveSpeed; } set { fMoveSpeed = value; } }
    

    float fRandXOffset = 0.0f; //������ X�� ��鸲 ������(������) ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        /*
         * Mathf.Sin(Time.time)
         * -------------------------------------------------------------
         * - Sin(����) �Լ��� �ε巴�� �ݺ��Ǵ� ��� ���� �����Ѵ�.
         * - Time.time�� �Է°����� ����ϸ�, ������ ����� �ð��� ���� ���� ��� ��ȭ�Ѵ�.
         * 
         * ��) �ð��� ���� 0 �� 1 �� 0 �� -1 �� 0�� �ݺ��ϸ�
         *     ������ �ε巴�� ��鸮�� �ȴ�.
         * -------------------------------------------------------------
         * ����(Sine)�� ������ ������ �ε巯�� ������ �¿� ��鸲�� �ֱ� ���� ���
         */
        fRandXOffset = Mathf.Sin(Time.time) * fRandomMoveRange; 

        transform.Translate(new Vector3(fRandXOffset, fMoveSpeed, 0.0f) * Time.deltaTime);
        //x��: fRandXOffset, y��: fMoveSpeed, z��: 0.0f * �ð�
        //��, �ð��� ���� �¿�� ��鸮�� �̵� �ӵ� ��ŭ ���� �̵���


        /*
         * Destroy �޼ҵ� : �Ű������� ������ ������Ʈ�� ����
         * �޸𸮰� ������� �ʵ��� ȭ���� ȭ�� ������ ������ ������Ʈ�� �Ҹ���Ѿ� ��
         */
        if (transform.position.y > 5.1f)
        {
            Destroy(gameObject); //������ ȭ�� ������ ������ ���� ������Ʈ�� �Ҹ��Ų��.
        }
    }

}
