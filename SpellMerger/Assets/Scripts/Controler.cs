using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Controler : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRange;
    public LayerMask jumpLayers;
    [SerializeField] private float jumpForce;
    
    private float dirLook;
    private float x;
    private float z;
    private Vector3 move;
    public float moveSpeed;

    [Space] [Header("Spells")]
    public float launchForce = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject breathLaunchable;
    [SerializeField] private GameObject sulfurLaunchable;

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
    
    void MovePackage()
    {
        //if(!onGround) speed=speed/2;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        move = transform.right * x;
        transform.position += move.normalized * Time.deltaTime * moveSpeed;
        if(x == 0 && z == 0) move = Vector3.zero;

        if (x > 0) dirLook = 0.65f;
        if (x < 0) dirLook = -.65f;
        firePoint.transform.localPosition = new Vector3(dirLook,0 , 0);

    }
//|| !Input.GetKeyDown(KeyCode.Z)) return;
    void JumpPackage()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if(!GroundCheck()) return;
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);  
        }
    }
    
    public bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, new Vector3(0,-1,0),out hit, groundCheckRange, jumpLayers))
        {
            Debug.Log("jumping");
            return true;
        }
        else
        {
            Debug.Log("not jumping");
            return false;
        }
    }

    public void LaunchSpell(GameObject spell)
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 launchDir = cursorPos - transform.position;
        Debug.Log(cursorPos);
        Debug.Log("direction " + launchDir);
        GameObject spellInstance = Instantiate(spell, firePoint.position, quaternion.identity);

        spellInstance.GetComponent<Rigidbody>().AddForce(launchDir.normalized * launchForce, ForceMode.Impulse);
    }
}
