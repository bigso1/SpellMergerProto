using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SouffleSpell : MonoBehaviour
{
    public bool isRight;
    private void OnTriggerStay(Collider other)
    {
        if (CompareTag("Player"))
        {
            if (isRight) other.GetComponent<Rigidbody>().AddForce(1, 0, 0);
            else other.GetComponent<Rigidbody>().AddForce(-1, 0, 0);
        }
    }
    
    
}
