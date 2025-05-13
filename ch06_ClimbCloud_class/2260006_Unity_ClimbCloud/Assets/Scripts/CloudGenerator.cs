/*
 * ���� �ð����� ������ �����ϴ� ��ũ��Ʈ
 */
using System.Transactions;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gCloudPrefab = null; //���� Prefab�� ���� �������Ʈ
    GameObject gCloudInstance = null; //���� �ν��Ͻ� ���� ����

    float fCloudCreateSpan = 2.0f;  //���� ���� ����
    float fCreateYPos = -6.0f;      //���� ���� Y��
    float fCreateTimer = 0.0f;      //���� ������ ���� Ÿ�̸�
    float fCloudPosRange = 0.0f; //������ X�� ���� ���� ����
    float fRandCloudScale = 0.0f;   //���� ���� ũ�� ����

    float fMinXRange = -2.5f;       //X�� ���� ȭ�� ��
    float fMaxXRange = 2.5f;        //X�� ������ ȭ�� ��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update �޼ҵ�� �����Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ������ Time.deltaTime�� ���Ե�
         * Time.deltaTime�� �� ������ �� �����ϴ� �ð��� ���ϴµ�, ���� float ���·� ��ȯ�ϰ� ������ �ʸ� �����
         * ��, �����Ӱ� ������ ������ �ð� ���̸� fDeltaTime ������ ����
         */
        fCreateTimer += Time.deltaTime;

        if(fCreateTimer >= fCloudCreateSpan)
        {
            fCreateTimer = 0.0f; //�����Ӱ� ������ ������ �ð� ���� ���� ���� �ʱ�ȭ
            f_CreateCloud(); //���� ���� �޼ҵ� ȣ��

            //Debug.Log("�������� �޼ҵ� ȣ���");
        }
    }

    void f_CreateCloud()
    {
        gCloudInstance = Instantiate(gCloudPrefab); //�������� ���� �ν��Ͻ� ����
        
        fCloudPosRange = Random.Range(fMinXRange, fMaxXRange);  //���� ��ġ �߻�

        gCloudInstance.transform.position = new Vector3(fCloudPosRange, fCreateYPos, 0.0f);     //���� ��ġ�� �̵�
        gCloudInstance.GetComponent<CloudController>().moveSpeed = Random.Range(0.5f, 2.0f);    //�̵� �ӵ� �������� ����

        fRandCloudScale = Random.Range(0.7f, 1.4f); //���� ũ�� �߻�
        gCloudInstance.transform.localScale = Vector3.one * fRandCloudScale; //���� ũ��� ����
    }
}
