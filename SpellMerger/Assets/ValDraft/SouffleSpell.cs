using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SouffleSpell : MonoBehaviour
{
    public int windForce;
    public bool isRight;
    [SerializeField] private Transform groundCheck;
    public LayerMask grounds;
    [SerializeField] private Rigidbody rb;
    public float lifeTime = 5;
    public bool isEnviro;
    private Vector3 groundDir = new Vector3(0, -1, 0);
    


    private void Start()
    {
        if(!isEnviro) StartCoroutine(LifeTime());
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player")||other.CompareTag("Enemy")||other.CompareTag("MovableItem"))
        {
            if (isRight) other.GetComponent<Rigidbody>().AddForce(windForce*other.GetComponent<Rigidbody>().mass, 0, 0);
            else other.GetComponent<Rigidbody>().AddForce(-windForce*other.GetComponent<Rigidbody>().mass, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Sulfur")) return;
        Destroy(gameObject);
    }

    private void Update()
    {
        groundDir = Physics.gravity;
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, groundDir.normalized, out hit, .5f, grounds))
        {
            rb.useGravity = false;
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
