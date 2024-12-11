using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;


public class GotchiData {
    public int currentDay;
    public int money;
    public int selectedEgg;
    public bool isNewGame;

    public int affection;
    public int health;
    public int intelligence;
    public int cleanliness;
    public int sociality;
    public int willpower;

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int currentDay = 1;
    public int maxDays = 15;
    public int money = 0;

    public int selectedEgg = 0;
    public bool isNewGame = true;
    private string path;
    private Dictionary<string, bool> buttonStates;

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

        path = Path.Combine(Application.persistentDataPath, "gotchi_save.json");

        InitializeButtonStates();
    }

    private void InitializeButtonStates()
    {
        buttonStates = new Dictionary<string, bool>
        {
            { "exercise", false },
            { "feed", false },
            { "read", false },
            { "shower", false },
            { "handling", false },
            { "goPlay", false }
        };
    }
    public bool CanPerformAction(string actionName)
    {
        return buttonStates.ContainsKey(actionName) && !buttonStates[actionName];
    }

    public void SetActionPerformed(string actionName)
    {
        if (buttonStates.ContainsKey(actionName))
        {
            buttonStates[actionName] = true;
        }
    }

    public bool DeductMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        else
        {
            Debug.LogWarning("돈이 부족합니다!");
            return false;
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

    public void SaveData()
    {
        GotchiData data = new GotchiData
        {
            currentDay = currentDay,
            money = money,
            selectedEgg = selectedEgg,
            isNewGame = isNewGame,
            affection = affection,
            health = health,
            intelligence = intelligence,
            cleanliness = cleanliness,
            sociality = sociality,
            willpower = willpower
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("데이터 저장 완료");
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GotchiData data = JsonUtility.FromJson<GotchiData>(json);

            currentDay = data.currentDay;
            money = data.money;
            selectedEgg = data.selectedEgg;
            isNewGame = data.isNewGame;

            affection = data.affection;
            health = data.health;
            intelligence = data.intelligence;
            cleanliness = data.cleanliness;
            sociality = data.sociality;
            willpower = data.willpower;

            Debug.Log("데이터 불러오기 완료");
        }
        else
        {
            Debug.LogWarning("저장된 데이터 없음");
        }
    }

    public void ResetGame()
    {
        isNewGame = false;

        currentDay = 1;
        money = 0;
        affection = 0;
        health = 20;
        intelligence = 0;
        cleanliness = 20;
        sociality = 0;
        willpower = 0;

        selectedEgg = UnityEngine.Random.Range(1, 4);

        SaveData();

    }
    public void ResetButtonStates()
    {
        foreach (var key in new List<string>(buttonStates.Keys))
        {
            buttonStates[key] = false; // Dictionary 수정 허용
        }
    }
}
