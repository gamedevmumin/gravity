using Interfaces;
using PlayerController.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace GeneralScripts.Gravity
{
    public class GravityHandler : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private ConstantForce2D _constantForce2D;
    
        private bool _isGlobalGravityOn = true;

        private bool _isRoomGravityOn = false;
        private Vector2 _roomGravity;

        private string previousRoomName;
        [FormerlySerializedAs("_roomCheckers")] [SerializeField] private RoomChecker[] roomCheckers;
        
        private bool _isPersonalGravityOn = false;

        private Vector2 _personalGravity;

        private Quaternion _newRotation;
        private IDirectionManager _directionManager;

        public Vector2 CurrentMovementAxis { get; private set; }
        
        private void Awake()
        {
            _rb = GetComponent <Rigidbody2D>();
            _directionManager = GetComponent<IDirectionManager>();
            _constantForce2D = GetComponent<ConstantForce2D>();
            _newRotation = transform.rotation;
        }

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
            _isRoomGravityOn = true;
            _roomGravity = gravity;
            _directionManager.ToggleSide();
            ChangeGravity();
        }

        public void TurnOffRoomGravity()
        {
            _isRoomGravityOn = false;
            _roomGravity = Vector2.zero;
            _directionManager.ToggleSide();
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
                _constantForce2D.force = _roomGravity;
            }

            if (_isPersonalGravityOn)
            {
                _constantForce2D.force = _personalGravity;
            }

            if (!_isPersonalGravityOn && !_isRoomGravityOn)
            {
                _constantForce2D.force = Vector2.zero;
            }
        
            if (_isPersonalGravityOn || _isRoomGravityOn || !_isGlobalGravityOn)
            {
                _rb.gravityScale = 0f;
                var normalizedForceDirection =_constantForce2D.force.normalized;
                
                var rotZ = Mathf.Atan2(normalizedForceDirection.y, normalizedForceDirection.x) * Mathf.Rad2Deg;
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
                }
            }
            else
            {
                _rb.gravityScale = 2f;
                var normalizedForceDirection =_constantForce2D.force.normalized;

                var rotZ = Mathf.Atan2(normalizedForceDirection.y, normalizedForceDirection.x) * Mathf.Rad2Deg;
                _newRotation = Quaternion.Euler (0f, 0f, rotZ*-2 );
            }
        }
    }
}
