using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum GameStateType
{
    NONE,
    PAUSED,
    PLAY
}
public enum Platform
{
    Windows,
    Mobile
}

public class GameController : MonoBehaviourSingleton<GameController>
{
    [SerializeField]
    private Platform curentSystemPlatform;
    public Platform CurrentSystemPlatform { get => curentSystemPlatform; }

    [SerializeField]
    private PlayerController player;
    public PlayerController Player
    { 
        get 
        {
            if (player == null)
                player = PlayerController.Instance;

            return player; 
        } 
    }

    private DataManager dataManager;
    public DataManager DataManager
    {
        get
        {
            if (dataManager == null)
                dataManager = DataManager.Instance;

            return dataManager;
        }
    }

    [SerializeField]
    private ObjectPooler pooler;
    public ObjectPooler Pooler { get => pooler; }

    private GameStateType state;
    public GameStateType State { get => state; }


    [SerializeField] private UnityEvent StartGameEvent;
    [SerializeField] private UnityEvent PauseEvent;
    [SerializeField] private UnityEvent UnpauseEvent;
    [SerializeField] private UnityEvent EndGameEvent;


    private void Start()
    {
        PauseGame();
    }

    public void GameOver()
    {
        DataManager.PlayerData.RefreshData(player.CollectedDiamonds, player.OverlappedRings);
        SceneManager.Instance.StartGame();
    }

    public void BackToMenu()
    {
        SceneManager.Instance.BackToMenu();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        state = GameStateType.PAUSED;
        PauseEvent.Invoke();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        state = GameStateType.PLAY;
        UnpauseEvent.Invoke();
    }
}
