using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnmyZombie : EnemyBase
{
    public List<Transform> checkPointList = new List<Transform>();
    private Vector3 targetPos;
    private float targetX;

    public float lerpDuration = 5; //delai à partir duquel le mob change de direction
   
    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveLoop());
    }

    
    void Update()
    {
        transform.position += new Vector3((targetX - transform.position.x), 0, 0).normalized * moveSpeed * Time.deltaTime;
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
}
