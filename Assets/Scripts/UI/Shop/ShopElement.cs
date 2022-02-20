using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopElementStatus 
{ 
    LOCKED,
    UNLOCKED,
    COMPLETED
}

public abstract class ShopElement : MonoBehaviour, ISave
{
    [SerializeField]
    protected ShopElementStatus status;
    public ShopElementStatus Status { get => status; }


    public abstract void TryBuy();
    public abstract void Buy();
    public abstract void Unlock();
    public abstract void Save();
    public abstract void Load();

    protected void OnEnable()
    {
        Load();
    }
}
