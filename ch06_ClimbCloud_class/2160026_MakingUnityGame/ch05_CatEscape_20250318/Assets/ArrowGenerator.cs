/*
 * ȭ�� ������Ʈ�� 1�ʿ� �� ���� �����ϴ� �˰���
 * Update �޼ҵ�� �����Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ������ Timedelta�� �μ��� �޴´�.
 * �����Ӱ� ������ ������ �ð� ���̸� delta�� ������ 1�� �̻��� �Ǹ� delta ������ ���
 * ������ ���� ������ 1�ʿ� �ѹ��� ȭ���� ����
 * instantiate �޼ҵ� : �μ��� ������ ������Ʈ�� �����ϴ� �޼ҵ�
 * ������ �����ϴ� ���߿� ���� ������Ʈ�� ������ �� ����
 * ȭ�� �������� �̿��Ͽ� ȭ�� �ν��Ͻ��� �����ϴ� �޼ҵ�
 * Random.Range() : �μ��� ������ �� ���� ������ ������ �����ϴ� �޼ҵ�
 * ����ڰ� ������ �ּڰ��� �ִ� ������ ������ ���ڸ� ����
 * ù��° �Ű��������� ũ�ų� ���� �ι�° �Ű��������� ���� �������� ������ ���� �����ϰ� ��ȯ
 */
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    // public���� �� ������ �ν����� â���� ���� ������ �� �ֵ��� �ϱ� ����
    /*
     * arrowPrefab ������ ������ ��ü�� �����ϱ� ���ؼ� public  ���� ������
     * ������� ����� public ���� �����ڸ� ����ϸ� �ν����� â���� Prefab���赵 ������ �� �ֵ��� ��
     * ȭ�� �뷮 ������ ���ؼ� ��� ��迡 �Ѱ� �� Prefab ���赵�� �Ѱ� �־�� ��
     */
    public GameObject gArrowPrefab = null;     // ȭ�� Prefab�� ���� �������Ʈ ���� ����
   
    GameObject gArrowInstance = null;          // ȭ�� �ν��Ͻ� ���� ����
    float fArrowCreateSpan = 1.0f;
    float fDeltaTime = 0;
    int nArrowPositionRange = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update �޼ҵ�� �����Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ���̴� Time.deltaTime�� �μ��� �޴´�.
         * Time.deltaTime�� "�� ������ �� �����ϴ� �ð�"�� �ǹ�, �� float �������� ��ȯ�ϰ� ������ �ʸ� ���
         * ��, �����Ӱ� ������ ������ �ð� ���̸� fDeltaTime�� ����
        */
        this.fDeltaTime += Time.deltaTime;

        // ȭ���� 1��(fArrowCreateSpan)���� �� ���� ����
        // �����Ӵ� �ð����̰� 1�ʸ� �Ѿ�� ȭ���� ����
        if (this.fDeltaTime > this.fArrowCreateSpan)
        {
            this.fDeltaTime = 0.0f;

            //intantiante �޼ҵ� : ȭ�� �������� �̿��Ͽ�, ȭ�� �ν��Ͻ��� �����ϴ� �޼ҵ�
            // �Ű������� �������� �����ϸ� ��ȯ������ ������ �ν��Ͻ��� �����ش�
            // instantiate �޼ҵ带 ����ϸ� ������ �����ϴ� ���߿� ���� ������Ʈ�� ������ �� ����
            // RPG�����̶�� ������ ������, ĳ����, ���� ��� �͵��� ��� �̸� ����� ������ ������?
            // ���� ������Ʈ�� �������� ����
            // instantiate(GameObject original, vector3 position, quaternion rotation) 
            // gameobject original : ������ ������Ʈ
            // vector3 position : ������ ������Ʈ�� ��ġ
            // quaternion rotation : ������ ������Ʈ�� ȸ��
            gArrowInstance = Instantiate( gArrowPrefab );

            // Random.Range() : �μ��� ������ �� ���� ������ ������ �����ϴ� �޼ҵ�
            // ������ �ּڰ��� �ִ��� �������� �Ǽ������� ���� ���� �Ǵ� �Ǽ��� ��ȯ��
            // ù��° �Ű��������� ũ�ų� ���� �ι�° �Ű��������� ���� �������� ������ ���� ������ ��ȯ
            // ȭ���� x��ǥ�� -6~6 ���̿� �ұ�Ģ�ϰ� ����
            nArrowPositionRange = Random.Range(-6, 7);
            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 7, 0);
        }
    }
}
