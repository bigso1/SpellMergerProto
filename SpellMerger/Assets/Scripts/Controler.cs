using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private BoxCollider platformToChange;
    [Space] [Header("Spells")]
    public float launchForce = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody breathLaunchable;
    [SerializeField] private Rigidbody sulfurLaunchable;
    

    public bool breathUnlocked = false;
    private Vector3 groundDir = new Vector3(0,-1,0);

    private bool sulfurReady;
    public float sulfureCd = 1f;

    private float currentSulfurCd=1;

    private bool souffleReady;
    public float souffleCD = 1f;

    private float currentSouffleCD=1;

    private int score;

    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        currentSulfurCd = sulfureCd;
        currentSouffleCD = souffleCD;
    }

    // Update is called once per frame
    void Update()
    {
        SpellsCdManager();
        
        JumpPackage();
        if (Input.GetMouseButtonDown(0))
        {
            if(!sulfurReady) return;
            sulfurReady = false;
            LaunchSpell(sulfurLaunchable, true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(!breathUnlocked) return;
            if (!souffleReady) return;
            souffleReady = false;
            LaunchSpell(breathLaunchable,false);
        }
    }

    private void FixedUpdate()
    {
        MovePackage();
    }

    void SpellsCdManager()
    {
        if (!sulfurReady)
        {
            currentSulfurCd -= Time.deltaTime;
            if (currentSulfurCd <= 0)
            {
                sulfurReady = true;
                currentSulfurCd = sulfureCd;
            }
        }

        if (souffleReady) return;
        currentSouffleCD -= Time.deltaTime;
        if (currentSouffleCD <= 0)
        {
            souffleReady = true;
            currentSouffleCD = souffleCD;
        }
    }

    public void GravityManager(bool gravityDir)
    {
        if (gravityDir)
        {
            Debug.Log("i should flip");
            gravityFlipped = true;
            transform.rotation = Quaternion.Euler(180,0,0);
            groundDir = new Vector3(0, 1, 0);
            
        }
        else
        {
            gravityFlipped = false;
            transform.rotation = Quaternion.Euler(0,0,0);
            groundDir = new Vector3(0, -1, 0);
        }
    }
    
    void MovePackage()
    {
        //if(!onGround) speed=speed/2;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        move = transform.right * x;
        
        rb.MovePosition(transform.position + move.normalized * (Time.fixedDeltaTime * moveSpeed));
        
        if(x == 0 && z == 0) move = Vector3.zero;

        if (gravityFlipped)
        {
            if(rb.velocity.y > 0) rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiply-1) * Time.fixedDeltaTime);
        }
        else
        {
            if(rb.velocity.y < 0) rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiply-1) * Time.fixedDeltaTime);
        }
    }
    
//|| !Input.GetKeyDown(KeyCode.Z)) return;
    void JumpPackage()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if(!GroundCheck()) return;
            rb.AddForce(jumpForce * -(Physics.gravity).normalized, ForceMode.Impulse);  
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
        if (Physics.Raycast(groundCheck.position, groundDir,out hit, groundCheckRange, platformsMask))
        {
            platformToChange = hit.transform.GetComponent<BoxCollider>();
            return true;
        }
        return false;
    }

    IEnumerator GetDown()
    {
        platformToChange.isTrigger = true;
        yield return new WaitForSeconds(.5f);
        platformToChange.isTrigger = false;
    }
    public bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, groundDir,out hit, groundCheckRange, jumpLayers))
        {
            if(hit.transform.gameObject.layer != 9) return true;
            else if (hit.transform.CompareTag("Salt")) return true;
            else return false;
        }
        
        return false;
    }

    public void LaunchSpell(Rigidbody spell, bool sulfur)
    {
        var dir = firePoint.GetComponent<firePointScript>().GetDirection();
        Rigidbody spellInstance = Instantiate(spell, firePoint.position, quaternion.identity);
        if (!gravityFlipped)
        {
            spellInstance.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            spellInstance.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
        
        spellInstance.AddForce(dir * launchForce, ForceMode.Impulse);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patoune"))
        {
            print(other.gameObject);
            Destroy(other.gameObject);
            score++;
            scoreText.text = score.ToString();
        }
    }
}
