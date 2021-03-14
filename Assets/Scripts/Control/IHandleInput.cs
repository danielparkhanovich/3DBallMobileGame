using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandleInput
{
    bool isLeft();
    bool isRight();
    bool isDoubleJump();
    float getSingleAngleRotate();
}
