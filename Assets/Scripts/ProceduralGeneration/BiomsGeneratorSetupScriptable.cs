using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomsGeneratorSetup
{
    public RangeElement RingStepIncreaseRange;
    public int RingsToNext;
    public RangeElement FrequencyDecreaseRange;
    public Vector2 RingStepNoise;

    [Header("Body")]
    public RangeElement FloorSizeDecreaseRange;
    public RangeElement BodyHeightIncreaseRange;

    [Header("Puddle")]
    public RangeElement PuddleSpawnChanceIncreaseRange;
    public RangeElement BoostChanceRange;
    public RangeElement BoostPowerIncreaseRange;
    public RangeElement SlowPowerIncreaseRange;

    [Header("Trampolines")]
    public RangeElement TrampolineSpawnChanceRange;

    [Header("Diamonds")]
    public RangeElement DiamondsSpawnChanceIncreaseRange;
    public RangeElement BiomsToIncreaseUpperBoundRange;


    // Copy constructor
    public BiomsGeneratorSetup(BiomsGeneratorSetup biomsGenerator)
    {
        this.RingStepIncreaseRange = biomsGenerator.RingStepIncreaseRange;
        this.RingsToNext = biomsGenerator.RingsToNext;
        this.FrequencyDecreaseRange = biomsGenerator.FrequencyDecreaseRange;
        this.RingStepNoise = biomsGenerator.RingStepNoise;
        this.FloorSizeDecreaseRange = biomsGenerator.FloorSizeDecreaseRange;
        this.BodyHeightIncreaseRange = biomsGenerator.BodyHeightIncreaseRange;
        this.PuddleSpawnChanceIncreaseRange = biomsGenerator.PuddleSpawnChanceIncreaseRange;
        this.BoostChanceRange = biomsGenerator.BoostChanceRange;
        this.BoostPowerIncreaseRange = biomsGenerator.BoostPowerIncreaseRange;
        this.SlowPowerIncreaseRange = biomsGenerator.SlowPowerIncreaseRange;
        this.TrampolineSpawnChanceRange = biomsGenerator.TrampolineSpawnChanceRange;
        this.DiamondsSpawnChanceIncreaseRange = biomsGenerator.DiamondsSpawnChanceIncreaseRange;
        this.BiomsToIncreaseUpperBoundRange = biomsGenerator.BiomsToIncreaseUpperBoundRange;
    }

}

[CreateAssetMenu(menuName = "Procedural/BiomsGeneratorSetup", fileName = "NewGeneratorSetup")]
public class BiomsGeneratorSetupScriptable : ScriptableObject
{
    public BiomsGeneratorSetup BiomsGeneratorSetup;
}
