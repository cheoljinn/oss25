using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSelect : MonoBehaviour
{
    public void SelectEgg(int eggId)
    {
        DataManager.instance.selectedEgg = eggId;
        DataManager.instance.isNewGame = false;
        DataManager.instance.SaveData();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
    }
}
