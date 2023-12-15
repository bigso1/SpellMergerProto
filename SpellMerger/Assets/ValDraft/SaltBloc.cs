using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltBloc : MonoBehaviour
{
    private bool willDetonate;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    [SerializeField] private SphereCollider myShockwave;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject.CompareTag("Enemy") && !EnemiesInRange.Contains(other.gameObject))
        {
            EnemiesInRange.Add(other.gameObject);
        }*/
        if (!willDetonate && other.CompareTag("Sulfur"))
        {
            print("sulfurSalt");
            willDetonate = true;
            StartCoroutine(Detonation());
        }
        else if (!willDetonate && other.CompareTag("Souffle"))
        {
            print("quoicoubeh");
            
            Destroy(other.gameObject);
            StartCoroutine(Consumed());
            if (Physics.gravity.y > 0)
            {
                print("Ruef");
                Physics.gravity = new Vector3(0, -9.81f, 0);
                FindObjectOfType<Controler>().GravityManager(false);
            }
            else
            {
                print("Feur");
                Physics.gravity = new Vector3(0, 9.81f, 0);
                FindObjectOfType<Controler>().GravityManager(true);
            }
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && EnemiesInRange.Contains(other.gameObject))
        {
            EnemiesInRange.Remove(other.gameObject);
        }
    }*/

    /*private void OnCollisionEnter(Collision other)
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
            if (Physics.gravity.y > 0)
            {
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, 9.81f, 0);
            }
        }
    }*/

    IEnumerator Detonation()
    {
        print("Kaboom");
        yield return new WaitForSeconds(1.5f);
        SphereCollider shock = Instantiate(myShockwave, transform.position, Quaternion.identity);

        willDetonate = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(5f);

        transform.position = startPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator Consumed()
    {
        print("resetblock");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(5f);

        transform.position = startPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
