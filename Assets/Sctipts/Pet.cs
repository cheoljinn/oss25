using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pet : MonoBehaviour
{

    public int maxDay = 15;

    public Sprite[] petSprites;
    public TextMeshProUGUI previous;
    public TextMeshProUGUI next;
    private bool isDayChanging = false;

    private SpriteRenderer sr;


   

    private void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        CheckLevel();
        StartCoroutine(UpdateSprite());
    }

    private void CheckLevel()
    {
        if (DataManager.instance.currentDay==1)
        {
            DataManager.instance.currentLevel = 0;
            StartCoroutine(UpdateSprite());
        }
        else if(DataManager.instance.currentDay < 7)
        {
            if (DataManager.instance.currentLevel == 0)
            {
                DataManager.instance.currentLevel = 1;
                StartCoroutine(UpdateSprite());
            }
        }
        else if(DataManager.instance.currentDay < 11)
        {
            if (DataManager.instance.currentLevel == 1)
            {
                DataManager.instance.currentLevel = 2;
                StartCoroutine(UpdateSprite());
            }
        }
        else if(DataManager.instance.currentDay < 16)
        {
            if (DataManager.instance.currentLevel == 2)
            {
                DataManager.instance.currentLevel = 3;
                StartCoroutine(UpdateSprite());
            }
        }
    }

    public IEnumerator UpdateSprite()
    {
        if (isDayChanging)
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
            int spriteIndex = (eggId - 1) * 4 + DataManager.instance.currentLevel;
            sr.sprite = petSprites[spriteIndex];
            Debug.Log($"현재 인덱스{spriteIndex}");
        }
    }

    public void AdvanceDay()
    {
        isDayChanging = true;
        DataManager.instance.currentDay++;
        DataManager.instance.money += 20;
        DataManager.instance.health -= 5;
        DataManager.instance.cleanliness -= 5;
        DataManager.instance.ResetButtonStates();

        Debug.Log($"현재 날짜: {DataManager.instance.currentDay}");
        CheckLevel();

        int day = DataManager.instance.currentDay;
        previous.text = (day - 1).ToString();
        next.text = day.ToString();
        DataManager.instance.SaveData();

        isDayChanging=false;

        if (DataManager.instance.currentDay > maxDay)
        {
            Debug.Log("엔딩");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
        }
    }
}
