using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{
    // ��� ������ �� ������ ����(���� �ʱ�ȭ)
    public static int fruitCount = 0;

    private bool isCollected = false;       //������ �������� Ȯ���ϴ� ����(������ �Ծ��� �� ������ 2���� �����Ǵ� ���װ� �־ �߰�)

    // Start is called before the first frame update
    void Start()            
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && fruitCount == 0)       // ���� GameScene�̰�, fruitCount�� �ʱ�ȭ ������(������������ ���� �ٽ� �����Ҷ� ������ ������ �ʱ�ȭ �ϱ� ����)
        {
            fruitCount = GameObject.FindGameObjectsWithTag("Fruit").Length;             // Fruit �±׸� ���� ��� ������Ʈ�� ������ fruitCount�� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected) // �÷��̾ ���Ͽ� ��Ұ�, ������ �Ծ����� Ȯ�� �ƴٸ�,
        {
            isCollected = true;                 // ������ �������� ǥ��
            Destroy(gameObject);                // ���� ���� ������Ʈ ����
            fruitCount--;                       // �÷��̾ ���Ͽ� ������ ���� ���� ����

            Debug.Log($"���� ���� ����: {fruitCount}, �ð�: {Time.time:F2}��");
        }

    }
    public static void ResetFruitCount()        // ������ �ٽ� ���۵� �� fruitCount�� �ʱ�ȭ
    {
        fruitCount = 0;                         // ���� ���� �ʱ�ȭ
    }
}