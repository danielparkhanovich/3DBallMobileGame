using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject, ISave
{
    [SerializeField]
    private int highscore;
    public int Highscore { get => highscore; }

    [SerializeField]
    private int diamonds;
    public int Diamonds { get => diamonds; }


    public void SpendDiamonds(int cost)
    {
        diamonds -= cost;
    }

    public void RefreshData(int diamondsToAdd, int newHighscore)
    {
        diamonds += diamondsToAdd;
        if (newHighscore > highscore)
        {
            highscore = newHighscore;
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        DataCipher.SaveElement(nameof(highscore), highscore.ToString());
        DataCipher.SaveElement(nameof(diamonds), diamonds.ToString());
    }

    [ContextMenu("Load")]
    public void Load()
    {
        highscore = int.Parse(DataCipher.LoadElement(nameof(highscore)));
        diamonds  = int.Parse(DataCipher.LoadElement(nameof(diamonds)));
    }
}
