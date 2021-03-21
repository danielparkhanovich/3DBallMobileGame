using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public enum PuddleTypes
    {
        BOOST,
        SLOW
    }
    private PuddleTypes currentType;

    public void SetPuddleType(PuddleTypes type)
    {
        currentType = type;
        switch (currentType)
        {
            case PuddleTypes.BOOST:
                // Green color
                GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                break;

            case PuddleTypes.SLOW:
                // Black color
                GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                break;
        }
    }

    public void AffectBall(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        PlayerController pc = ball.GetComponent<PlayerController>();

        float value = pc.GetSpeedOfForwardMovement();

        switch (currentType)
        {
            case PuddleTypes.BOOST:
                value *= Bioms.instance.GetPuddleBoostPower();
                pc.IncreaseSpeedOfForwardMovement(value);
                break;
            case PuddleTypes.SLOW:
                value *= Bioms.instance.GetPuddleSlowPower() * -1;
                pc.IncreaseSpeedOfForwardMovement(value);
                break;
        }
    }
}
