using Interfaces;
using UnityEngine;

namespace GeneralScripts.Movement
{
    public class HorizontalMovement : MonoBehaviour, IMovement
    {
        [SerializeField]
        private Rigidbody2D _rb;

        public void Move(Vector2 direction, float speed)
        {
            
            if (direction.x != 0)
            {
                _rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, _rb.velocity.y);
            } else if (direction.y != 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, direction.y * speed * Time.fixedDeltaTime);
            }
        }
    }
}
