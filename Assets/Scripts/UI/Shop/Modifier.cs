using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    STARTSPEED,
    MOREDIAMONDS,
    TRAMPOLINEBOOST,
    THRUSTBOOST
}

public class Modifier : ShopElement
{
    [SerializeField]
    private ModifierType modifierType;

    [SerializeField]
    private int currentProgressValue;

    [SerializeField]
    private int cost;

    [SerializeField]
    private int maxProgressValue;


    [ContextMenu("TryBuy")]
    public override void TryBuy()
    {
        if (status != ShopElementStatus.UNLOCKED)
        {
            return;
        }

        if (DataManager.Instance.PlayerData.Diamonds >= cost)
        {
            Buy();
        }
    }

    [ContextMenu("Buy")]
    public override void Buy()
    {
        DataManager.Instance.PlayerData.SpendDiamonds(cost);
        currentProgressValue += 1;
        if (currentProgressValue >= maxProgressValue)
        {
            currentProgressValue = maxProgressValue;
            status = ShopElementStatus.COMPLETED;
        }
        Save();
    }

    [ContextMenu("Unlock")]
    public override void Unlock()
    {
        if (status != ShopElementStatus.LOCKED)
        {
            return;
        }

        status = ShopElementStatus.UNLOCKED;
        Save();
    }

    [ContextMenu("Save")]
    public override void Save()
    {
        DataCipher.SaveElement(nameof(status), status.ToString());
        DataCipher.SaveElement(nameof(currentProgressValue), currentProgressValue.ToString());
    }

    [ContextMenu("Load")]
    public override void Load()
    {
        status = (ShopElementStatus)Enum.Parse(typeof(ShopElementStatus), DataCipher.LoadElement(nameof(status)));
        currentProgressValue = int.Parse(DataCipher.LoadElement(nameof(currentProgressValue)));
    }
}
