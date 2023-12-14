using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class EnmyShooter : EnemyBase
{

    [SerializeField] private Rigidbody proj;
    public List<Transform> checkPointList = new List<Transform>();
    private Vector3 targetPos;
    [SerializeField] private Transform firePoint;
    private float targetX;
    private bool aiming;
    private bool canShoot;
    public float lineOfSight;
    [SerializeField] private float shootDelay;
    private float currentShootDelay = 0;
    private Transform target;
    public float shootStrength = 20f;
    public float lerpDuration = 5; //delai à partir duquel le mob change de direction

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Controler>().transform;
        currentShootDelay = shootDelay;
        StartCoroutine(MoveLoop());
    }

    void ShootManager()
    {
        if (canShoot) return;
        currentShootDelay -= Time.deltaTime;
        if (currentShootDelay > 0) return;
        canShoot = true;
        currentShootDelay = shootDelay;
    }
    // Update is called once per frame
    void Update()
    {
        ShootManager();
        if (CheckPlayerInSight())
        {
            BagarBehaviour();
        }
        else
        {
            SentinelBehaviour();
        }
    }

    private bool CheckPlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) <= lineOfSight) return true;
        return false;
    }
    IEnumerator MoveLoop()
    {
        for (int i = 0; i < checkPointList.Count; i++)
        {
            {
                targetX = checkPointList[i].position.x;
                yield return new WaitForSeconds(lerpDuration);
                if (i >= checkPointList.Count-1) i = -1;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    void SentinelBehaviour()
    {
        transform.position += new Vector3((targetX - transform.position.x), 0, 0).normalized * moveSpeed * Time.deltaTime;
    }

    void BagarBehaviour()
    {
        if(!canShoot) return;
        canShoot = false;
        targetPos = target.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        Rigidbody projInstance = Instantiate(proj, firePoint.position, quaternion.identity);
        projInstance.AddForce(dir * shootStrength, ForceMode.Impulse);
    }
    
}
