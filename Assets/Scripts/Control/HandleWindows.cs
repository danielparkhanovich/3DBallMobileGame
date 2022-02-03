using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls: 
   A - rotate to left 
   D - rotate to right
   W - thrust
 */

public class HandleWindows : IHandleInput
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

    public bool IsThrust()
    {
        if (Input.GetKey("w"))
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

    public void ShowDebugInfo()
    {
        
    }
}
