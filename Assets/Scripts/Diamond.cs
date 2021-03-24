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
    [SerializeField] private DiamondTypes currentType;
    [SerializeField] private GameObject gatherParticleSystem;
    private int points;

    private void Awake()
    {
        Destroy(gameObject, 20f);
        switch (currentType)
        {
            case (DiamondTypes.COMMON):
                points = 1;
                break;
            case (DiamondTypes.RARE):
                points = 10;
                break;
            case (DiamondTypes.MYTHICAL):
                points = 500;
                break;
            case (DiamondTypes.DRAGON):
                points = 5000;
                break;
            case (DiamondTypes.ONYKS):
                points = 50000;
                break;
        }
    }

    public static GameObject SpawnDiamond(DiamondTypes currentUpperBound, GameObject[] models, Dictionary<DiamondTypes, double> diamondsProbabilities, Transform parent)
    {
        double randomValue = Random.value;
        DiamondTypes selectedType = DiamondTypes.COMMON;
        Debug.Log(randomValue);
        foreach (KeyValuePair<DiamondTypes, double> entry in diamondsProbabilities)
        {
            Debug.Log("test");
            Debug.Log(entry.Key);
        }

        foreach (KeyValuePair<DiamondTypes, double> entry in diamondsProbabilities)
        {
            DiamondTypes type = entry.Key;
            Debug.Log(type);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // UI
            PlayerController pc = other.GetComponent<PlayerController>();
            int newPoints = pc.IncreaseNumberOfDiamonds(points);
            pc.GetTextDiamonds().GetComponent<TMPro.TextMeshProUGUI>().text = "Diamonds: " + newPoints;

            // Effect
            Instantiate(gatherParticleSystem, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }


}
