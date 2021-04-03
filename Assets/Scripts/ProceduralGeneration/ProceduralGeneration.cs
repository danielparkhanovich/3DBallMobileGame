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
    [SerializeField] private int renderRings;
    [SerializeField] private float renderFOV;
    [SerializeField] private bool cutFOV;

    [Header("Pillars")]
    [SerializeField] private bool biomsON;
    [SerializeField] private GameObject pillarObj;
    [SerializeField] private GameObject pillarTextObj;
    [SerializeField] private GameObject trampolineObj;

    private Bioms bioms;
    private float defaultRenderRadius;
    private int prerenderRings;
    private bool prerendering;
    private int ring = 1;
    private int ballRing = 1;
    private float fixedR = 0;
    private float ballDistance = 0;
    private float oldPillarsRingStep;

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

        // Prerender
        prerendering = true;
        ring = 1;
        StartCoroutine(DelayedPrerender());
    }
    private IEnumerator DelayedPrerender()
    {
        yield return null;

        float pillarsRingStep = Bioms.GetInstance().GetCurrentBiomData().RingStep;
        oldPillarsRingStep = pillarsRingStep;
        prerenderRings = Mathf.RoundToInt(renderRadius / pillarsRingStep);
        defaultRenderRadius = renderRadius;

        if (prerenderRings == 0)
        {
            prerenderRings = 1;
        }
        for (int i = 1; i <= prerenderRings; i++)
        {
            GenerateRing(ring);
            ring++;
        }
        prerendering = false;
    }

    public float GetRing()
    {
        return ring;
    }
    public float GetBallRing()
    {
        return ballRing;
    }

    private void GenerateRing(int ring)
    {
        // Bioms
        if (biomsON)
        {
            Bioms.instance.CheckNewBiom(ballRing);
        }

        BiomData generationData = Bioms.instance.GetCurrentBiomData();
        // pillars param init zone
        float pillarsRingStep = generationData.RingStep;
        float pillarsFrequency = generationData.PillarsFrequency;
        Vector2 pillarsBodyHeight = generationData.PillarsBodyHeightRange;
        Vector2 pillarsFloorSize = generationData.PillarsFloorSizeRange;
        Color pillarsFloorColor = generationData.PillarsFloorColor;
        Color pillarsBodyColor = generationData.PillarsBodyColor;
        // trampoline param init zone
        float trampolineSpawnChance = generationData.TrampolineSpawnChance;
        Vector2 trampolineBodyHeight = generationData.TrampolineBodyHeightRange;
        Vector2 trampolineFloorSize = generationData.TrampolineFloorSizeRange;

        float rPrev = fixedR + 10.0f;
        fixedR += pillarsRingStep;
        float r = fixedR;

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

        float ballMoveAngle = Mathf.Atan2(ballPos.y - centerPos.y, ballPos.x - centerPos.x) * (180.0f / Mathf.PI);
        if (ballMoveAngle < 0.0f)
        {
            ballMoveAngle += 360.0f;
        }

        // Cut FOV
        if (ring > 5 && cutFOV)
        {
            renderFOV /= (1.0f + 1.0f/(ring*pillarsRingStep*0.05f));
        }
        float thetaMin = ballMoveAngle - renderFOV/2;
        float thetaMax = thetaMin + renderFOV;

        // full circle thetaMin = 0; thetaMax = 360.0f
        for (float theta = thetaMin; theta < thetaMax; theta += angleStep)
        {

            float rDist = Random.Range(rPrev, r); //Mathf.Sqrt(Random.value)
            float x = center.position.x + rDist * Mathf.Cos(theta * (Mathf.PI / 180.0f));
            float z = center.position.z + rDist * Mathf.Sin(theta * (Mathf.PI / 180.0f));


            Vector2 bodyHeight;
            Vector2 floorSize;
            GameObject obj;

            // Default pillar
            if (Random.value > trampolineSpawnChance)
            {
                bodyHeight = pillarsBodyHeight;
                floorSize = pillarsFloorSize;
                obj = pillarObj;
            }
            // Trampoline pillar
            else
            {
                bodyHeight = trampolineBodyHeight;
                floorSize = trampolineFloorSize;
                obj = trampolineObj;
            }
            float h = Random.Range(bodyHeight.x, bodyHeight.y);
            float s = Random.Range(floorSize.x, floorSize.y);

            Color[] color = { pillarsFloorColor, pillarsBodyColor };
            CreatePillar(x, z, s, h, color, obj, !prerendering, ring);
        }
    }

    public bool IsRender()
    {
        return prerendering;
    }

    private GameObject CreatePillar(float x, float z, float s, float h, Color[] ringColor, GameObject obj, bool isAnimate, int ring)
    {
        Vector3 position = new Vector3(x, transform.position.y, z);
        Color floorColor = ringColor[0];
        Color bodyColor = ringColor[1];

        BiomData biomData = Bioms.instance.GetCurrentBiomData();

        GameObject pillar = Instantiate(obj, position, Quaternion.identity);
        pillar.GetComponent<Pillar>().SetRing(ring);

        // Animations
        if (isAnimate)
        { 
            pillar.GetComponent<Animator>().SetTrigger("Appear");
        }
        
        // Coloring
        Transform pillarModel = pillar.transform.GetChild(0);
        if (obj.tag == "Bounce")
        {
            pillarModel.GetChild(0).GetComponent<Renderer>().material.color = floorColor;
            pillarModel.GetChild(1).GetComponent<Renderer>().material.color = bodyColor;
        }
        pillarModel.localScale = new Vector3(s, h, s);

        // Puddle
        if (Random.value <= biomData.PuddleSpawnChance && obj.tag == "Bounce")
        {
            Puddle puddle = pillarModel.GetChild(0).gameObject.AddComponent(typeof(Puddle)) as Puddle;
            
            // Text
            Vector3 textSpawnPosition = pillarModel.GetChild(0).position;
            textSpawnPosition = new Vector3(textSpawnPosition.x, textSpawnPosition.y + 0.1f, textSpawnPosition.z);
            GameObject textPuddle = Instantiate(pillarTextObj, textSpawnPosition, Quaternion.identity);
            textPuddle.transform.parent = pillarModel.GetChild(0);

            if (Random.value <= biomData.PuddleBoostChance)
            {
                // Text
                textPuddle.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (int)(biomData.PuddleBoostPower*100) + "%";

                puddle.SetPuddleType(Puddle.PuddleTypes.BOOST);
            }
            else
            {
                // Text
                textPuddle.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (int)(biomData.PuddleSlowPower*100) + "%";

                puddle.SetPuddleType(Puddle.PuddleTypes.SLOW);
            }
        }

        // Diamonds
        if (Random.value <= biomData.DiamondsSpawnChance && obj.tag == "Bounce")
        {
            GameObject diamond = Diamond.SpawnDiamond(
                biomData.DiamondsVariety, 
                Bioms.instance.GetDiamondsPrefabs(), 
                Bioms.instance.GetDiamondsProbabilities(), 
                pillarModel.GetChild(0));
            diamond.transform.parent = pillarModel;
        }

        return pillar;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float pillarsRingStep = Bioms.GetInstance().GetCurrentBiomData().RingStep;
        for (int i = 1; i <= ring; i++)
        {
           UnityEditor.Handles.DrawWireDisc(center.position, center.up, pillarsRingStep * i);
        }
    }

    void Update()
    {
        Debug.Log($"Ballring: {ballRing}, current Ring: {ring}");
        BiomData biomdata = Bioms.instance.GetCurrentBiomData();

        if (!prerendering)
        {

            // new ring
            Vector2 ballPos = new Vector2(ballTransform.position.x, ballTransform.position.z);
            Vector2 centerPos = new Vector2(center.position.x, center.position.z);

            float pillarsRingStep = biomdata.RingStep;
            Vector2 pillarsFloorSize = biomdata.PillarsFloorSizeRange;

            renderRadius = renderRings * pillarsRingStep;

            if (Vector2.Distance(ballPos, centerPos) >= ballDistance + oldPillarsRingStep)
            {
                if (ballRing % Bioms.instance.GetRingsToNextBioms() == 0)
                {
                    oldPillarsRingStep = pillarsRingStep;
                }
                ballDistance += oldPillarsRingStep;
                ballRing += 1;
            }
            if (Vector2.Distance(ballPos, centerPos) + renderRadius >= pillarsRingStep + fixedR)
            {
                ring += 1;
                GenerateRing(ring);
            }
        }
    }
}
