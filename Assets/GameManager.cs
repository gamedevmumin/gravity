using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController.Scripts.PlayerController playerController;

    [SerializeField] private Checkpoint currentCheckpoint;

    [SerializeField] private float timeToSpawn;

    [SerializeField] private CinemachineVirtualCamera currentCamera;
    
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

    public void SwitchRoom(CinemachineVirtualCamera roomCamera)
    {
        StartCoroutine(SwitchRoomDelay(roomCamera));
    }


    public void SwitchCamera(CinemachineVirtualCamera roomCamera)
    {
        currentCamera.Priority = 0;
        currentCamera = roomCamera;
        currentCamera.Priority = 1;
    }
    private IEnumerator SwitchRoomDelay(CinemachineVirtualCamera roomCamera)
    {
        SwitchCamera(roomCamera);
        playerController.SetPaused(true);
        yield return new WaitForSeconds(0f);
        playerController.SetPaused(false);
    }
    
}
