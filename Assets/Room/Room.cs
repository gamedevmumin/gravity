using System;
using System.Collections;
using GeneralScripts.Gravity;
using UnityEngine;

namespace Room
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2 gravity;
        private bool _cooldown = false;

        public void TurnOnRoomGravity(GravityHandler gravityHandler)
        {
            gravityHandler.TurnOnRoomGravity(gravity);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var gravityHandler = other.gameObject.GetComponent<GravityHandler>();
           
            if (gravityHandler && !_cooldown)
            {
                //gravityHandler.TurnOnRoomGravity(gravity);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var gravityHandler = other.GetComponent<GravityHandler>();
            if (gravityHandler && !_cooldown)
            {
                //gravityHandler.TurnOffRoomGravity();
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
