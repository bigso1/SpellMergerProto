using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Material originMat;
    [SerializeField] private Material feedbackMat;
    [SerializeField] private int maxHp = 100;
    public int hp;
    protected float moveSpeed = 5f; // Vitesse de déplacement du mob
    private bool canBeDmged = true;
    private Color _originColor;
   
    protected virtual void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void DebugDmg()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //GetComponent<Rigidbody>().AddForce(1000*Vector3.right, ForceMode.Impulse);
             StartCoroutine(TakeDamages(1));
        }
    }

    public void TakeDamages(int dmg)
    {
        if (!canBeDmged) return;
        StartCoroutine(Damager(dmg));
    }
    public IEnumerator Damager(int dmg)
    {
        canBeDmged = false;
        hp -= dmg;
        GetComponent<MeshRenderer>().material = feedbackMat;
        if(hp<=0) Destroy(gameObject);
        yield return new WaitForSeconds(.1f);
        GetComponent<MeshRenderer>().material = originMat;
        canBeDmged = true;
    }
}
