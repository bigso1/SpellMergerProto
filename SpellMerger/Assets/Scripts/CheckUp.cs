using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp : MonoBehaviour
{
    public CapsuleCollider parentCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            other.isTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            other.isTrigger = false;
        }
    }
}
