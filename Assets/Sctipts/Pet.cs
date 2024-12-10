using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pet : MonoBehaviour
{

    public int maxDay = 15;

    public Animator animator;
    public Sprite[] petSprites;
    public TextMeshProUGUI previous;
    public TextMeshProUGUI next;

    private SpriteRenderer sr;
    private int currentLevel = 0;

    private void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        CheckLevel();
    }

    private void CheckLevel()
    {
        if (DataManager.instance.currentDay==1)
        {
            currentLevel = 0;
            StartCoroutine(UpdateSprite());
        }
        else if(DataManager.instance.currentDay == 2 && currentLevel == 0)
        {
            currentLevel = 1;
            StartCoroutine(UpdateSprite());
        }
        else if(DataManager.instance.currentDay == 7 && currentLevel == 1)
        {
            currentLevel = 2;
            StartCoroutine(UpdateSprite());
        }
        else if(DataManager.instance.currentDay == 11 &&currentLevel == 2)
        {
            currentLevel = 3;
            StartCoroutine(UpdateSprite());
        }
    }

    public IEnumerator UpdateSprite()
    {
        if (currentLevel != 0)
        {
            yield return new WaitForSeconds(2f);
        }

        int eggId = DataManager.instance.selectedEgg;
        if (eggId == 0)
        {
            Debug.Log("알이 선택되지 않음");
        }
        else
        {
            int spriteIndex = (eggId - 1) * 4 + currentLevel;
            sr.sprite = petSprites[spriteIndex];
        }
    }

    public void GoPlay()
    {
        animator.SetTrigger("goPlay");
    }
    public void Feed()
    {
        animator.SetTrigger("feed");
    }
    public void Read()
    {
        animator.SetTrigger("read");
    }
    public void Exercise()
    {
        animator.SetTrigger("exercise");
    }
    public void Shower()
    {
        animator.SetTrigger("shower");
    }
    public void Sleep()
    {
        animator.SetTrigger("sleep");
    }
    public void Handling()
    {
        animator.SetTrigger("handling");
    }

    public void IncreaseAffection(int v) => DataManager.instance.ModifyStat("affection", v);
    public void IncreaseHealth(int v) => DataManager.instance.ModifyStat("health", v);
    public void IncreaseIntelligence(int v) => DataManager.instance.ModifyStat("intelligence", v);
    public void IncreaseCleanliness(int v) => DataManager.instance.ModifyStat("cleanliness", v);
    public void IncreaseSociality(int v) => DataManager.instance.ModifyStat("sociality", v);
    public void IncreaseWillpower(int v) => DataManager.instance.ModifyStat("willpower", v);


    public void AdvanceDay()
    {
        DataManager.instance.currentDay++;
        DataManager.instance.money += 20;
        DataManager.instance.health -= 5;
        DataManager.instance.cleanliness -= 5;

        Debug.Log($"현재 날짜: {DataManager.instance.currentDay}");
        CheckLevel();

        int day = DataManager.instance.currentDay;
        previous.text = (day - 1).ToString();
        next.text = day.ToString();
        DataManager.instance.SaveData();

        if (DataManager.instance.currentDay > maxDay)
        {
            Debug.Log("엔딩");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
        }
    }
}
