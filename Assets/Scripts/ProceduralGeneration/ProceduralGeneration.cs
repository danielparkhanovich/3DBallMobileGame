using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.Events;

public class ProceduralGeneration : MonoBehaviourSingleton<ProceduralGeneration>
{
    [Header("Track")]
    [SerializeField] 
    private Transform originCenter;

    [Header("Renderer")]
    [SerializeField]
    private ObjectPooler objectPooler;
    public ObjectPooler ObjectPooler { get => objectPooler; }

    [SerializeField] 
    private int renderRings;
    public int RenderRings { get => renderRings; }

    [SerializeField]
    private int ringsToDestroyOld;
    public int RingsToDestroyOld { get => ringsToDestroyOld; }

    [SerializeField]
    private int renderRingNumberFactor;

    [SerializeField] 
    private float renderFOV;

    [Header("Pillars")]
    [SerializeField] 
    private GameObject pillarPrefab;

    [SerializeField] 
    private GameObject pillarTextObj;

    [SerializeField] 
    private GameObject trampolineObj;

    [Header("Bioms area")]
    [SerializeField] 
    private List<BiomData> bioms;
    public List<BiomData> Bioms { get => bioms; }
   
    private BiomData currentBiom;
    public BiomData CurrentBiom { get => currentBiom; set => currentBiom = value; }

    private int generatedRings;
    public int GeneratedRings { get => generatedRings; }

    private float generatedRadius;
    private float overlappedDistance;

    private int overlappedRings;

    private Transform ballTransform;
    public Transform BallTransform { get => ballTransform; set => ballTransform = value; }

    public UnityEvent NewRingEvent = new UnityEvent();

    private bool isPooling;
    public bool IsPooling { get => isPooling; }

    private bool isEditorUsing;
    public bool IsEditorUsing { get => isEditorUsing;  set => isEditorUsing = value; }


    private void Start()
    {
        isEditorUsing = false;

        ballTransform = PlayerController.Instance.transform;
        SwitchBiom(0);

        ResetRings();
        PrerenderRings(true);
    }

    public void PrerenderRings(bool isActivePooling)
    {
        this.isPooling = isActivePooling;

        int totalPillars = renderRings * renderRingNumberFactor * Mathf.RoundToInt(renderFOV / currentBiom.PillarsFrequency);

        objectPooler.AddObject(pillarPrefab, totalPillars, true);
        objectPooler.AddObject(currentBiom.DiamondsData.DiamondPrefab, 1, true);

        for (int i = 1; i <= renderRings; i++)
        {
            GenerateRing(false);
        }
    }

    // Utility function for editor
    public void ResetRings()
    {
        if (generatedRings == 0)
            return;

        overlappedRings = 0;
        generatedRings = 0;
        generatedRadius = 0;

        // Clear pooled objects lists
        for (int i = objectPooler.pooledObjectsList[0].Count - 1; i >= 0; i--)
        {
            DestroyImmediate(objectPooler.pooledObjectsList[0][i]);
        }
        for (int i = objectPooler.pooledObjectsList[1].Count - 1; i >= 0; i--)
        {
            DestroyImmediate(objectPooler.pooledObjectsList[1][i]);
        }

        objectPooler.itemsToPool = new List<ObjectPoolItem>();
        objectPooler.pooledObjects = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        // New ring
        var resultVector = ballTransform.position - originCenter.transform.position;

        if (resultVector.magnitude >= overlappedDistance + currentBiom.RingStep)
        {
            OverlapRing(true);
            NewRingEvent.Invoke();

            overlappedDistance += currentBiom.RingStep;
        }
    }

    public void OverlapRing(bool isAnimate)
    {
        GenerateRing(isAnimate);
        overlappedRings += 1;
    }

