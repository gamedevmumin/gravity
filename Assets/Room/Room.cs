using System;
using System.Collections;
using System.Collections.Generic;
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
        private List<GravityHandler> _gravityHandlers;
        private bool _cooldown = false;

        public RoomInfo RoomInfo => roomInfo;

        private void Awake()
        {
            _gravityHandlers = new List<GravityHandler>();
        }
        
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
            }

            if (gravityHandler && !_cooldown)
            {
                _gravityHandlers.Add(gravityHandler);
                gravityHandler.TurnOnRoomGravity(roomInfo.Gravity);
            }
        }
        

        public void ChangeRoomGravity(Vector2 roomGravity)
        {
            roomInfo.Gravity = roomGravity;
            foreach (var gravityHandler in _gravityHandlers)
            {
                gravityHandler.TurnOnRoomGravity(roomGravity);
            }

        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var gravityHandler = other.GetComponent<GravityHandler>();
            if (gravityHandler && !_cooldown)
            {
                gravityHandler.TurnOffRoomGravity();
                _gravityHandlers.Remove(gravityHandler);
            }
        }
    }
}

