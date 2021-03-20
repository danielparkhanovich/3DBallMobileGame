using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private Vector2 boost;
    private bool used = false;

    public void BumpBall(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        ball.GetComponent<PlayerController>().IncreaseSpeedOfForwardMovement(boost.x);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + boost.y, rb.velocity.z);
        used = true;
    }
}
