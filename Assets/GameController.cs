using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            // Singletone pattern
            if (instance == null)
            {
                instance = new GameController();
            }

            return instance;
        }
    }
    private GameStateType state;
    [SerializeField] private UnityEvent StartGameEvent;
    [SerializeField] private UnityEvent PauseEvent;
    [SerializeField] private UnityEvent UnpauseEvent;
    [SerializeField] private UnityEvent EndGameEvent;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance)
        {
            Destroy(gameObject);
        }
        PauseGame();
    }
    private GameController()
    {
        state = GameStateType.DEFAULT;
    }

    public void PauseGame()
    {
        SetState(GameStateType.PAUSE, enterEvent: PauseEvent);
    }
    public void UnpauseGame()
    {
        SetState(GameStateType.PLAY, exitEvent: UnpauseEvent);
    }

    public void SetState(GameStateType newState, UnityEvent enterEvent = null, UnityEvent exitEvent = null)
    {
        state.OnStateExit(exitEvent);
        state = newState;
        state.OnStateEnter(enterEvent);
    }
    public GameStateType GetState()
    {
        return state;
    }

    private void Update()
    {
        switch (state)
        {
            case GameStateType.DEFAULT:
                SetState(GameStateType.MENU);
                break;
            case GameStateType.MENU:
                // Waiting until user click play
                break;
            case GameStateType.PLAY:
                break;
            case GameStateType.PAUSE:
                break;
        }
    }
}
