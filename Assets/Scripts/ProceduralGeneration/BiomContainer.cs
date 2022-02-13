using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomContainer 
{
    public string BiomName;
    public int RingsToNext;
    public float RingStep;

    [Header("Pillars")]
    public float PillarsFrequency;

    [Tooltip("X - step in units on line from center <-X, X>, Y - step in angles on arc <-Y,Y>")]
    public Vector2 RingStepNoise;


    public RangeElement PillarsFloorSizeRange;
    public RangeElement PillarsBodyHeightRange;

    public Color PillarsFloorColor;
    public Color PillarsBodyColor;

    [Header("Puddles")]
    [Range(0, 1)]
    public float PuddleSpawnChance;

    [Range(0, 1)]
    public float PuddleBoostChance;

    [Range(0, 1)]
    public float PuddleBoostPower;

    [Range(0, 1)]
    public float PuddleSlowPower;

    [Header("Trampolines")]
    [Range(0, 1)]
    public float TrampolineSpawnChance;

    public RangeElement TrampolineFloorSizeRange;
    public RangeElement TrampolineBodyHeightRange;

    [Header("Diamonds")]
    [Range(0, 1)]
    public float DiamondsSpawnChance;

    public DiamondType DiamondsUpperBound;

    [HideInInspector]
    public int CurrentRing = 0;


    // Copy constructor
    public BiomContainer(BiomContainer biomContainer)
    {
        this.BiomName = biomContainer.BiomName;
        this.RingsToNext = biomContainer.RingsToNext;
        this.RingStep = biomContainer.RingStep;
        this.PillarsFrequency = biomContainer.PillarsFrequency;
        this.RingStepNoise = biomContainer.RingStepNoise;
        this.PillarsFloorSizeRange = biomContainer.PillarsFloorSizeRange;
        this.PillarsBodyHeightRange = biomContainer.PillarsBodyHeightRange;
        this.PillarsFloorColor = biomContainer.PillarsFloorColor;
        this.PillarsBodyColor = biomContainer.PillarsBodyColor;
        this.PuddleSpawnChance = biomContainer.PuddleSpawnChance;
        this.PuddleBoostChance = biomContainer.PuddleBoostChance;
        this.PuddleBoostPower = biomContainer.PuddleBoostPower;
        this.PuddleSlowPower = biomContainer.PuddleSlowPower;
        this.TrampolineSpawnChance = biomContainer.TrampolineSpawnChance;
        this.TrampolineFloorSizeRange = biomContainer.TrampolineFloorSizeRange;
        this.TrampolineBodyHeightRange = biomContainer.PillarsBodyHeightRange;
        this.DiamondsSpawnChance = biomContainer.DiamondsSpawnChance;
        this.DiamondsUpperBound = biomContainer.DiamondsUpperBound;
        this.CurrentRing = 0;
    }
}
