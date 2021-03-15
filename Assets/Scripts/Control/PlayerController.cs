using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Platform
    {
        Windows,
        Android,
        IOS
    }
    [SerializeField] private Platform curentPlatform;
    [SerializeField] private HandleWindows handleWindows;

    private IHandleInput handleInput;
    private Rigidbody rb;

    [SerializeField] private float speedOfRotate;
    [SerializeField] private float speedOfForwardMovement;

    private float currentRotateDirection = 0.0f;
    private bool isBounce = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bounce")
        {
            Debug.Log("Bounce !");
            isBounce = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        handleWindows = GetComponent<HandleWindows>();

        if (curentPlatform == Platform.Windows)
        {
            handleInput = handleWindows;
        }
    }

    void ManageInput()
    {
        if (handleInput.IsLeft())
        {
            currentRotateDirection -= speedOfRotate * Time.deltaTime * handleInput.GetSingleAngleRotate();
            if (currentRotateDirection < 0)
            {
                currentRotateDirection += 2 * Mathf.PI;
            }
        }
        else if (handleInput.IsRight())
        {
            currentRotateDirection += speedOfRotate * Time.deltaTime * handleInput.GetSingleAngleRotate();
            if (currentRotateDirection > 2 * Mathf.PI)
            {
                currentRotateDirection -= 2 * Mathf.PI;
            }
        }
    }

    void Update()
    {
        ManageInput();
        transform.eulerAngles = new Vector3(0, currentRotateDirection * (180 / Mathf.PI), 0);

        // Movement 
        rb.velocity = new Vector3(
            transform.forward.x * speedOfForwardMovement,
            rb.velocity.y,
            transform.forward.z * speedOfForwardMovement);

        isBounce = false;
    }
}
