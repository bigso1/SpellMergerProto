using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp : MonoBehaviour
{
    public LayerMask toIgnore;
    public CapsuleCollider parentCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Debug.Log("Ignore");
            parentCollider.isTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            parentCollider.isTrigger = false;
        }
    }
}
