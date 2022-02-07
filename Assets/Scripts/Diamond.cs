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
    private int points;

    private void Awake()
    {
        StartCoroutine(Disable(20f));
    }

    public void InitValues(DiamondsData.DiamondData data)
    {
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

            // Effect
            gatherParticleSystem.GetComponent<ParticleSystem>().Play();

            gameObject.SetActive(false);
        }
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private IEnumerator PlayParticle()
    {
        // .. un parent object play and parent
        yield return null;
    }
}
