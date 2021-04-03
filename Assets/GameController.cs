using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }
    private GameStateType state;
    [SerializeField] private UnityEvent StartGameEvent;
    [SerializeField] private UnityEvent EndGameEvent;

    private GameController()
    {
        state = GameStateType.DEFAULT;
    }

    public void SetState(GameStateType newState, UnityEvent ev = null)
    {
        state.OnStateExit(ev);
        state = newState;
        state.OnStateEnter(ev);
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
                SetState(GameStateType.PLAY, StartGameEvent);
                break;
            case GameStateType.MENU:
                break;
            case GameStateType.PAUSE:
                break;
            case GameStateType.PLAY:
                break;
        }
    }
}
