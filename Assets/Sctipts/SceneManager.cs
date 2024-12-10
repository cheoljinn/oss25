using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void StartNewGame()
    {
        DataManager.instance.isNewGame = true;

        DataManager.instance.currentDay = 1;
        DataManager.instance.selectedEgg = 0;
        DataManager.instance.SaveData();

        UnityEngine.SceneManagement.SceneManager.LoadScene("EggSelect");
    }

    public void RestartGame()
    {

        DataManager.instance.ResetGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
