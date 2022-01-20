using System.Collections;
using Interfaces;
using PlayerController.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace GeneralScripts.Gravity
{
    public class GravityHandler : MonoBehaviour
    {
        private bool _isGlobalGravityOn = true;

        private bool _isRoomGravityOn = false;
        private Vector2 _roomGravity;

        [FormerlySerializedAs("_roomCheckers")] [SerializeField] private RoomChecker[] roomCheckers;
        
        private bool _isPersonalGravityOn = false;

        private Vector2 _personalGravity;

        public Vector2 CurrentMovementAxis { get; private set; }
        
        [SerializeField] private Rigidbody2D[] rbs;
        [SerializeField] private ConstantForce2D[] constantForce2Ds;
        [SerializeField] private GravityInfo _gravityInfo;

        public void TurnOnRoomGravity(Vector2 gravity)
        {
            StartCoroutine(TurnOnRoomGravityCoroutine(gravity));
        }

        private IEnumerator TurnOnRoomGravityCoroutine(Vector2 gravity)
        {
            yield return new WaitForSeconds(0.5f);
            _isRoomGravityOn = true;
            _roomGravity = gravity;
            ChangeGravity();
        }
        
        
        public void TurnOffRoomGravity()
        {
            StartCoroutine(TurnOffRoomGravityCoroutine());
        }

        private IEnumerator TurnOffRoomGravityCoroutine()
        {
            yield return new WaitForSeconds(0f);
            _isRoomGravityOn = false;
            _roomGravity = Vector2.zero;
            ChangeGravity();
        }
    
        public void TurnOnPersonalGravity(Vector2 gravity)
        {
            _isPersonalGravityOn = true;
            _personalGravity = gravity;
            ChangeGravity();
        }

        public void TurnOffPersonalGravity()
        {
            _isPersonalGravityOn = false;
            _personalGravity = Vector2.zero;
            ChangeGravity();
        }

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

            if (_isPersonalGravityOn)
            {
                foreach (var constantForce2D in constantForce2Ds)
                {
                    constantForce2D.force = _personalGravity;
                    if (gameObject.CompareTag("Player"))
                    {
                        _gravityInfo.gravityDirection = (int) (_personalGravity.y / Mathf.Abs(_personalGravity.y));
                    }
                }
            }

            if (!_isPersonalGravityOn && !_isRoomGravityOn)
            {
                foreach (var constantForce2D in constantForce2Ds)
                {
                    constantForce2D.force = Vector2.zero;
                }
            }
        
            if (_isPersonalGravityOn || _isRoomGravityOn || !_isGlobalGravityOn)
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
