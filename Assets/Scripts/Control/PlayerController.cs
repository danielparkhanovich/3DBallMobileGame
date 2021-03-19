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
    [SerializeField] private Platform curentSystemPlatform;
    [SerializeField] private HandleWindows handleWindows;

    [SerializeField] private float speedOfRotate;
    [SerializeField] private float speedOfForwardMovement;

    private IHandleInput handleInput;
    private Thrust thrust;
    private Rigidbody rb;

    private float currentRotateDirection = 0.0f;
    private bool bounce = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bounce")
        {
            Debug.Log("Bounce !");
            bounce = true;
        }
        else if (collision.gameObject.tag == "Trampoline")
        {
            collision.gameObject.GetComponentInParent<Trampoline>().BumpBall(gameObject);
            Debug.Log("Trampoline !");
            bounce = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handleWindows = GetComponent<HandleWindows>();

        thrust = GetComponent<Thrust>();

        if (curentSystemPlatform == Platform.Windows)
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
        else if (handleInput.IsThrust())
        {
            float force = thrust.ToThrust();
            //rb.AddRelativeForce(force);
            transform.Translate(Vector3.forward * force);
        }
    }

    public void IncreaseSpeedOfForwardMovement(float value)
    {
        speedOfForwardMovement += value;
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

        bounce = false;
    }
}
