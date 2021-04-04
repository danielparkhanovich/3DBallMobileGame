using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
        // Try call event
        try
        {
            ev.Invoke();
        }
        catch (NullReferenceException e) { }

        switch (state)
        {
            case GameStateType.DEFAULT:
                break;
            case GameStateType.MENU: 
                break;
            case GameStateType.PAUSE:
                Time.timeScale = 0.0f;
                break;
            case GameStateType.PLAY:
                break;
            default: 
                break;
        }
    }
    public static void OnStateExit(this GameStateType state, UnityEvent ev = null)
    {
        try
        {
            ev.Invoke();
        }
        catch (NullReferenceException e) { }

        switch (state)
        {
            case GameStateType.DEFAULT:
                break;
            case GameStateType.MENU:
                break;
            case GameStateType.PAUSE:
                Time.timeScale = 1.0f;
                break;
            case GameStateType.PLAY:
                break;
            default:
                break;
        }
    }
}
