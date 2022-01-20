using Interfaces;
using UnityEngine;

namespace PlayerController.Scripts
{
    /**
     * class representing player controller
     */
    public class PlayerController : MonoBehaviour
    {
        private IMovement _movement;
        private Vector2 _movementInput;

        [SerializeField] private Rigidbody2D[] rbs;
        
        [SerializeField] private float speed = 100f;
        private IJumpingController _jumpingController;
        private bool _isPaused = false;
        
        private void Start()
        {
            _movement = GetComponent<IMovement>();
            _jumpingController = GetComponent<IJumpingController>();
        }

        
        private void Update()
        {
            _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            _jumpingController.ManageJumping();
        }

        private void FixedUpdate()
        {
            if ((Mathf.Abs(_movementInput.x) > 0f || Mathf.Abs(_movementInput.y) > 0f) && !_isPaused)
            {
                _movement.Move(_movementInput, speed);
            }
        }

        /**
         * sets isPaused field to given value
         */
        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}
