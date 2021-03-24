using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAndroid : MonoBehaviour, IHandleInput
{
    public bool IsLeft()
    {
        if (Input.acceleration.x < 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRight()
    {
        if (Input.acceleration.x > 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsThrust()
    {
        Debug.Log("OK");
        if (Input.touchCount > 0)
        {
            Debug.Log("OK");
            return true;
        }
        else
        {
            return false;
        }
    }
   

    public float GetSingleAngleRotate()
    {
        return 0.75f * Mathf.Abs(Input.acceleration.x);
    }

    private void Update()
    {
        Debug.Log(Input.acceleration);
        Debug.Log(Input.touchCount);
    }
}
