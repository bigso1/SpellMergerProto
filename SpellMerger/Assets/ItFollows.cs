using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItFollows : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Controler>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y+2, -10);
    }
}
