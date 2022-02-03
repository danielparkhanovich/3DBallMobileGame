using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInput : MonoBehaviour
{
    private enum Platform
    {
        Windows,
        Mobile
    }

    [SerializeField]
    private Platform curentSystemPlatform;
    [SerializeField]
    private PlayerController playerController;

    private IHandleInput handleInput;


    private void Start()
    {
        if (curentSystemPlatform == Platform.Windows)
        {
            handleInput = new HandleWindows();
        }
        else
        {
            handleInput = new HandleMobile();
        }
    }

    private void Update()
    {
        ManageInput();
        handleInput.ShowDebugInfo();
    }

    private void ManageInput()
    {
        if (handleInput.IsLeft())
        {
            playerController.TurnTickLeft(handleInput.GetSingleAngleRotate());
        }
        else if (handleInput.IsRight())
        {
            playerController.TurnTickRight(handleInput.GetSingleAngleRotate());
        }
        if (handleInput.IsThrust())
        {
            playerController.IsThrusting = true;
        }
    }
}
