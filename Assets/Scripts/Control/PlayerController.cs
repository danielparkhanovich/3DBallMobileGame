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
    private GameObject textRings;
    [SerializeField] 
    private GameObject textDiamonds;

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
    private int overlappedRings;

    private float currentRotateDirection;

    private bool isThrusting;
    public bool IsThrusting { set => isThrusting = value; }


    private void Start()
    {
        ProceduralGeneration.Instance.NewRingEvent.AddListener(IncreaseNumberOfRings);
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
        if (collision.gameObject.tag == "Bounce")
        {
            Puddle puddle = collision.gameObject.GetComponent<Puddle>();
            if (puddle)
            {
                puddle.AffectBall(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Trampoline")
        {
            collision.gameObject.GetComponentInParent<Trampoline>().BumpBall(gameObject);

            if (textRings)
            {
                // Need observer instead of ProceduralGeneration.instance
                // textRings.GetComponent<TextMeshProUGUI>().text = "Rings: " + ProceduralGeneration.instance.GetBallRing(); textRings.GetComponent<TextMeshProUGUI>().text = "Rings: " + ProceduralGeneration.instance.GetBallRing();
            }
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            // Lose
            //Destroy(gameObject);
            SceneManager.Instance.StartGame();
        }
    }

    public int IncreaseNumberOfDiamonds(int value)
    {
        collectedDiamonds += value;
        return collectedDiamonds;
    }

    private void IncreaseNumberOfRings()
    {
        overlappedRings += 1;
        textRings.GetComponent<TextMeshProUGUI>().text = "Rings: " + overlappedRings;
    } 

    public GameObject GetTextDiamonds()
    {
        return textDiamonds;
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
