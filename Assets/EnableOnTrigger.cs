using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;

    public void Disable()
    {
        objectToEnable.SetActive(false);
        enabled = false; 
    }
    
    public void Enable()
    {
        enabled = true; 
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToEnable.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToEnable.SetActive(false);
        }
    }
}
