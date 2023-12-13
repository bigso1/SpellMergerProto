using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedSpell : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(0,1,0);
        }
    }
}
