using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrust : MonoBehaviour
{
    [SerializeField] private GameObject sliderObj;
    [SerializeField] private float thrustReserve;
    [SerializeField] private float thrustReloadDelay;
    [SerializeField] private float thrustReloadSpeed;
    [SerializeField] private float thrustForce; // recommended ~0.1
    [SerializeField] private bool affectOnCameraZoom;

    private float currentThrustReserve;
    private float currentThrustDelay;
    private float elapsedDelay;
    private bool waiting = false;
    private bool thrusting = false;

    private void Start()
    {
        currentThrustReserve = thrustReserve;
        currentThrustDelay = thrustReloadDelay;
    }

    public float ToThrust()
    {
        if (currentThrustReserve > 0)
        {
            thrusting = true;

            float decreaseThrustReserve = 15 * Time.deltaTime;
            currentThrustReserve -= decreaseThrustReserve;

            StartCoroutine(WaitThrustDelay(thrustReloadDelay));

            return thrustForce;
        }
        else
        {
            return 0;
        }
    }

    public IEnumerator WaitThrustDelay(float delay)
    {
        // If coroutine is started - refresh elapsed time to 0
        if (waiting)
        {
            elapsedDelay = 0.0f;
        }
        else
        {
            waiting = true;
            elapsedDelay = 0.0f;
            while (elapsedDelay < delay)
            {
                elapsedDelay += Time.deltaTime;
                yield return null;
            }
            waiting = false;
        }
    }

    private void FixedUpdate()
    {
        // Update UI 
        if (currentThrustReserve <= thrustReserve && !waiting)
        {
            currentThrustReserve += thrustReloadSpeed * Time.deltaTime;
        }
        sliderObj.GetComponent<Slider>().value = currentThrustReserve/thrustReserve;
    }
}
