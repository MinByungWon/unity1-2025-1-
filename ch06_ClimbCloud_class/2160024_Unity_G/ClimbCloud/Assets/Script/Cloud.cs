using Unity.Hierarchy;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject cloudPrefab; // 랜덤 생성할 구름 프리팹
    public GameObject cloudMovePrefab; // 구름 프리팹
    float spawnInterval = 2f; // 생성 간격
    float minX = -2.0f; // X축의 최소범위
    float maxX = 2.0f; // X축의 최대범위
    float yoffset = 1f;  // 플레이어 위로 구름이 생성되는 범위 값

    int movDefCloudCount = 0; // 움직이는 구름 생성 스택 

    public Transform player;

    


    float nextSpawnY = 0f; // 다음 구름위치를 갱신하는 코드

    void Start()
    {
        nextSpawnY = player.position.y + yoffset;
        //다음 구름 생성 주기 Y 좌표
    }

    // Update is called once per frame
    void Update()
    {
        
        // 매 프레임마다 플레이어의 위치를 확인해서, 플레이어가 nextSpawnY보다 올라갔다면 구름을 하나 생성한다.
        if (player.position.y + yoffset > nextSpawnY)
        {
            if (movDefCloudCount > 2) // 3번째 구름마다
            {
                CreateMovingCloud();
                movDefCloudCount = 0; // 구름 카운트 초기화
                //Debug.Log("움직이는 구름 생성, cloud count = "+ movDefCloudCount);
            }
            else
            {
                CreateDefaultCloud(); // 구름 생성 함수
                movDefCloudCount++; // 구름 카운트 증가
                //Debug.Log("기본 구름 생성, cloud count = "+ movDefCloudCount);
            }
                
            nextSpawnY += spawnInterval; // 다음 구름의 생성위치를 spawnInterval 만큼 위에서 생성한다
            //Debug.Log($"[Update] Created cloud at nextSpawnY: {nextSpawnY}");
        }



        
    }

    void CreateDefaultCloud()
    {
        // minX maxX 사이의 좌표값
        float randomX = Random.Range(minX, maxX);
        // 구름을 생성할 랜덤 위치 설정 
        Vector3 spawnPosition = new Vector3(randomX, nextSpawnY, 0f);
        //해당 위치에 구름 프리팹 생성
        Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
    }

    void CreateMovingCloud()
    {
        // minX maxX 사이의 좌표값
        float randomX = Random.Range(minX, maxX);
        // 구름을 생성할 랜덤 위치 설정 
        Vector3 spawnPosition = new Vector3(randomX, nextSpawnY, 0f);
        //해당 위치에 구름 프리팹 생성
        Instantiate(cloudMovePrefab, spawnPosition, Quaternion.identity);
    }



}
