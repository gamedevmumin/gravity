using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public void FollowPlayer(PlayerController.Scripts.PlayerController playerController)
   {
      GetComponent<CinemachineVirtualCamera>().Follow = playerController.transform;
   }
}
