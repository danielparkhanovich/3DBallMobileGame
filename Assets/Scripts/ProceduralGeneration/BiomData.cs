using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Procedural/Biom", fileName = "NewBiom")]
public class BiomData : ScriptableObject
{
    [SerializeField]
    private BiomContainer biom;
    public BiomContainer Biom { get => biom; }

    [SerializeField]
    private DiamondsData diamondsData;
    public DiamondsData DiamondsData { get => diamondsData; }
}
