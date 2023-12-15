using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemScript : MonoBehaviour
{
    [SerializeField] private GameObject uiToSpawn;
    [SerializeField] private GameObject canvasButtonObject;

    private bool isClaimable;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        uiToSpawn.SetActive(true);
        isClaimable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        uiToSpawn.SetActive(false);
        isClaimable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isClaimable) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<Controler>().breathUnlocked = true;
            canvasButtonObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
