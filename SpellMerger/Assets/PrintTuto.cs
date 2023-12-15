using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintTuto : MonoBehaviour
{
    public GameObject toPrint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        toPrint.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        toPrint.SetActive(false);
    }
}
