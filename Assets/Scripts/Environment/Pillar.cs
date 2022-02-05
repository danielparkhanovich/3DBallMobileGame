using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private int lifetime;

    public void InitValues(int lifetime)
    {
        this.lifetime = lifetime;
    }

    public void TryDisable()
    {
        Debug.Log("Disable : " + lifetime);
        lifetime -= 1;

        if (lifetime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
