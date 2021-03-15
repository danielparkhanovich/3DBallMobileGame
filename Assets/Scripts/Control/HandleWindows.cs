using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWindows : MonoBehaviour, IHandleInput
{
    public bool IsLeft()
    {
        if (Input.GetKey("a"))
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
        if (Input.GetKey("d"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDoubleJump()
    {
        if (Input.GetKeyDown("space"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetSingleAngleRotate()
    {
        return 0.5f;
    }
}
