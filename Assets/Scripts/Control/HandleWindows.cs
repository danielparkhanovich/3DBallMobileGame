using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWindows : MonoBehaviour, IHandleInput
{
    public bool isLeft()
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

    public bool isRight()
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

    public bool isDoubleJump()
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

    public float getSingleAngleRotate()
    {
        return 0.5f;
    }
}
