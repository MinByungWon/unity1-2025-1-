using UnityEngine;

public class ArrowJenerator : MonoBehaviour
{

    public GameObject ArrowPrefab = null;
    //ȭ�� ������ ������


    float fArrowCreateSpan = 0.7f;
    float fDeltaTime = 0.0f;
    float lastActivationTime = 0;
    bool _isStarted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ArrowPrefab == null)
        {
            Debug.LogError("ArrowPrefab�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }//ȭ�� �������� ������ ���� ���

    public void StartArrowJen()
    {
        _isStarted = true;
        fDeltaTime = 0.0f; 
        lastActivationTime = 0; 
        
    }
    //���� ���۽� ȣ��� == ���� ���� ���� ����, ȭ�� ���� ��Ÿ��, ���� �����ϸ� ���ڰ�
    

    

    
    

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.Instance.gameTime - lastActivationTime >= 10)          //���� ���� �� 10�ʰ� ������ ȭ�� ���� ��Ÿ���� ����
        {
            this.fArrowCreateSpan -= 0.2f;
            lastActivationTime = GameDirector.Instance.gameTime;
        }
        if (_isStarted)
        {
            this.fDeltaTime += Time.deltaTime;
            if (this.fDeltaTime > this.fArrowCreateSpan)
            {
                spawnArrow();
            }
        }
    }

    
    
    private void spawnArrow()
    {
        this.fDeltaTime = 0;
        GameObject spawnedArrow = Instantiate(ArrowPrefab);
        int randX = Random.Range(-6, 6);
        spawnedArrow.transform.position = new Vector3(randX, 7, 0);
    }


}



