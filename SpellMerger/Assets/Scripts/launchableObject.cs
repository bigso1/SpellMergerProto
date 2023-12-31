using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class launchableObject : MonoBehaviour
{
    [SerializeField] private GameObject souffleFx;
    public LayerMask walls;
    private float moduloPlace;
    public GameObject objectType;
    public float myLifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        souffleFx.SetActive(true);
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(myLifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector2 vel = GetComponent<Rigidbody>().velocity;
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Platform")) return;
        if(other.CompareTag("UI")) return;
        if (other.CompareTag(objectType.tag)) return;
        if(other.CompareTag("Patoune")) return;

        if (other.gameObject.layer == 10)
        {
            if (Physics.gravity.y > 0)
            {
                if(vel.y < 0) return;
            }
            else
            {
                if (vel.y > 0) return;
            }
        }

        RaycastHit hit;
        Vector3 grounDir = new Vector3(0,-1,0);
        if (Physics.gravity.y > 0) grounDir = new Vector3(0, 1, 0);
        if (Physics.Raycast(transform.position, grounDir, out hit,1, walls ))
        {
            moduloPlace = hit.transform.position.y - transform.position.y;
        }
        GameObject spell = Instantiate(objectType, new Vector3(transform.position.x, transform.position.y-moduloPlace, 2),
            quaternion.identity);
        if (vel.x > 0) spell.GetComponent<SouffleSpell>().isRight = true;
        else spell.GetComponent<SouffleSpell>().isRight = false;
        spell.transform.position =
            new Vector3(transform.position.x, transform.position.y + spell.transform.localScale.y / 2, 2);
        Debug.Log(other.name);
        Destroy(gameObject);
    }
}
