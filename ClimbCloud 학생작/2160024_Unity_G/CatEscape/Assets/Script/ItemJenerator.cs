using UnityEngine;

public class ItemJenerator : MonoBehaviour
{

    public GameObject ItemPrefab = null;
    float fItemCreateSpan = 5.0f;
    float fDeltaTime = 0.0f;
    bool _isStarted = false;
    

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ItemPrefab == null)
        {
            Debug.LogError("ItemPrefab이 할당되지 않았습니다.");
        }
    }

    public void StartItemJen()
    {
        _isStarted = true;
        fDeltaTime = 0.0f;
        

    }



    // Update is called once per frame
    void Update()
    {
        if (_isStarted)
        {
            this.fDeltaTime += Time.deltaTime;
            if (this.fDeltaTime > this.fItemCreateSpan)
            {
                spawnItem();
            }
        }
    }



    private void spawnItem()
    {
        this.fDeltaTime = 0;
        GameObject spawnedItem = Instantiate(ItemPrefab);
        int randX = Random.Range(-6, 6);
        spawnedItem.transform.position = new Vector3(randX, 7, 0);
    }
}
