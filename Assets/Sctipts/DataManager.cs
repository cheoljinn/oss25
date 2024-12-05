using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int currentDay = 1;
    public int maxDays = 15;

    //스탯
    public int affection;
    public int health;
    public int intelligence;
    public int cleanliness;
    public int sociality;
    public int willpower;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AdvanceDay()
    {
        currentDay++;
        Debug.Log($"현재 날짜: {currentDay}");

        if(currentDay > maxDays)
        {
            EndGame();
        }
    }

    public void ModifyStat(string statName, int value)
    {
        switch (statName.ToLower()) {
            case "affection":
                affection += value;
                break;
            case "health":
                health += value;
                break;
            case "intelligence":
                intelligence += value;
                break;
            case "cleanliness":
                cleanliness += value;
                break;
            case "sociality":
                sociality += value;
                break;
            case "willpower":
                willpower += value;
                break;
            default:
                Debug.Log($"Unknown stat: {statName}");
                break;
        }
        Debug.Log($"스탯 {statName} 변경됨: {value}");
    }

    public void IncreaseAffection(int v) => ModifyStat("affection", v);
    public void IncreaseHealth(int v) => ModifyStat("health", v);
    public void IncreaseIntelligence(int v) => ModifyStat("intelligence", v);
    public void IncreaseCleanliness(int v) => ModifyStat("cleanliness", v);
    public void IncreaseSociality(int v) => ModifyStat("sociality", v);
    public void IncreaseWillpower(int v) => ModifyStat("willpower", v);

    public void EndGame()
    {
        Debug.Log("엔딩을 맞이합니다");
    }
}
