using System;
using System.Collections;
using Cinemachine;
using GeneralScripts.Gravity;
using UnityEngine;

namespace Room
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomInfo roomInfo;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CinemachineVirtualCamera roomCamera;
        [SerializeField] private Rigidbody2D[] rbs;
        private GravityHandler _gravityHandler;
        private bool _cooldown = false;

        public RoomInfo RoomInfo => roomInfo;

        public void TurnOnRoomGravity(GravityHandler gravityHandler)
        {
            gravityHandler.TurnOnRoomGravity(roomInfo.Gravity);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var gravityHandler =  other.gameObject.GetComponent<GravityHandler>();
            if (other.CompareTag("Player"))
            {
                gameManager.SwitchRoom(roomCamera);
                _gravityHandler = gravityHandler;
            }
            else if (other.CompareTag("Box"))
            {
                gravityHandler.TurnOnRoomGravity(roomInfo.Gravity);
            }
            
            if (_gravityHandler && !_cooldown)
            {
                _gravityHandler.TurnOnRoomGravity(roomInfo.Gravity);
            }
        }
        

        public void ChangeRoomGravity(Vector2 roomGravity)
        {
            roomInfo.Gravity = roomGravity;
            if (_gravityHandler)
            {
                _gravityHandler.TurnOnRoomGravity(roomGravity);
            }
            
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var gravityHandler = other.GetComponent<GravityHandler>();
            if (gravityHandler && !_cooldown)
            {
                gravityHandler.TurnOffRoomGravity();
                if (other.CompareTag("Player"))
                {
                    _gravityHandler = null;
                }
            }
        }

        private IEnumerator StartCooldown()
        {
            _cooldown = true;
            yield return new WaitForSeconds(0.05f);
            _cooldown = false;
        }
    }
}

