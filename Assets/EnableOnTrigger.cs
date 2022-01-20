using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class that handles logic of enabling an object
 * when player enters a trigger and disabling it
 * when player exits trigger
 */
public class EnableOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private bool isEnabled = true;
    
    /**
     * sets isEnabled to false 
     */
    public void Disable()
    {
        objectToEnable.SetActive(false);
        isEnabled = false; 
    }
    
    /**
     * sets isEnabled to true 
     */
    public void Enable()
    {
        isEnabled = true; 
    }
    
    /**
     * when player enters trigger it activates objectToEnable
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnabled)
        {
            objectToEnable.SetActive(true);
        }
    }
    
    /**
     * when player exits trigger it deactivates objectToEnable
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnabled)
        {
            objectToEnable.SetActive(false);
        }
    }
}
