using GeneralScripts.Gravity;
using Interfaces;
using UnityEngine;

namespace PlayerController.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private IMovement _movement;
        private IDirectionManager _directionManager;
        private GravityHandler _gravityHandler;
        private Vector2 _movementInput;

        [SerializeField] private Rigidbody2D[] rbs;
        
        [SerializeField] private float speed = 100f;

        private bool _isPaused = false;
        
        private void Start()
        {
            _movement = GetComponent<IMovement>();
            _directionManager = GetComponent<IDirectionManager>();
           // _gravityHandler = GetComponent<GravityHandler>();
        }


        private void Update()
        {
            _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            _directionManager.ManageDirection(_movementInput);
            if (Input.GetKeyDown(KeyCode.Space) && !_isPaused)
            {
                foreach (var rb in (rbs))
                {
                    rb.velocity = new Vector2(rb.velocity.x, 15f);
                }
            }
        }

        private void FixedUpdate()
        {
            if ((Mathf.Abs(_movementInput.x) > 0f || Mathf.Abs(_movementInput.y) > 0f) && !_isPaused)
            {
                _movement.Move(_movementInput, speed);
            }
        }

        public void SetPaused(bool isPaused)
        {
            this._isPaused = isPaused;
        }
    }
}
