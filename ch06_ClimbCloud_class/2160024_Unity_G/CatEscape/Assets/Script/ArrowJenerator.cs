using UnityEngine;

public class ArrowJenerator : MonoBehaviour
{

    public GameObject ArrowPrefab = null;
    //화살 프리팹 가져옴


    float fArrowCreateSpan = 0.7f;
    float fDeltaTime = 0.0f;
    float lastActivationTime = 0;
    bool _isStarted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ArrowPrefab == null)
        {
            Debug.LogError("ArrowPrefab이 할당되지 않았습니다.");
        }
    }//화살 프리팹이 없을때 오류 출력

    public void StartArrowJen()
    {
        _isStarted = true;
        fDeltaTime = 0.0f; 
        lastActivationTime = 0; 
        
    }
    //게임 시작시 호출됨 == 각각 게임 시작 변수, 화살 생성 쿨타임, 레벨 스케일링 인자값
    

    

    
    

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.Instance.gameTime - lastActivationTime >= 10)          //게임 시작 후 10초가 지나면 화살 생성 쿨타임을 줄임
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



