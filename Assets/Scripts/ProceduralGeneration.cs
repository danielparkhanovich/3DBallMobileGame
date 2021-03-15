using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public static ProceduralGeneration instance = null;

    [Header("Track")]
    [SerializeField] private Transform center;
    [SerializeField] private Transform ballTransform;

    // Render parameters
    [Header("Renderer")]
    [SerializeField] private float renderRadius;
    [SerializeField] private float renderFOV;

    [Header("Pillars")]
    [SerializeField] private GameObject pillarObj;

    // Pillars generation parameters
    [SerializeField] private float pillarsRingStep;
    [SerializeField] private float pillarsСloseness;
    [SerializeField] private float pillarsFrequency;   // e.g. 5 -> 360/5 = 72, pillar on every 72 deg

    [SerializeField] private Vector2 pillarsFloorSize;
    [SerializeField] private Vector2 pillarsBodyHeight;

    private int prerenderRings;
    private bool isPrerendering;
    private int ring = 1;
    private int ballRing = 1;

    void Start()
    {
        // Singletone pattern
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }

        prerenderRings = Mathf.RoundToInt(renderRadius / pillarsRingStep);

        if (prerenderRings == 0)
        {
            prerenderRings = 1;
        }

        // Prerender
        isPrerendering = true;
        for (int i = 1; i <= prerenderRings; i++)
        {
            GenerateRing(i);
            ring = i;
        }
        isPrerendering = false;
    }

    public float GetRing()
    {
        return ring;
    }
    public float GetBallRing()
    {
        return ballRing;
    }

    void GenerateRing(int ring)
    {
        float rPrev = pillarsRingStep * (ring - 1) + 10.0f;
        float r = pillarsRingStep * ring;

        int numberOfPillars = 0;
        if (ring == 1)
        {
            numberOfPillars = Mathf.RoundToInt(pillarsFrequency);
        }
        else
        {
            numberOfPillars = Mathf.RoundToInt(pillarsFrequency * r/pillarsRingStep);
        }
        float angleStep = 360.0f / numberOfPillars;

        Vector2 ballPos = new Vector2(ballTransform.position.x, ballTransform.position.z);
        Vector2 centerPos = new Vector2(center.position.x, center.position.z);

        float thetaMin = -Mathf.Atan2(centerPos.y - ballPos.y, centerPos.x - ballPos.x)*(180.0f/Mathf.PI) - renderFOV/2;
        float thetaMax = thetaMin + renderFOV;

        Color ringColor = new Color(Random.value, Random.value, Random.value);

        // full circle thetaMin = 0; thetaMax = 360.0f
        for (float theta = thetaMin; theta < thetaMax; theta += angleStep)
        {

            Debug.Log("rPrev: " + rPrev);
            Debug.Log("r: " + r);
            Debug.Log("Theta: " + theta);

            float rDist = Random.Range(rPrev, r); //Mathf.Sqrt(Random.value)
            float x = center.position.x + rDist * Mathf.Cos(theta * (Mathf.PI / 180.0f));
            float z = center.position.z + rDist * Mathf.Sin(theta * (Mathf.PI / 180.0f));

            float h = Random.Range(pillarsBodyHeight.x, pillarsBodyHeight.y);
            float s = Random.Range(pillarsFloorSize.x, pillarsFloorSize.y);

            CreatePillar(x, z, s, h, ringColor, !isPrerendering, ring);
        }
    }

    public bool IsRender()
    {
        return isPrerendering;
    }

    private GameObject CreatePillar(float x, float z, float s, float h, Color ringColor, bool isAnimate, int ring)
    {
        Vector3 position = new Vector3(x, transform.position.y, z);
        GameObject pillar = Instantiate(pillarObj, position, Quaternion.identity);
        pillar.GetComponent<Pillar>().SetRing(ring);
        // Animation
        if (isAnimate)
        {
            Debug.Log("Animate !");
            pillar.GetComponent<Animator>().SetTrigger("Appear");
        }
        
        // Coloring
        Transform pillarModel = pillar.transform.GetChild(0);
        pillarModel.GetChild(0).GetComponent<Renderer>().material.color = ringColor;
        pillarModel.localScale = new Vector3(s, h, s);
        return pillar;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 1; i <= ring; i++)
        {
            UnityEditor.Handles.DrawWireDisc(center.position, center.up, pillarsRingStep * i);
        }
    }

    void Update()
    {
        // new ring
        Vector2 ballPos = new Vector2(ballTransform.position.x, ballTransform.position.z);
        Vector2 centerPos = new Vector2(center.position.x, center.position.z);

        Debug.Log("Ring: " + ring);

        if (Vector2.Distance(ballPos, centerPos) + renderRadius >= pillarsRingStep * ring + (pillarsFloorSize.y / 2))
        {
            ring += 1;
            ballRing += 1;
            GenerateRing(ring);
        }
    }
}
