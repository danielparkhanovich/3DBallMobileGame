using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Trampoline trampoline;
    public Trampoline Trampoline { get => trampoline; }

    [SerializeField]
    private Puddle puddle;
    public Puddle Puddle { get => puddle; }

    [SerializeField]
    private GameObject model;
    public GameObject Model { get => model; }

    [Header("Construction")]
    [SerializeField]
    private GameObject body;
    public GameObject Body { get => body; }

    [SerializeField]
    private GameObject floor;
    public GameObject Floor { get => floor; }

    private int lifetime;


    public void InitValues(int lifetime)
    {
        this.lifetime = lifetime;
    }

    public void TryDisable()
    {
        lifetime -= 1;

        if (lifetime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
