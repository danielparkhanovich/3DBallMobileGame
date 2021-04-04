using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector2 cameraZoomForce;
    private Vector3 defaultPosition;

    private void Start()
    {
        defaultPosition = transform.localPosition;
    }

    private void Update()
    {
        float velocity = Vector3.Magnitude(target.GetComponent<PlayerController>().GetBallVelocity());
        transform.localPosition = new Vector3(defaultPosition.x, 
                                              defaultPosition.y + (velocity / 100) * cameraZoomForce.y, 
                                              defaultPosition.z - (velocity / 50) * cameraZoomForce.y);
    }
}
