using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public enum PuddleTypes
    {
        BOOST,
        SLOW
    }
    private PuddleTypes currentType;
    public PuddleTypes CurrentType { get => currentType; set => currentType = value; }

    [SerializeField]
    private GameObject boostPuddle;
    public GameObject BoostPuddle { get => boostPuddle; }

    [SerializeField]
    private GameObject slowPuddle;
    public GameObject SlowPuddle { get => slowPuddle; }

    [SerializeField]
    private TextMeshProUGUI puddleText;
    public TextMeshProUGUI PuddleText { get => puddleText; }

    private float boostPower;
    public float BoostPower { set { boostPower = value; } }


    public void AffectBall(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        PlayerController pc = PlayerController.Instance;

        float value = pc.GetSpeedOfForwardMovement();
        value *= boostPower;
        pc.IncreaseSpeedOfForwardMovement(value);
    }
}
