using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviourSingletonPersistent<DataManager>, ISave
{
    [SerializeField]
    private PlayerData playerData;
    public PlayerData PlayerData { get => playerData; }


    private void Start()
    {
        Load();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        playerData.Save();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        playerData.Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
