using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandleInput
{
    bool IsLeft();
    bool IsRight();
    bool IsThrust();
    float GetSingleAngleRotate();
    void ShowDebugInfo();
}
