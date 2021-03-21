using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public enum DiamondTypes
    {
        COMMON,   // White color  - 1
        RARE,     // Blue color   - 10
        MYTHICAL, // Purple color - 500
        DRAGON,   // Red color    - 5.000
        ONYKS     // Black color  - 50.000
    }

    public static GameObject SpawnDiamond(DiamondTypes currentUpperBound, GameObject[] models, Dictionary<DiamondTypes, double> diamondsProbabilities, Transform parent)
    {
        double randomValue = Random.value;
        DiamondTypes selectedType = DiamondTypes.COMMON;

        foreach (KeyValuePair<DiamondTypes, double> entry in diamondsProbabilities)
        {
            DiamondTypes type = entry.Key;
            if (entry.Key != DiamondTypes.COMMON)
            {
                if (randomValue <= entry.Value)
                {
                    selectedType = entry.Key;
                }
            }
            if (entry.Key == currentUpperBound)
            {
                break;
            };
        }
        Debug.Log(selectedType);

        int model = 0;
        switch (selectedType)
        {
            case DiamondTypes.RARE:
                model = 1;
                break;
            case DiamondTypes.MYTHICAL:
                model = 2;
                break;
            case DiamondTypes.DRAGON:
                model = 3;
                break;
            case DiamondTypes.ONYKS:
                model = 4;
                break;
        }
        var spawnPosition = new Vector3(parent.position.x, parent.position.y + 2, parent.position.z);
        return Instantiate(models[model], spawnPosition, Quaternion.identity);

    }

    private DiamondTypes currentType;

   

  
}
