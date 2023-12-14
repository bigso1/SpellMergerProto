using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SulfurSpell : MonoBehaviour
{
    public GameObject sulfurSouffleSpell;
    private bool hasCombined;
    private List<GameObject> damagedEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ResetList());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && !damagedEnemies.Contains(other.gameObject))
        {
            damagedEnemies.Add(other.gameObject);
            other.GetComponent<EnemyBase>().TakeDamages(10);
        }
        else if (other.gameObject.CompareTag("Souffle") && !hasCombined)
        {
            hasCombined = true;
            Instantiate(sulfurSouffleSpell, transform.position + (other.transform.position - transform.position) / 2, quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator ResetList()
    {
        yield return new WaitForSeconds(0.5f);
        damagedEnemies.Clear();
        StartCoroutine(ResetList());
    }
}
