using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class GotchiData {

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int currentDay = 1;
    public int maxDays = 15;
    public int money = 0;

    public int selectedEgg = 0;
    public bool isNewGame = true;

    //Ω∫≈»
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
        Debug.Log($"Ω∫≈» {statName} ∫Ø∞Êµ : {value}");
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {

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
}