    private void GenerateRing(bool isAnimate)
    {
        generatedRings += 1;

        int pillarsNumberFactor = renderRingNumberFactor;
        float fixedRenderFov = renderFOV;

        generatedRadius += currentBiom.RingStep;

        if (generatedRings < renderRingNumberFactor)
        {
            pillarsNumberFactor = generatedRings;
        }
        else if (generatedRings > renderRingNumberFactor)
        {
            fixedRenderFov *= 19f * renderRingNumberFactor / generatedRadius;
        }

        int numberOfPillars = pillarsNumberFactor * Mathf.RoundToInt(renderFOV / currentBiom.PillarsFrequency);

        Vector2 ballPos = new Vector2(ballTransform.position.x, ballTransform.position.z);
        float ballMoveAngle = Mathf.Atan2(ballPos.y - originCenter.transform.position.z, ballPos.x - originCenter.transform.position.x) * (180.0f / Mathf.PI);
        if (ballMoveAngle < 0.0f)
        {
            ballMoveAngle += 360.0f;
        };

        float thetaMin = ballMoveAngle - fixedRenderFov / 2f;
        float thetaMax = ballMoveAngle + fixedRenderFov / 2f;

        if (isEditorUsing || generatedRings <= renderRings + 1)
        {
            thetaMin = -ballTransform.rotation.eulerAngles.y - fixedRenderFov / 2f + 90f;
            thetaMax = -ballTransform.rotation.eulerAngles.y + fixedRenderFov / 2f + 90f;
        }

        float angleStep = fixedRenderFov / numberOfPillars;

        // full circle thetaMin = 0; thetaMax = 360.0f
        for (float theta = thetaMin; theta <= Mathf.Ceil(thetaMax); theta += angleStep)
        {
            float noiseX = Random.Range(-currentBiom.RingStepNoise.x, currentBiom.RingStepNoise.x);
            float noiseY = Random.Range(-currentBiom.RingStepNoise.y, currentBiom.RingStepNoise.y);

            float rDist = generatedRadius + noiseX;
            float x = originCenter.transform.position.x + rDist * Mathf.Cos(theta * (Mathf.PI / 180.0f)) + noiseY;
            float z = originCenter.transform.position.z + rDist * Mathf.Sin(theta * (Mathf.PI / 180.0f));

            Vector3 position = new Vector3(x, transform.position.y, z);
            GameObject pillarObj = objectPooler.GetReadyToUsePoolObject(0, position, Quaternion.identity);

            //Quaternion rotation = Quaternion.Euler(0f, Vector3.Angle(originCenter.transform.position, ballTransform.position), 0f);
            //GameObject pillarObj = objectPooler.GetReadyToUsePoolObject(0, position, rotation);

            PillarTransformator.ReshapePillar(pillarObj, currentBiom);

            // Set lifetime
            Pillar pillar = pillarObj.GetComponent<Pillar>();

            if (generatedRings < renderRings)
            {
                pillar.InitValues(ringsToDestroyOld + (generatedRings - 1));
            }
            else
            {
                pillar.InitValues(ringsToDestroyOld + renderRings);
            }

            if (isPooling)
            {
                NewRingEvent.RemoveListener(pillar.TryDisable);
                NewRingEvent.AddListener(pillar.TryDisable);
            }

            // Animations
            if (isAnimate)
            {
                pillar.Animator.SetTrigger("Appear");
            }
        }

        TrySwitchBiom();
    }

    private void TrySwitchBiom()
    {
        currentBiom.GeneratedRing();
        if (currentBiom.IsNewBiom())
        {
            SwitchBiom(bioms.IndexOf(currentBiom) + 1);
        }
    }

    private void SwitchBiom(int index)
    {
        if (index < bioms.Count - 1)
        {
            currentBiom = bioms[index];
            currentBiom.SetNewBiom();
        }
        else
        {
            return;
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (!currentBiom)
            SwitchBiom(0);
        if (!ballTransform)
            ballTransform = PlayerController.Instance.transform;

        Gizmos.color = Color.yellow;
        float pillarsRingStep = currentBiom.RingStep;
        for (int i = 1; i <= generatedRings; i++)
        {
            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.DrawWireDisc(originCenter.position, Vector3.up, pillarsRingStep * i);
            UnityEditor.Handles.color = Color.cyan;
            UnityEditor.Handles.DrawWireDisc(originCenter.position, Vector3.up, (pillarsRingStep * i) + currentBiom.RingStepNoise.x);
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(originCenter.position, Vector3.up, (pillarsRingStep * i) + -currentBiom.RingStepNoise.x);
        }

        // Draw FOV
        float fov = renderFOV;

        Gizmos.color = Color.yellow;
        var left = Quaternion.AngleAxis(- fov / 2, Vector3.up) * ballTransform.forward;
        Gizmos.DrawRay(new Vector3(originCenter.transform.position.x, transform.position.y, originCenter.transform.position.y), left * 100f);

        Gizmos.color = Color.gray;
        Gizmos.DrawRay(new Vector3(originCenter.transform.position.x, transform.position.y, originCenter.transform.position.y), ballTransform.forward * 100f);

        Gizmos.color = Color.yellow;
        var right = Quaternion.AngleAxis(fov / 2, Vector3.up) * ballTransform.forward;
        Gizmos.DrawRay(new Vector3(originCenter.transform.position.x, transform.position.y, originCenter.transform.position.y), right * 100f);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(originCenter.transform.position, ballTransform.position);
    }
    #endif

    
}
