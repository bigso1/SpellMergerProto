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
    
    [SerializeField] private Transform groundCheck;
    public LayerMask grounds;
    [SerializeField] private Rigidbody rb;
    public float lifeTime = 5;


    
        
    private void Start()
    {
        StartCoroutine(ResetList());
        StartCoroutine(LifeTime());
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
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, .5f, grounds))
        {
            rb.useGravity = false;
        }
    }
    
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
    }
}
