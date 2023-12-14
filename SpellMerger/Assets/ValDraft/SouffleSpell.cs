using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SouffleSpell : MonoBehaviour
{
    public int windForce;
    public bool isRight;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (isRight) other.GetComponent<Rigidbody>().AddForce(windForce, 0, 0);
            else other.GetComponent<Rigidbody>().AddForce(-windForce, 0, 0);
        }
    }
    
    
}
