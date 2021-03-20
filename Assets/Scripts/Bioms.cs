using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bioms : MonoBehaviour
{
    // Bioms
    [SerializeField] private int ringsToNext; // default 25

    // Pillars generation parameters
    [SerializeField] private float[] pillarsRingStep = new float[] { 25.0f };
    [SerializeField] private float[] pillarsСloseness = new float[] { 0.0f };
    [SerializeField] private float[] pillarsFrequency = new float[] { 12.0f }; // e.g. 5 -> 360/5 = 72, pillar on every 72 deg
    [SerializeField] private Vector2[] pillarsFloorSize = new Vector2[] { new Vector2(0.5f,1.0f) };
    [SerializeField] private Vector2[] pillarsBodyHeight = new Vector2[] {new Vector2(0.7f, 0.9f) };

    //Trampolins generation parameters
    [Range(0, 1)]
    [SerializeField] private float[] trampolineSpawnChance = new float[] { 0.1f };
    [SerializeField] private Vector2[] trampolineFloorSize = new Vector2[] { new Vector2(0.5f,1.0f) };
    [SerializeField] private Vector2[] trampolineBodyHeight = new Vector2[] { new Vector2(0.6f,0.7f) };


    private int currentBiom = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
