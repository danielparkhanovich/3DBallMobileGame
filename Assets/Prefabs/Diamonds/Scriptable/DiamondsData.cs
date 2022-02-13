
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Procedural/DiamondsData", fileName = "DiamondsData")]
public class DiamondsData : ScriptableObject
{
    [System.Serializable]
    public struct DiamondData
    {
        public DiamondType Type;
        public Material ModelMaterial;
        public Material ParticleMaterial;
        public int Points;
        public float Probability;
    }

    [SerializeField]
    private GameObject diamondPrefab;
    public GameObject DiamondPrefab { get => diamondPrefab; }

    [SerializeField]
    private List<DiamondData> dataList;
    public List<DiamondData> DataList { get => dataList; }

    public DiamondData GetDiamondData(DiamondType type)
    {
        return dataList.Where(diamondData => diamondData.Type == type).FirstOrDefault();
    }
}
