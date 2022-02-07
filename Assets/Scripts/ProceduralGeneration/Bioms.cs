using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bioms : MonoBehaviour
{
    public static Bioms instance = null;

    [SerializeField] private List<BiomData> bioms;

    // Bioms
    [Header("Bioms")]
    [SerializeField] private int ringsToNext = 25; // default 25
    [SerializeField] private GameObject[] diamondsPrefabs;

    private int currentBiom = 0;
    private bool newBiom = false;

    private void Start()
    {
        // Singletone pattern
        if (instance == null)
        {
            instance = this;

            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.COMMON, 0.744);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.RARE, 0.2);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.MYTHICAL, 0.05);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.DRAGON, 0.005);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.ONYKS, 0.001);
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
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.COMMON, 0.744);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.RARE, 0.2);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.MYTHICAL, 0.05);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.DRAGON, 0.05);
            //instance.diamondsProbabilities.Add(Diamond.DiamondTypes.ONYKS, 0.001);

            return instance;
        }
        else
        {
            return instance;
        }
    }

    public void CheckNewBiom(int ballRing)
    {
        int numberOfBioms = bioms.Count;
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

}
