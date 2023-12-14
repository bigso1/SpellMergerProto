using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour
{
    // Start is called before the first frame update
    private bool flip;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            switch (flip)
            {
                
                case true:
                    Physics.gravity = new Vector3(0, 9.8f, 0);
                    
                    flip = false;
                    break;
                case false:
                    Physics.gravity = new Vector3(0, -9.8f, 0);
                    flip = true;
                    break;
            }
            
        }
    }
}
