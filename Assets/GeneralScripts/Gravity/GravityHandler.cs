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

        private void Update()
        {
            /*transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * 4.5f);
            if (!roomCheckers[0].CurrentRoomName.Equals(roomCheckers[1].CurrentRoomName) ||
                roomCheckers[0].CurrentRoomName.Equals(previousRoomName)) return;
            previousRoomName = roomCheckers[0].CurrentRoomName;
            roomCheckers[0].CurrentRoom.TurnOnRoomGravity(this);*/
        }
        
        public void TurnOnRoomGravity(Vector2 gravity)
        {
            StartCoroutine(TurnOnRoomGravityCoroutine(gravity));
        }

        private IEnumerator TurnOnRoomGravityCoroutine(Vector2 gravity)
        {
            yield return new WaitForSeconds(0.5f);
            _isRoomGravityOn = true;
            _roomGravity = gravity;
            /*_directionManager.ToggleSide();*/
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
            /*_directionManager.ToggleSide();*/
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
                    Debug.Log(gameObject.tag);
                    constantForce2D.force = _roomGravity;
                    Debug.Log(_roomGravity.y);
                    _gravityInfo.gravityDirection = (int)(_roomGravity.y / Mathf.Abs(_roomGravity.y));
                    Debug.Log(_gravityInfo.gravityDirection);
                }
            }

            if (_isPersonalGravityOn)
            {
                foreach (var constantForce2D in constantForce2Ds)
                {
                    constantForce2D.force = _personalGravity;
                    _gravityInfo.gravityDirection = (int)(_personalGravity.y / Mathf.Abs(_personalGravity.y));
                    Debug.Log(_gravityInfo.gravityDirection);
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
                //     var normalizedForceDirection =_constantForce2D.force.normalized;
                
                /*var rotZ = Mathf.Atan2(normalizedForceDirection.y, normalizedForceDirection.x) * Mathf.Rad2Deg;
                if (normalizedForceDirection == Vector2.down)
                {
                    _newRotation = Quaternion.Euler (0f, 0f, 0f);
                    CurrentMovementAxis = Vector2.right;
                } else if (normalizedForceDirection == Vector2.up)
                {
                    _newRotation = Quaternion.Euler (0f, 0f, 180f);
                    CurrentMovementAxis = Vector2.right;
                } else if (normalizedForceDirection == Vector2.left)
                {
                    _newRotation = Quaternion.Euler (0f, 0f, 270f);
                    CurrentMovementAxis = -Vector2.up;
                } else if (normalizedForceDirection == Vector2.right)
                {
                    _newRotation = Quaternion.Euler (0f, 0f, 90f);
                    CurrentMovementAxis =  Vector2.up;
                }*/
            }
            else
            {
                foreach (var rb in (rbs))
                {
                    rb.gravityScale = 2f;
                    _gravityInfo.gravityDirection = 1;
                    Debug.Log(_gravityInfo.gravityDirection);
                }

                /*var normalizedForceDirection =_constantForce2D.force.normalized;

                var rotZ = Mathf.Atan2(normalizedForceDirection.y, normalizedForceDirection.x) * Mathf.Rad2Deg;
                _newRotation = Quaternion.Euler (0f, 0f, rotZ*-2 );*/
            }
            Debug.Log(_gravityInfo.gravityDirection);
        }
    }
}
