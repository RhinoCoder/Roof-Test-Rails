using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barrel : MonoBehaviour
{

    [SerializeField] private GameObject[] barEdgePositions;
    private int randVal;
    
    
    private void Awake()
    {
        DOTween.Init();
        InitialPosDecider();
    }

    private void InitialPosDecider()
    {
        randVal = UnityEngine.Random.Range(0, 2);
    }

    public void CollectHandler()
    {
        gameObject.tag = "Untagged";
        var goToPos = barEdgePositions[randVal].transform.position;
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
        transform.DOMove(goToPos, 1f, false);
        Destroy(gameObject,0.1f);
        
    }
    

    
}
