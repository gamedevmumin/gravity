using System.Collections;
using Interfaces;
using PlayerController.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace GeneralScripts.Gravity
{
    /**
     * class that handles logic behind changing gravity
     */
    public class GravityHandler : MonoBehaviour
    {
        private bool _isGlobalGravityOn = true;

        private bool _isRoomGravityOn;
        private Vector2 _roomGravity;

        private Vector2 _personalGravity;

        [SerializeField] private Rigidbody2D[] rbs;
        [SerializeField] private ConstantForce2D[] constantForce2Ds;
        [SerializeField] private GravityInfo _gravityInfo;

        /**
         * starts coroutine that delays change of room gravity
         */
        public void TurnOnRoomGravity(Vector2 gravity)
        {
            StartCoroutine(TurnOnRoomGravityCoroutine(gravity));
        }

        /**
         * sets room gravity of object to given gravity and calls ChangeGravity function
         */
        private IEnumerator TurnOnRoomGravityCoroutine(Vector2 gravity)
        {
            yield return new WaitForSeconds(0.5f);
            _isRoomGravityOn = true;
            _roomGravity = gravity;
            ChangeGravity();
        }
        
        /**
         * starts coroutine that delays change of room gravity
         */
        public void TurnOffRoomGravity()
        {
            StartCoroutine(TurnOffRoomGravityCoroutine());
        }

        /**
         * sets _isRoomGravityOn to false and calls ChangeGravity function
         */
        private IEnumerator TurnOffRoomGravityCoroutine()
        {
            yield return new WaitForSeconds(0f);
            _isRoomGravityOn = false;
            _roomGravity = Vector2.zero;
            ChangeGravity();
        }

        /**
         * sets force of all constant forces of object to room gravity and gravityScale of all rigidbodies of to 0
         * - if _isRoomGravityOn equals true 
         * or sets its rigidbodies gravityScale to 2 if _isRoomGravityOn is false
         */
        private void ChangeGravity()
        {
            if (_isRoomGravityOn)
            {
                foreach (var constantForce2D in constantForce2Ds)
                {
                    constantForce2D.force = _roomGravity;
                    if (gameObject.CompareTag("Player"))
                    {
                        _gravityInfo.gravityDirection = (int) (_roomGravity.y / Mathf.Abs(_roomGravity.y));
                    }
                }
            }

            if (!_isRoomGravityOn)
            {
                foreach (var constantForce2D in constantForce2Ds)
                {
                    constantForce2D.force = Vector2.zero;
                }
            }
        
            if (_isRoomGravityOn || !_isGlobalGravityOn)
            {
                foreach (var rb in (rbs))
                {
                    rb.gravityScale = 0f;
                }
            }
            else
            {
                foreach (var rb in rbs)
                {
                    rb.gravityScale = 2f;
                    if (gameObject.CompareTag("Player"))
                    {
                        _gravityInfo.gravityDirection = 1;
                    }
                }
                
            }
        }
    }
}
