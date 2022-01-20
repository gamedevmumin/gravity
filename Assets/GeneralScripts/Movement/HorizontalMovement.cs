using Interfaces;
using UnityEngine;

namespace GeneralScripts.Movement
{
    /**
     * class that handles horizontal movement logic
     */
    public class HorizontalMovement : MonoBehaviour, IMovement
    {
        [SerializeField]
        private Rigidbody2D _rb;

        /**
         * sets rigidbody velocity to simulate movement
         * @param direction - direction of movement
         * @param speed - speed of movement
         */
        public void Move(Vector2 direction, float speed)
        {
            _rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, _rb.velocity.y);
        }
    }
}
