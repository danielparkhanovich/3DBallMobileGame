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

public class GameController : MonoBehaviourSingleton<GameController>
{
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
