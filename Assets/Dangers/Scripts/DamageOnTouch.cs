using Interfaces;
using UnityEngine;

namespace Dangers.Scripts
{
    public class DamageOnTouch : MonoBehaviour, IDamaging
    {
        [SerializeField]
        private string whatToDamageTag;
    
        public void DealDamage(Transform target)
        {
            var damageable = target.GetComponent<IDamageable>();
            damageable.TakeDamage(0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(whatToDamageTag))
            {
                DealDamage(other.transform);
            }
        }
    }
}
