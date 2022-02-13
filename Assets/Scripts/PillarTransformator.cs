using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PillarType
{
    PILLAR,
    TRAMPOLINE
}

public static class PillarTransformator
{
    public static void ReshapePillar(GameObject pillarObj, BiomContainer biomContainer, BiomsGenerator biomsGenerator)
    {
        float drawnTrampolineChance = Random.value;

        Pillar pillar = pillarObj.GetComponent<Pillar>();
        pillar.Trampoline.enabled = false;

        pillar.Puddle.enabled = false;
        pillar.Puddle.gameObject.SetActive(false);

        if (drawnTrampolineChance > biomContainer.TrampolineSpawnChance)
        {
            ReshapeToCommonPillar(pillar, biomContainer);

            float drawnPuddleChance = Random.value;
            if (drawnPuddleChance <= biomContainer.PuddleSpawnChance)
            {
                AddPuddle(pillar, biomContainer);
            }
        }
        else
        {
            ReshapeToTrampoline(pillar, biomContainer);
        }

        float drawnDiamondsChance = Random.value;
        if (drawnDiamondsChance <= biomContainer.DiamondsSpawnChance)
        {
            GameObject diamondObj = ObjectPooler.SharedInstance.GetReadyToUsePoolObject(1, pillar.DiamondSpawnTransform.position, Quaternion.identity);
            diamondObj.transform.parent = pillar.transform; 
            AddDiamond(diamondObj.GetComponent<Diamond>(), biomContainer.DiamondsUpperBound, biomsGenerator.DiamondsData);
            diamondObj.transform.localScale = new Vector3(1f, 1f, 1f);
            pillar.DiamondOnPillar = diamondObj;
        }
    }

    private static void ReshapeToCommonPillar(Pillar pillar, BiomContainer biomContainer)
    {
        ReshapeTo(pillar,
                  Random.Range(biomContainer.PillarsBodyHeightRange.Min, biomContainer.PillarsBodyHeightRange.Max),
                  Random.Range(biomContainer.PillarsFloorSizeRange.Min, biomContainer.PillarsFloorSizeRange.Max),
                  biomContainer.PillarsBodyColor,
                  biomContainer.PillarsFloorColor);
    }

    private static void ReshapeToTrampoline(Pillar pillar, BiomContainer biomContainer)
    {
        Trampoline trampoline = pillar.Trampoline;
        trampoline.enabled = true;

        ReshapeTo(pillar, 
                  Random.Range(biomContainer.TrampolineBodyHeightRange.Min, biomContainer.TrampolineBodyHeightRange.Max),
                  Random.Range(biomContainer.TrampolineFloorSizeRange.Min, biomContainer.TrampolineFloorSizeRange.Max),
                  trampoline.BodyColor, 
                  trampoline.FloorColor);

        pillar.HideAfterDelay(10f);
    }

    private static void ReshapeTo(Pillar pillar, float h, float s, Color body, Color floor)
    {
        // Set size
        pillar.Model.transform.localScale = new Vector3(s, h, s);

        // Set coloring
        pillar.BodyRenderer.material.color = body;
        pillar.FloorRenderer.material.color = floor;
    }


    private static void AddPuddle(Pillar pillar, BiomContainer biomContainer)
    {
        Puddle puddle = pillar.Puddle;
        puddle.enabled = true;
        puddle.gameObject.SetActive(true);

        if (Random.value <= biomContainer.PuddleBoostChance)
        {
            puddle.SlowPuddle.SetActive(false);
            puddle.BoostPuddle.SetActive(true);

            puddle.PuddleText.text = (int)(biomContainer.PuddleBoostPower * 100) + "%";
            puddle.BoostPower = biomContainer.PuddleBoostPower;
            puddle.CurrentType = Puddle.PuddleTypes.BOOST;
            return;
        }
        else
        {
            Debug.Log("SLOOW");
            puddle.SlowPuddle.SetActive(true);
            puddle.BoostPuddle.SetActive(false);

            puddle.PuddleText.text = (int)(biomContainer.PuddleSlowPower * 100) + "%";
            puddle.BoostPower = biomContainer.PuddleSlowPower * -1;
            puddle.CurrentType = Puddle.PuddleTypes.SLOW;
        }
    }

    private static void AddDiamond(Diamond diamond, DiamondType upperBound, DiamondsData diamondsData)
    {
        float randomValue = Random.value;
        DiamondsData.DiamondData selectedData = diamondsData.DataList[0];

        foreach (var data in diamondsData.DataList)
        {
            DiamondType type = data.Type;
            if (type != DiamondType.COMMON)
            {
                if (randomValue <= data.Probability)
                {
                    selectedData = data;
                }
            }
            if (type == upperBound)
            {
                break;
            };
        }

        diamond.InitValues(selectedData);
    }
}
