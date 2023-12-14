using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltBloc : MonoBehaviour
{
    private bool willDetonate;
    private List<GameObject> EnemiesInRange = new List<GameObject>();
    private Vector3 startPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !EnemiesInRange.Contains(other.gameObject))
        {
            EnemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && EnemiesInRange.Contains(other.gameObject))
        {
            EnemiesInRange.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!willDetonate && other.gameObject.CompareTag("Sulfur"))
        {
            print("sulfurSalt");
            willDetonate = true;
            StartCoroutine(Detonation());
        }
        else if (!willDetonate && other.gameObject.CompareTag("Souffle"))
        {
            Destroy(other.gameObject);
            if (Physics.gravity.y == 9.81f)
            {
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, 9.81f, 0);
            }
        }
    }

    IEnumerator Detonation()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject gam in EnemiesInRange)
        {
            gam.GetComponent<EnemyBase>().TakeDamages(100);
        }

        willDetonate = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(3f);

        transform.position = startPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
