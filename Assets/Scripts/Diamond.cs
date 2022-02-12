using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diamond : MonoBehaviour
{
    public static UnityAction<int> DiamondGathered;

    [SerializeField] private DiamondsData.DiamondType currentType;
    [SerializeField] private GameObject gatherParticleSystem;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float diamondLifetime;
    private int points;


    public void InitValues(DiamondsData.DiamondData data)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        ResetGatherEffect();

        StartCoroutine(Disable(diamondLifetime));

        if (this.currentType == data.Type)
        {
            this.points = data.Points;
            return;
        }

        this.currentType = data.Type;
        this.points = data.Points;
        this._renderer.material = data.ModelMaterial;

        // Switch particle system
        this.gatherParticleSystem.GetComponent<ParticleSystemRenderer>().material = data.ParticleMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DiamondGathered.Invoke(points);
            StopAllCoroutines();

            // Effect
            gatherParticleSystem.transform.parent = null;
            gatherParticleSystem.GetComponent<ParticleSystem>().Play();

            gameObject.SetActive(false);
        }
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private void ResetGatherEffect()
    {
        gatherParticleSystem.SetActive(true);
        gatherParticleSystem.transform.parent = transform;
        gatherParticleSystem.transform.localPosition = Vector3.zero;
        gatherParticleSystem.transform.localScale = Vector3.one;
    }
}
