using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class representing checkpoint
 */
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    /**
     * calls game manager OnCheckpointEntered method
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.OnCheckpointEntered(this);
    }
}
