using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SulfurSpell : MonoBehaviour
{
    public GameObject sulfurSouffleSpell;
    private bool hasCombined;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            
        }
        else if (other.gameObject.CompareTag("Souffle") && !hasCombined)
        {
            hasCombined = true;
            Instantiate(sulfurSouffleSpell, transform.position + (other.transform.position - transform.position) / 2, quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
