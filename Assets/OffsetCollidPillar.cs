using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetCollidPillar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pillar")
        {
            Debug.Log("CALL");
            Destroy(gameObject);
        }
    }
}
