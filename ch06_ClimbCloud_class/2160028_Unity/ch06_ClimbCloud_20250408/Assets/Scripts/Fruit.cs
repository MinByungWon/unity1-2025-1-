using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{
    // 모든 과일의 총 개수를 저장(변수 초기화)
    public static int fruitCount = 0;

    private bool isCollected = false;       //과일이 먹혔는지 확인하는 변수(과일을 먹었을 때 개수가 2개씩 차감되는 버그가 있어서 추가)

    // Start is called before the first frame update
    void Start()            
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && fruitCount == 0)       // 씬이 GameScene이고, fruitCount이 초기화 됐을때(떨어졌을때나 게임 다시 시작할때 과일의 개수를 초기화 하기 위해)
        {
            fruitCount = GameObject.FindGameObjectsWithTag("Fruit").Length;             // Fruit 태그를 가진 모든 오브젝트의 개수를 fruitCount에 저장
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected) // 플레이어가 과일에 닿았고, 과일을 먹었는지 확인 됐다면,
        {
            isCollected = true;                 // 과일이 먹혔음을 표시
            Destroy(gameObject);                // 닿은 과일 오브젝트 삭제
            fruitCount--;                       // 플레이어가 과일에 닿으면 과일 개수 감소

            Debug.Log($"남은 과일 개수: {fruitCount}, 시간: {Time.time:F2}초");
        }

    }
    public static void ResetFruitCount()        // 게임이 다시 시작될 때 fruitCount를 초기화
    {
        fruitCount = 0;                         // 과일 개수 초기화
    }
}