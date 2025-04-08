using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum E_ITEM_TYPE_WITH_SCORE
    {
        Cherry = 10,
        WaterMellon = 20,
        Tomato = 30,
        Banana = 40,
        Grape = 50,
        Golden = 100
    }

    [SerializeField]
    E_ITEM_TYPE_WITH_SCORE eItemWithScore = E_ITEM_TYPE_WITH_SCORE.Cherry;

    // Start is called before the first frame update
    void Start()
    {
        if(eItemWithScore == E_ITEM_TYPE_WITH_SCORE.Cherry)
        {
            //이미지를 체리로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cherry");
        }
        else if(eItemWithScore == E_ITEM_TYPE_WITH_SCORE.WaterMellon)
        {
            //이미지를 수박으로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("watermelon");
        }
        else if (eItemWithScore == E_ITEM_TYPE_WITH_SCORE.Tomato)
        {
            //이미지를 토마토으로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("tomato");
        }
        else if (eItemWithScore == E_ITEM_TYPE_WITH_SCORE.Banana)
        {
            //이미지를 토마토으로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("banana");
        }
        else if (eItemWithScore == E_ITEM_TYPE_WITH_SCORE.Grape)
        {
            //이미지를 토마토으로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("grape");
        }
        else if (eItemWithScore == E_ITEM_TYPE_WITH_SCORE.Golden)
        {
            //이미지를 토마토으로 변경
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("golden");
        }
    }

    public int GetScore()
    {
        return (int)eItemWithScore;
    }
}
