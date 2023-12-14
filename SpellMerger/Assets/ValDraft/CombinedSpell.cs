using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedSpell : MonoBehaviour
{
    [SerializeField] private float windForce;
    private Vector3 direction;

    private void Start()
    {
        direction = new Vector3(0, windForce * -Physics.gravity.y, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("MovableItem"))
        {
            other.GetComponent<Rigidbody>().AddForce(direction * other.GetComponent<Rigidbody>().mass);
        }
    }
}
