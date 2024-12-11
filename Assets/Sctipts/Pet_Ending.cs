using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Ending : MonoBehaviour
{
    public Sprite[] petSprites;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    // Update is called once per frame
    void UpdateSprite()
    {
        int eggId = DataManager.instance.selectedEgg;
        if (eggId == 0)
        {
            Debug.Log("알이 선택되지 않음");
        }
        else
        {
            int spriteIndex = eggId - 1;
            sr.sprite = petSprites[spriteIndex];
            Debug.Log($"현재 인덱스{spriteIndex}");
        }
    }
}
