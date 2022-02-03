using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    MAINMENU,
    GAMEPLAY
}


public class SceneManager : MonoBehaviourSingletonPersistent<SceneManager>
{
    private SceneType currentScene = SceneType.MAINMENU;
    public SceneType CurrentScene { get => currentScene; }


    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene");
        currentScene = SceneType.GAMEPLAY;
    }

    public void ResetGame()
    {
        StartGame();
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        currentScene = SceneType.MAINMENU;
    }
}
