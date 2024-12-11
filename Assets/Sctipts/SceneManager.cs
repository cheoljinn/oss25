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
        SoundManager.instance.Playsfx(SoundManager.Sfx.button);

        UnityEngine.SceneManagement.SceneManager.LoadScene("EggSelect");
    }
    
    public void LoadGame()
    {
        DataManager.instance.LoadData();
        SoundManager.instance.Playsfx(SoundManager.Sfx.button);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
    }

    public void RestartGame()
    {
        SoundManager.instance.Playsfx(SoundManager.Sfx.button);

        DataManager.instance.ResetGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
