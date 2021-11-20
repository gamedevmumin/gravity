using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.OnCheckpointEntered(this);
    }
}
