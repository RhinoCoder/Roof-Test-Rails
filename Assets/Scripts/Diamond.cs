using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectParticle;
    private void Awake()
    {
        DOTween.Init();
    }

    public void GemCollect()
    {
        collectParticle.Play();
        gameObject.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.1f);
        Destroy(gameObject,0.2f);
    }
}
