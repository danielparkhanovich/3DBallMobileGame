using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public enum GameStateType
{
    DEFAULT,
    MENU,
    PAUSE,
    PLAY
}

static class GameStateExtensions
{
    public static void OnStateEnter(this GameStateType state, UnityEvent ev = null)
    {
        // Call event
        ev.Invoke();
        switch (state)
        {
            case GameStateType.DEFAULT:
                break;
            case GameStateType.MENU: 
                break;
            case GameStateType.PAUSE:
                break;
            case GameStateType.PLAY:
                break;
            default: 
                break;
        }
    }
    public static void OnStateExit(this GameStateType state, UnityEvent ev = null)
    {
        ev.Invoke();
        switch (state)
        {
            case GameStateType.DEFAULT:
                break;
            case GameStateType.MENU:
                break;
            case GameStateType.PAUSE:
                break;
            case GameStateType.PLAY:
                break;
            default:
                break;
        }
    }
}
