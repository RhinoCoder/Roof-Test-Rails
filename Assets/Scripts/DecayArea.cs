using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerMovement>().ScaleDecreaser();
            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerMovement>().ScaleDecreaser();
            
        }
    }
}
