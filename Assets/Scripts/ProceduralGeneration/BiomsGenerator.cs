using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiomsGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct SpecialBiomElement
    {
        public BiomData BiomData;
        [Tooltip("In every new biom appear")]
        public float SpawnChance;

        public BiomContainer GetBiom(BiomContainer previousBiom)
        {
            BiomContainer nextBiom = new BiomContainer(BiomData.Biom);

            var pillarsDifferenceHeight = nextBiom.PillarsBodyHeightRange.GetDifference();
            nextBiom.PillarsBodyHeightRange = previousBiom.PillarsBodyHeightRange;
            nextBiom.PillarsBodyHeightRange.Max += 0.2f;
            nextBiom.PillarsBodyHeightRange.Min = nextBiom.PillarsBodyHeightRange.Max - pillarsDifferenceHeight;

            var trampolineDifferenceHeight = nextBiom.TrampolineBodyHeightRange.GetDifference();
            nextBiom.TrampolineBodyHeightRange = previousBiom.TrampolineBodyHeightRange;
            nextBiom.TrampolineBodyHeightRange.Max += 0.2f;
            nextBiom.TrampolineBodyHeightRange.Min = nextBiom.TrampolineBodyHeightRange.Max - trampolineDifferenceHeight;

            return nextBiom;
        }
    }

    [System.Serializable]
    public class PillarColorElement
    {
        public Color FloorColor;
        public Color BodyColor;
    }

    private readonly string pathToScriptable = "Assets/Prefabs/Bioms";

    [Tooltip("After these bioms goes generation")]
    [SerializeField]
    private List<BiomData> predefinedBioms = new List<BiomData>();
    public List<BiomData> PredefinedBioms { get => predefinedBioms; }

    [SerializeField]
    private List<SpecialBiomElement> specialBioms = new List<SpecialBiomElement>();

    [SerializeField]
    private List<PillarColorElement> colorsForPillars = new List<PillarColorElement>();

    [Tooltip("Fixed value for biom generaion")]
    [SerializeField]
    private int maxBiomGeneration;

    [SerializeField]
    private bool infiniteGeneration;

    [SerializeField]
    private BiomsGeneratorSetupScriptable generatorSetupScriptable;
    private BiomsGeneratorSetup generatorSetup;

    [SerializeField]
    private DiamondsData diamondsData;
    public DiamondsData DiamondsData { get => diamondsData; }

    private BiomContainer currentBiom;
    public BiomContainer CurrentBiom
    {
        get
        {
            if (currentBiom == null)
            {
                currentBiom = predefinedBioms[0].Biom;
            }
            return currentBiom;
        }
        set { currentBiom = value; }
    }

    private bool isGenerating;

    private int generatedBiomNumber;
    private int biomsToPass;


    public void Initialize()
    {
        currentBiom = predefinedBioms[0].Biom;
        currentBiom.CurrentRing = 0;

        generatorSetup = new BiomsGeneratorSetup(generatorSetupScriptable.BiomsGeneratorSetup);
    }

    public void GeneratedRing()
    {
        currentBiom.CurrentRing += 1;
    }

    public bool IsNewBiom()
    {
        if (currentBiom.CurrentRing >= currentBiom.RingsToNext)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GoNextBiom()
    {
        if (isGenerating)
        {
            currentBiom = GenerateNextBiom(currentBiom);
        }
        else
        {
            var biomData = predefinedBioms.Where(x => x.Biom == currentBiom).FirstOrDefault();
            int index = predefinedBioms.IndexOf(biomData);
            if (index >= predefinedBioms.Count - 1)
            {
                isGenerating = true;
                GoNextBiom();
                return;
            }

            currentBiom = predefinedBioms[index + 1].Biom;
            currentBiom.CurrentRing = 0;
        }
    }

    public void TrySwitchBiom()
    {
        GeneratedRing();
        if (IsNewBiom())
        {
            GoNextBiom();
        }
    }

    public BiomContainer GenerateNextBiom(BiomContainer previousBiom, bool isCreateScriptable = false)
    {
        if (!infiniteGeneration && generatedBiomNumber > maxBiomGeneration)
        {
            return previousBiom;
        }

        BiomContainer nextBiom = new BiomContainer(previousBiom);

        // For special bioms
        foreach (var specialBiom in specialBioms)
        {
            if (UnityEngine.Random.value <= specialBiom.SpawnChance)
            {
                generatedBiomNumber += 1;
                return specialBiom.GetBiom(previousBiom);
            }
        }

        nextBiom.BiomName = previousBiom.BiomName + "_next_generated_(manually)";

        nextBiom.RingsToNext = generatorSetup.RingsToNext;

        var newRingStep = previousBiom.RingStep + generatorSetup.RingStepIncreaseRange.GetRandomFromRange();
        nextBiom.RingStep = Mathf.Min(newRingStep, 55f);

        var newFrequency = previousBiom.PillarsFrequency - generatorSetup.FrequencyDecreaseRange.GetRandomFromRange();
        nextBiom.PillarsFrequency = Mathf.Min(newFrequency, 60f);

        nextBiom.RingStepNoise = new Vector2(UnityEngine.Random.Range(generatorSetup.RingStepNoise.x, generatorSetup.RingStepNoise.y),
                                             UnityEngine.Random.Range(generatorSetup.RingStepNoise.x, generatorSetup.RingStepNoise.y));

        var newFloorSizeRange = previousBiom.PillarsFloorSizeRange - generatorSetup.FloorSizeDecreaseRange;
        nextBiom.PillarsFloorSizeRange = new RangeElement(Mathf.Max(newFloorSizeRange.Min, 0.2f),
                                                          Mathf.Max(newFloorSizeRange.Max, 0.2f));

        var newBodyHeightRange = previousBiom.PillarsBodyHeightRange + generatorSetup.BodyHeightIncreaseRange;

        nextBiom.PillarsBodyHeightRange = new RangeElement(Mathf.Max(newBodyHeightRange.Min, 0.95f),
                                                           Mathf.Min(newBodyHeightRange.Max, 4f));

        if (generatedBiomNumber != 0 && generatedBiomNumber % colorsForPillars.Count == 0)
        {
            for (int i = 0; i < colorsForPillars.Count; i++)
            {
                colorsForPillars[i].FloorColor = new Color(
                    colorsForPillars[i].FloorColor.r - 10f, 
                    colorsForPillars[i].FloorColor.g - 10f, 
                    colorsForPillars[i].FloorColor.b - 10f);
            }
        }

        nextBiom.PillarsFloorColor = colorsForPillars[generatedBiomNumber % colorsForPillars.Count].FloorColor;
        nextBiom.PillarsBodyColor  = colorsForPillars[generatedBiomNumber % colorsForPillars.Count].BodyColor;

        var newPuddleSpawnChance = previousBiom.PuddleSpawnChance + generatorSetup.PuddleSpawnChanceIncreaseRange.GetRandomFromRange();
        nextBiom.PuddleSpawnChance = Mathf.Min(newPuddleSpawnChance, 0.75f);       
        
        var newPuddleBoostPower = previousBiom.PuddleBoostPower + generatorSetup.BoostPowerIncreaseRange.GetRandomFromRange();
        nextBiom.PuddleBoostPower = Mathf.Min(newPuddleBoostPower, 0.3f);

        var newPuddleSlowPower = previousBiom.PuddleSlowPower + generatorSetup.SlowPowerIncreaseRange.GetRandomFromRange();
        nextBiom.PuddleSlowPower = Mathf.Min(newPuddleSlowPower, 0.45f);


        var newTrampolineSpawnChance = generatorSetup.TrampolineSpawnChanceRange.GetRandomFromRange();
        nextBiom.TrampolineSpawnChance = newTrampolineSpawnChance;

        var newTrampolineFloorSizeRange = new RangeElement(Mathf.Max(nextBiom.PillarsFloorSizeRange.Min - 0.2f, 0.2f),
                                                           Mathf.Max(nextBiom.PillarsFloorSizeRange.Max - 0.2f, 0.2f));
        nextBiom.TrampolineFloorSizeRange = newTrampolineFloorSizeRange;

        var newTrampolineBodyHeightRange = new RangeElement(Mathf.Max(nextBiom.PillarsBodyHeightRange.Min - 0.25f, 0.8f),
                                                            Mathf.Min(nextBiom.PillarsBodyHeightRange.Max - 0.25f, 3.8f));

        nextBiom.TrampolineBodyHeightRange = newTrampolineBodyHeightRange;

        var newDiamondsSpawnChance = previousBiom.DiamondsSpawnChance + generatorSetup.DiamondsSpawnChanceIncreaseRange.GetRandomFromRange();

        nextBiom.DiamondsSpawnChance = Mathf.Min(newDiamondsSpawnChance, 0.35f);
        
        if (generatedBiomNumber % biomsToPass == 0)
        {
            var diamondTypes = Enum.GetValues(typeof(DiamondType)).Cast<DiamondType>().ToList();
            var previousUpperBoundIndex = diamondTypes.IndexOf(previousBiom.DiamondsUpperBound);
            if (previousUpperBoundIndex < diamondTypes.Count - 1)
            {
                nextBiom.DiamondsUpperBound = diamondTypes[previousUpperBoundIndex + 1];
                biomsToPass = (int)generatorSetup.BiomsToIncreaseUpperBoundRange.GetRandomFromRange();
            }
            else
            {
                // Maximum value
                nextBiom.DiamondsUpperBound = previousBiom.DiamondsUpperBound;
                biomsToPass = 999;
            }
        }
        generatedBiomNumber += 1;

        return nextBiom;
    }
}
