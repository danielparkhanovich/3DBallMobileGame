using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourSingleton<PlayerController>
{
    [Header("Components")]
    [SerializeField]
    private Thrust thrust;
    [SerializeField]
    private Rigidbody rb;

    [Header("UI components")]
    [SerializeField] 
    private TextMeshProUGUI textRings;
    [SerializeField] 
    private TextMeshProUGUI textDiamonds;

    [Header("Player settings")]
    [SerializeField] 
    private float speedOfForwardMovement;
    [SerializeField] 
    private float speedOfRotate;
    [SerializeField] 
    private Vector2 maxSpeed;
    [SerializeField] 
    private Vector2 minSpeed;

    private int collectedDiamonds;
    public int CollectedDiamonds { get => collectedDiamonds; }

    private int overlappedRings;
    public int OverlappedRings { get => overlappedRings; }

    private float currentRotateDirection;

    private bool isThrusting;
    public bool IsThrusting { set => isThrusting = value; }


    private void Start()
    {
        ProceduralGeneration.Instance.NewRingEvent.AddListener(IncreaseNumberOfRings);
        Diamond.DiamondGathered += IncreaseNumberOfDiamonds;
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, currentRotateDirection * (180 / Mathf.PI), 0);

        // Update movement 
        rb.velocity = new Vector3(
            transform.forward.x * speedOfForwardMovement,
            rb.velocity.y,
            transform.forward.z * speedOfForwardMovement);
    }

    private void FixedUpdate()
    {
        if (isThrusting)
        {
            float force = thrust.ToThrust();
            transform.Translate(Vector3.forward * force);
            isThrusting = false;
        }

        if (transform.position.y <= 0f)
        {
            GameController.Instance.GameOver();
        }
    }

    public void TurnTickLeft(float singleAngleRotate)
    {
        currentRotateDirection -= speedOfRotate * Time.deltaTime * singleAngleRotate;
        if (currentRotateDirection < 0f)
        {
            currentRotateDirection += 2f * Mathf.PI;
        }
    }

    public void TurnTickRight(float singleAngleRotate)
    {
        currentRotateDirection += speedOfRotate * Time.deltaTime * singleAngleRotate;
        if (currentRotateDirection > 2f * Mathf.PI)
        {
            currentRotateDirection -= 2f * Mathf.PI;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pillar pillar = collision.gameObject.GetComponentInParent<Pillar>();

        if (pillar.Puddle.enabled)
        {
            pillar.Puddle.AffectBall(gameObject);
        }

        if (pillar.Trampoline.enabled)
        {
            pillar.Trampoline.BumpBall(gameObject);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            // Lose
            GameController.Instance.GameOver();
        }

        if (pillar)
        {
            if (GameController.Instance.CurrentSystemPlatform == Platform.Mobile)
            {
                Handheld.Vibrate();
            }
        }
    }

    public void IncreaseNumberOfDiamonds(int value)
    {
        collectedDiamonds += value;
        textDiamonds.text = "Diamonds: " + collectedDiamonds;
    }

    private void IncreaseNumberOfRings()
    {
        overlappedRings += 1;
        textRings.text = "Rings: " + overlappedRings;
    } 

    public float GetSpeedOfForwardMovement()
    {
        return speedOfForwardMovement;
    }

    public void IncreaseSpeedOfForwardMovement(float value)
    {
        if (speedOfForwardMovement + value < maxSpeed.x && 
            speedOfForwardMovement + value > minSpeed.x)
        {
            speedOfForwardMovement += value;
        }
        else if (speedOfForwardMovement + value > maxSpeed.x)
        {
            speedOfForwardMovement = maxSpeed.x;
        }
        else if (speedOfForwardMovement + value < minSpeed.x)
        {
            speedOfForwardMovement = minSpeed.x;
        }
    }

    public Vector3 GetBallVelocity()
    {
        return rb.velocity;
    }
}
