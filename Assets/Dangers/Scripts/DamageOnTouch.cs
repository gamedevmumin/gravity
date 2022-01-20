using Interfaces;
using UnityEngine;

namespace Dangers.Scripts
{
    /**
     * class holding logic of damaging player
     */
    public class DamageOnTouch : MonoBehaviour, IDamaging
    {
        [SerializeField]
        private string whatToDamageTag;
    
        /**
         * calls TakeDamage method of target
         */
        public void DealDamage(Transform target)
        {
            var damageable = target.GetComponent<IDamageable>();
            damageable.TakeDamage(0);
        }

        /**
         * if colliding object matches tag given in whatToDamageTag field, DealDamage method is called
         */
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(whatToDamageTag))
            {
                DealDamage(other.transform);
            }
        }
    }
}
