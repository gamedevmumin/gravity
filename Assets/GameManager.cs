using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController.Scripts.PlayerController playerController;

    [SerializeField] private Checkpoint currentCheckpoint;

    [SerializeField] private float timeToSpawn;

    public void OnCheckpointEntered(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(DelayBeforeRespawn());
    }

    private IEnumerator DelayBeforeRespawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        playerController.transform.position = currentCheckpoint.transform.position;
        playerController.gameObject.SetActive(true);
    }
    
}
