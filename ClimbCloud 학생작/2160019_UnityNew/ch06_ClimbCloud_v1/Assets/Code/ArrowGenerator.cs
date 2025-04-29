using UnityEngine;

public class DiaGenerator : MonoBehaviour
{
    public GameObject gArrowPrefab = null;  // ȭ�� Prefab�� ���� �� ������Ʈ ���� 
    GameObject gArrowInstance = null;       // ȭ�� �ν��Ͻ� ���� ����

    float fArrowCreateSpan = 3;               // ȭ�� ���� ���� : ȭ���� 3�ʸ��� ���� ����
    float fDeltaTime = 0.0f;                // �� �����Ӱ� ���� ������ ������ �ð� ���̸� �����ϴ� ����

    int nArrowPositionRange = 0;            // ȭ���� X ��ǥ Range ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fDeltaTime += Time.deltaTime;


        if(fDeltaTime > fArrowCreateSpan)
        {
            fDeltaTime = 0.0f;  // �����Ӱ� ������ ������ �ð� ���� ���� ���� �ʱ�ȭ

            gArrowInstance = Instantiate(gArrowPrefab); // public���� ���� ȭ�� prefab�� ����


            nArrowPositionRange = Random.Range(-3, 3);    //x�࿡�� -3���� 3���� �������� ����

            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 46, 0);
        }
    }
}
