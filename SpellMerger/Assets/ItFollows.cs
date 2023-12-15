using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItFollows : MonoBehaviour
{
    private Transform target;
    private float decal = 2;
    private float newDecal = 0;
    public Vector3 gravityRef;

    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        gravityRef = Physics.gravity;
        target = FindObjectOfType<Controler>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        //else transform.position = new Vector3(target.position.x, target.position.y+2, -10);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y + decal, -10);
        FollowGravity();
    }

    void FollowGravity()
    {
        gravityRef = Physics.gravity;
        if (Physics.gravity.y> 0)
        {
            decal -= .1f;
            if (decal <= -2)
            {
                decal = -2;
            }
        }
        else if (Physics.gravity.y < 0)
        {
            decal += .1f;
            if (decal >= 2)
            {
                decal = 2;
            }
        }
        gravityRef = Physics.gravity;
        
    }
}
