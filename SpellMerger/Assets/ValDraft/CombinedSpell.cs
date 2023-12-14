using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedSpell : MonoBehaviour
{
    [SerializeField] private int windForce;
    public bool isUp;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if(isUp) other.GetComponent<Rigidbody>().AddForce(0,windForce,0);
            else other.GetComponent<Rigidbody>().AddForce(0,-windForce,0);
        }
    }
}
