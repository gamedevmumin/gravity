using Interfaces;
using UnityEngine;

namespace PlayerController.Scripts
{
    public class PlayerDirectionManager : MonoBehaviour, IDirectionManager
    {
        private bool IsRight { get; set; }
        [SerializeField]
        private SpriteRenderer sR;

        private void Start()
        {
            IsRight = true;
        }

        private void Flip()
        {
            /*IsRight = !IsRight;
            sR.flipX = !sR.flipX;*/
        }

        public void ManageDirection(Vector2 input)
        {
            if ((input.x < 0 && IsRight) || (input.x > 0 && !IsRight) 
                || (input.y > 0 && IsRight) || (input.y < 0 && !IsRight))
                Flip();
        }

        public void ToggleSide()
        {
            IsRight = !IsRight;
        }
    }
}