using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CombinedSpell : MonoBehaviour
{
    [SerializeField] private float windForce;
    private Vector3 direction;
    public bool isEnviro;
    public Transform toSpawn;
    public float lifeTime = 5f;
    public Vector3 position;

    private void Start()
    {
        direction = new Vector3(0, windForce * -Physics.gravity.y, 0);
        StartCoroutine(LifeTime(isEnviro));

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("MovableItem"))
        {
            other.GetComponent<Rigidbody>().AddForce(direction * other.GetComponent<Rigidbody>().mass);
        }
    }

    IEnumerator LifeTime(bool spawn)
    {
        yield return new WaitForSeconds(lifeTime);
        if (spawn)
        {
            Transform souffle = Instantiate(toSpawn, position, quaternion.identity);
            souffle.GetComponent<SouffleSpell>().isEnviro = true;
        }
        Destroy(gameObject);
    }
    
}
