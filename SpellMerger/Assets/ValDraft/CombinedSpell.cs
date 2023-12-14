using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedSpell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("hit");
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            print("Player");
            other.GetComponent<Rigidbody>().AddForce(0,1,0);
        }
    }
}
