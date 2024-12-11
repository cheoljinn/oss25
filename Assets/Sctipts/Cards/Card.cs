using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{

    [SerializeField]
    private Sprite animalSprite;

    [SerializeField]
    private SpriteRenderer cardRenderer;

    [SerializeField]
    private Sprite backSprite;

    private bool isFlipped = false;
    private bool isFlipping = false;
    private bool isMatched = false;

    public int cardID;

    public void SetCardID(int id)
    {
        cardID = id;
    }

    public void SetMatched()
    {
        isMatched = true;
    }
    public void SetAnimalSprite(Sprite sprite)
    {
        this.animalSprite = sprite;
    }
    
    public void FlipCard()
    {
        isFlipping = true;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z);
        //0.2초 동안 targetscale로 바뀜.Oncomplete: 그 작업이 끝나면 다음을 실행
        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            isFlipped = !isFlipped;
            if (isFlipped)
            {
                cardRenderer.sprite = animalSprite;
            }
            else
            {
                cardRenderer.sprite = backSprite;
            }

            transform.DOScale(originalScale, 0.2f).OnComplete(()=> { 
                isFlipping=false; //중복 뒤집기 방지
            });
        });
                   
        
    }

    void OnMouseDown()
    {
        if (!isFlipping&&!isMatched&&!isFlipped) //match되지 않앗고, 뒤집혀지거나 하나만 뒤집힌 상태가 아닐 때!
        {
            GameManager.instance.CardClicked(this);
        }
    }

}
