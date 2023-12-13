using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SulfurSpell : MonoBehaviour
{
    public GameObject sulfurSouffleSpell;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            
        }
        else if (other.gameObject.CompareTag("Souffle"))
        {
            Instantiate(sulfurSouffleSpell);
        }
    }
}
