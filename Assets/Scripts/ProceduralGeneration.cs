using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private Transform center;

    // Render parameters
    [SerializeField] private float renderRadius;


    [SerializeField] private GameObject pillarObj;

    // Pillars generation parameters
    [SerializeField] private float pillarsRingStep;
    [SerializeField] private float pillarsСloseness;
    [SerializeField] private float pillarsFrequency;   // e.g. 5 -> 360/5 = 72, pillar on every 72 deg

    [SerializeField] private Vector2 pillarsFloorSize;
    [SerializeField] private Vector2 pillarsBodyHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
