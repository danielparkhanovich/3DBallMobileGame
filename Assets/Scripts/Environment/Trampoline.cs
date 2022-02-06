using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private Color floorColor;
    public Color FloorColor { get => floorColor; }

    [SerializeField]
    private Color bodyColor;
    public Color BodyColor { get => bodyColor; }


    [SerializeField] 
    private Vector2 boost;
    public Vector2 Boost { set => boost = value; }

    private bool used = false;


    public void BumpBall(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        ball.GetComponent<PlayerController>().IncreaseSpeedOfForwardMovement(boost.x);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + boost.y, rb.velocity.z);
        used = true;

        Debug.Log("Bumb");
        GetComponent<Animator>().SetTrigger("Disappear");
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
