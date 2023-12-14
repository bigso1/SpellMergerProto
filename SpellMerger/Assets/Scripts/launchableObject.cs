using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class launchableObject : MonoBehaviour
{
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
        //SpawnSomeShit
        Debug.Log(other.name);
        Destroy(gameObject);
    }
}
