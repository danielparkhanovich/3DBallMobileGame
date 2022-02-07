using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Procedural/Biom", fileName = "NewBiom")]
public class BiomData : ScriptableObject
{
    [SerializeField] private float ringStep;
    public float RingStep
    {
        get
        {
            return ringStep;
        }
        protected set { }
    }

    [Header("Pillars")]
    [SerializeField] private float pillarsFrequency;
    public float PillarsFrequency
    {
        get
        {
            return pillarsFrequency;
        }
        protected set { }
    }

    [Tooltip("X - step in units on line from center <-X, X>, Y - step in angles on arc <-Y,Y>")]
    [SerializeField] private Vector2 ringStepNoise;
    public Vector2 RingStepNoise
    {
        get
        {
            return ringStepNoise;
        }
        protected set { }
    }


    [SerializeField] private Vector2 pillarsFloorSizeRange;
    public Vector2 PillarsFloorSizeRange
    {
        get
        {
            return pillarsFloorSizeRange;
        }
        protected set { }
    }

    [SerializeField] private Vector2 pillarsBodyHeightRange;
    public Vector2 PillarsBodyHeightRange
    {
        get
        {
            return pillarsBodyHeightRange;
        }
        protected set { }
    }

    [SerializeField] private Color pillarsFloorColor;
    public Color PillarsFloorColor
    {
        get
        {
            return pillarsFloorColor;
        }
        protected set { }
    }

    [SerializeField] private Color pillarsBodyColor;
    public Color PillarsBodyColor
    {
        get
        {
            return pillarsBodyColor;
        }
        protected set { }
    }

    [Header("Puddles")]
    [Range(0, 1)]
    [SerializeField] private float puddleSpawnChance;
    public float PuddleSpawnChance
    {
        get
        {
            return puddleSpawnChance;
        }
        protected set { }
    }

    [Range(0, 1)]
    [SerializeField] private float puddleBoostChance;
    public float PuddleBoostChance
    {
        get
        {
            return puddleBoostChance;
        }
        protected set { }
    }

    [Range(0, 1)]
    [SerializeField] private float puddleBoostPower;
    public float PuddleBoostPower
    {
        get
        {
            return puddleBoostPower;
        }
        protected set { }
    }

    [Range(0, 1)]
    [SerializeField] private float puddleSlowPower;
    public float PuddleSlowPower
    {
        get
        {
            return puddleSlowPower;
        }
        protected set { }
    }

    [Header("Trampolines")]
    [Range(0, 1)]
    [SerializeField] private float trampolineSpawnChance;
    public float TrampolineSpawnChance
    {
        get
        {
            return trampolineSpawnChance;
        }
        protected set { }
    }

    [SerializeField] private Vector2 trampolineFloorSizeRange;
    public Vector2 TrampolineFloorSizeRange
    {
        get
        {
            return trampolineFloorSizeRange;
        }
    }

    [SerializeField] private Vector2 trampolineBodyHeightRange;
    public Vector2 TrampolineBodyHeightRange
    {
        get
        {
            return trampolineBodyHeightRange;
        }
    }

    [Header("Diamonds")]
    [Range(0, 1)]
    [SerializeField] private float diamondsSpawnChance;
    public float DiamondsSpawnChance
    {
        get
        {
            return diamondsSpawnChance;
        }
        protected set { }
    }

    [SerializeField]
    private DiamondsData.DiamondType diamondsUpperBound;
    public DiamondsData.DiamondType DiamondsUpperBound { get => diamondsUpperBound; }

    [SerializeField]
    private DiamondsData diamondsData;
    public DiamondsData DiamondsData { get => diamondsData; }
}
