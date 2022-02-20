using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingElement : MonoBehaviour
{
    [SerializeField]
    private float pulseDuration = 1f;
    [SerializeField]
    private float pulseResize = 1f;

    private void Start()
    {
        transform.DOScale(pulseResize, pulseDuration).SetLoops(-1, LoopType.Yoyo);
    }
}
