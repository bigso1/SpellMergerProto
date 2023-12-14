using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Controler : MonoBehaviour
{

    [Header("Movements")] 
    public bool gravityFlipped;
    [SerializeField] Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRange;
    public LayerMask jumpLayers;
    public LayerMask platformsMask;
    [SerializeField] private float jumpForce;
    private float fallMultiply = 2.5f;
    private float dirLook;
    private float x;
    private float z;
    private Vector3 move;
    public float moveSpeed;

    [Space] [Header("Spells")]
    public float launchForce = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody breathLaunchable;
    [SerializeField] private Rigidbody sulfurLaunchable;

    public bool breathUnlocked = false;

    public Vector3 cursorPos;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MovePackage();
        JumpPackage();
        if (Input.GetMouseButtonDown(0))
        {
            LaunchSpell(sulfurLaunchable);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(!breathUnlocked) return;
            LaunchSpell(breathLaunchable);
        }
    }

    public void GravityManager(bool gravityDir)
    {
        if (gravityDir)
        {
            gravityFlipped = true;
            transform.rotation = Quaternion.Euler(0,180,180);
        }
        else
        {
            gravityFlipped = false;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    
    void MovePackage()
    {
        //if(!onGround) speed=speed/2;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        move = transform.right * x;
        transform.position += move.normalized * Time.deltaTime * moveSpeed;
        if(x == 0 && z == 0) move = Vector3.zero;

        switch (gravityFlipped)
        {
            case true :
                if(rb.velocity.y > 0) rb.velocity -= Vector3.up*Physics.gravity.y*(fallMultiply-1)*Time.deltaTime;
                break;
            case false:
                if(rb.velocity.y < 0) rb.velocity += Vector3.up*Physics.gravity.y*(fallMultiply-1)*Time.deltaTime;
                break;
        }
    }
    
//|| !Input.GetKeyDown(KeyCode.Z)) return;
    void JumpPackage()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if(!GroundCheck()) return;
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);  
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(!GoDown()) return;
            StartCoroutine(GetDown());
        }
        
    }

    public bool GoDown()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, new Vector3(0,-.5f,0),out hit, groundCheckRange, platformsMask))
        {
            return true;
        }
        return false;
    }

    IEnumerator GetDown()
    {
        CapsuleCollider caps = GetComponent<CapsuleCollider>();
        caps.isTrigger = true;
        yield return new WaitForSeconds(.5f);
        caps.isTrigger = false;
    }
    public bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, new Vector3(0,-1,0),out hit, groundCheckRange, jumpLayers))
        {
            Debug.Log("jumping");
            return true;
        }
        Debug.Log("not jumping");
        return false;
    }

    public void LaunchSpell(Rigidbody spell)
    {
        var dir = firePoint.GetComponent<firePointScript>().GetDirection();
        Rigidbody spellInstance = Instantiate(spell, firePoint.position, quaternion.identity);
        if (!gravityFlipped)
        {
            spellInstance.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            spellInstance.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        
        spellInstance.AddForce(dir * launchForce, ForceMode.Impulse);
    }
}
