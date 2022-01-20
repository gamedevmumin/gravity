using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/**
 * class representing camera controller
 */
public class CameraController : MonoBehaviour
{
    /**
     * sets camera it is attached to to follow player
     * @param playerController - player to follow
     */
   public void FollowPlayer(PlayerController.Scripts.PlayerController playerController)
   {
      GetComponent<CinemachineVirtualCamera>().Follow = playerController.transform;
   }
}
