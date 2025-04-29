using Unity.Hierarchy;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject cloudPrefab; // ���� ������ ���� ������
    public GameObject cloudMovePrefab; // ���� ������
    float spawnInterval = 2f; // ���� ����
    float minX = -2.0f; // X���� �ּҹ���
    float maxX = 2.0f; // X���� �ִ����
    float yoffset = 1f;  // �÷��̾� ���� ������ �����Ǵ� ���� ��

    int movDefCloudCount = 0; // �����̴� ���� ���� ���� 

    public Transform player;

    


    float nextSpawnY = 0f; // ���� ������ġ�� �����ϴ� �ڵ�

    void Start()
    {
        nextSpawnY = player.position.y + yoffset;
        //���� ���� ���� �ֱ� Y ��ǥ
    }

    // Update is called once per frame
    void Update()
    {
        
        // �� �����Ӹ��� �÷��̾��� ��ġ�� Ȯ���ؼ�, �÷��̾ nextSpawnY���� �ö󰬴ٸ� ������ �ϳ� �����Ѵ�.
        if (player.position.y + yoffset > nextSpawnY)
        {
            if (movDefCloudCount > 2) // 3��° ��������
            {
                CreateMovingCloud();
                movDefCloudCount = 0; // ���� ī��Ʈ �ʱ�ȭ
                //Debug.Log("�����̴� ���� ����, cloud count = "+ movDefCloudCount);
            }
            else
            {
                CreateDefaultCloud(); // ���� ���� �Լ�
                movDefCloudCount++; // ���� ī��Ʈ ����
                //Debug.Log("�⺻ ���� ����, cloud count = "+ movDefCloudCount);
            }
                
            nextSpawnY += spawnInterval; // ���� ������ ������ġ�� spawnInterval ��ŭ ������ �����Ѵ�
            //Debug.Log($"[Update] Created cloud at nextSpawnY: {nextSpawnY}");
        }



        
    }

    void CreateDefaultCloud()
    {
        // minX maxX ������ ��ǥ��
        float randomX = Random.Range(minX, maxX);
        // ������ ������ ���� ��ġ ���� 
        Vector3 spawnPosition = new Vector3(randomX, nextSpawnY, 0f);
        //�ش� ��ġ�� ���� ������ ����
        Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
    }

    void CreateMovingCloud()
    {
        // minX maxX ������ ��ǥ��
        float randomX = Random.Range(minX, maxX);
        // ������ ������ ���� ��ġ ���� 
        Vector3 spawnPosition = new Vector3(randomX, nextSpawnY, 0f);
        //�ش� ��ġ�� ���� ������ ����
        Instantiate(cloudMovePrefab, spawnPosition, Quaternion.identity);
    }



}
