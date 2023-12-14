using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enmyFirePoint : MonoBehaviour
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
        transform.parent.LookAt(target, Vector3.forward);
    }
}
