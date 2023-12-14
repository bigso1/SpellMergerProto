using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class launchableSulfure : MonoBehaviour
{
    public GameObject objectType;
    public float myLifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(myLifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Platform")) return;
        if(other.CompareTag("UI")) return;
        if (other.CompareTag(objectType.tag)) return;
        Vector2 vel = GetComponent<Rigidbody>().velocity;
        GameObject spell = Instantiate(objectType, new Vector3(transform.position.x, transform.position.y, 2), quaternion.identity);
        spell.transform.position =
            new Vector3(transform.position.x, transform.position.y + spell.transform.localScale.y / 2, 2);
        Debug.Log(other.name);
        Destroy(gameObject);
    }
}
