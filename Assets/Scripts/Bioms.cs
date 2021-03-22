using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bioms : MonoBehaviour
{
    public static Bioms instance = null;

    // Bioms
    [Header("Bioms")]
    [SerializeField] private int numberOfBioms = 2;
    [SerializeField] private int ringsToNext = 25; // default 25

    // Pillars generation parameters
    [Header("Pillars")]
    [SerializeField] private float[] pillarsRingStep = new float[] { 25.0f, 40.0f };
    [SerializeField] private float[] pillarsСloseness = new float[] { 0.0f, 0.0f };
    [SerializeField] private float[] pillarsFrequency = new float[] { 12.0f, 12.0f }; // e.g. 5 -> 360/5 = 72, pillar on every 72 deg
    [SerializeField] private Vector2[] pillarsFloorSize = new Vector2[] { new Vector2(0.5f, 1.0f), new Vector2(0.4f, 0.8f) };
    [SerializeField] private Vector2[] pillarsBodyHeight = new Vector2[] { new Vector2(0.7f, 0.9f), new Vector2(0.9f, 1.2f) };

    // Trampolins generation parameters
    [Header("Trampolines")]
    [Range(0, 1)]
    [SerializeField] private float[] trampolineSpawnChance = new float[] { 0.1f, 0.08f };
    [SerializeField] private Vector2[] trampolineFloorSize = new Vector2[] { new Vector2(0.5f, 1.0f), new Vector2(0.4f, 0.8f) };
    [SerializeField] private Vector2[] trampolineBodyHeight = new Vector2[] { new Vector2(0.6f, 0.7f), new Vector2(0.7f, 0.8f) };

    // Puddles generation parameters
    [Header("Puddles")]
    [Range(0, 1)]
    [SerializeField] private float[] puddleSpawnChance = new float[] { 0.0f, 0.0f };
    [Range(0, 1)]
    [SerializeField] private float[] puddleBoostChance = new float[] { 0.0f, 0.0f };
    [Range(0, 1)]
    [SerializeField] private float[] puddleBoostPower = new float[] { 0.15f, 0.2f };
    [Range(0, 1)]
    [SerializeField] private float[] puddleSlowPower = new float[] { 0.15f, 0.2f };

    [Header("Diamonds")]
    [Range(0, 1)]
    [SerializeField] private float[] diamondsSpawnChance = new float[] { 0.0f, 0.0f };
    // From the selected diamond type and below
    [SerializeField] private Diamond.DiamondTypes[] diamondsVariety = new Diamond.DiamondTypes[] { };
    [SerializeField] private GameObject[] diamondsPrefabs;
    private Dictionary<Diamond.DiamondTypes, double> diamondsProbabilities = new Dictionary<Diamond.DiamondTypes, double>();

    private int currentBiom = 0;
    private bool newBiom = false;

    private void Start()
    {
        // Singletone pattern
        if (instance == null)
        {
            instance = this;

            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.COMMON, 0.744);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.RARE, 0.2);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.MYTHICAL, 0.05);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.DRAGON, 0.005);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.ONYKS, 0.001);
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    public static Bioms GetInstance()
    {
        if (instance == null)
        {
            instance = new Bioms();

            // Init probabilities dictionary
            Debug.Log("Added");
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.COMMON, 0.699);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.RARE, 0.2);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.MYTHICAL, 0.05);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.DRAGON, 0.05);
            instance.diamondsProbabilities.Add(Diamond.DiamondTypes.ONYKS, 0.001);

            return instance;
        }
        else
        {
            return instance;
        }
    }

    public void CheckNewBiom(int ballRing)
    {
        if (Mathf.Floor(ballRing/ringsToNext) > currentBiom && currentBiom < numberOfBioms-1)
        {
            Debug.Log(ballRing + " / " + ringsToNext);
            currentBiom += 1;
            Debug.Log(currentBiom);
            newBiom = true;
        }
        else
        {
            newBiom = false;
        }
    }

    public bool GetNewBiom()
    {
        return newBiom;
    }

    public int GetCurrentBiom()
    {
        return currentBiom;
    }

    // Get zone
    // Default pillars
    public float GetPillarsRingStep()
    {
        return pillarsRingStep[currentBiom];
    }
    public float GetPillarsСloseness()
    {
        return pillarsСloseness[0];
    }
    public float GetPillarsFrequency()
    {
        return pillarsFrequency[currentBiom];
    }
    public Vector2 GetPillarsFloorSize()
    {
        return pillarsFloorSize[currentBiom];
    }
    public Vector2 GetPillarsBodyHeight()
    {
        return pillarsBodyHeight[currentBiom];
    }
    // Trampolines
    public float GetTrampolineSpawnChance()
    {
        return trampolineSpawnChance[currentBiom];
    }
    public Vector2 GetTrampolineFloorSize()
    {
        return trampolineFloorSize[currentBiom];
    }
    public Vector2 GetTrampolineBodyHeight()
    {
        return trampolineBodyHeight[currentBiom];
    }
    // Puddles
    public float GetPuddleSpawnChance()
    {
        return puddleSpawnChance[currentBiom];
    }
    public float GetPuddleBoostChance()
    {
        return puddleBoostChance[currentBiom];
    }
    public float GetPuddleBoostPower()
    {
        return puddleBoostPower[currentBiom];
    }
    public float GetPuddleSlowPower()
    {
        return puddleSlowPower[currentBiom];
    }
    // Diamonds
    public float GetDiamondsSpawnChance()
    {
        return diamondsSpawnChance[currentBiom];
    }
    public Diamond.DiamondTypes GetDiamondsVariety()
    {
        return diamondsVariety[currentBiom];
    }
    public GameObject[] GetDiamondsPrefabs()
    {
        return diamondsPrefabs;
    }
    public Dictionary<Diamond.DiamondTypes, double> GetDiamondsProbabilities()
    {
        return diamondsProbabilities;
    }
}
