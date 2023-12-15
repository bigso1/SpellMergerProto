using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveRoutine : MonoBehaviour
{
    public int dmg = 50;
    public List<Rigidbody> targets = new List<Rigidbody>();
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("MovableItem"))&& !targets.Contains(other.attachedRigidbody))
        {
            targets.Add(other.GetComponent<Rigidbody>());
        }
    }private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("MovableItem"))
        {
            targets.Remove(other.GetComponent<Rigidbody>());
        }
    }

    private void Start()
    {
        StartCoroutine(Wave());
        Destroy(gameObject);
    }

    IEnumerator Wave()
    {
        foreach (var target  in targets)
        {
            if(target.GetComponent<EnemyBase>()) target.GetComponent<EnemyBase>().TakeDamages(dmg);
            target.AddForce((target.transform.position-transform.position).normalized * target.mass * 5);
        }
        yield break;
    }
}
