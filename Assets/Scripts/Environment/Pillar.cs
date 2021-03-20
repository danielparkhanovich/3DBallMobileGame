using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] private float ringsToDestroy;

    private float fixedRing = 0;
    private float elapsedRings = 0;
    private float compareValue;


    public void SetRing(int ring)
    {
        fixedRing = ring;
        compareValue = ProceduralGeneration.instance.GetRing();
    }

    private void FixedUpdate()
    {
        if (!ProceduralGeneration.instance.IsRender())
        {
            if (ProceduralGeneration.instance.GetBallRing() != compareValue)
            {
                compareValue = ProceduralGeneration.instance.GetBallRing();
            }

            bool farToBall = compareValue - fixedRing > ringsToDestroy;

            if (farToBall)
            {
                Destroy(gameObject);
            }
        }
    }
}
