using System.Collections;
using Interfaces;
using UnityEngine;

namespace PlayerController.Scripts
{
    /**
     * class that handles logic of player being damaged
     */
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private ScreeneFreezer screenFreezer;
        [SerializeField] private GameEvent playerDeath;

        private void Start()
        {
            screenFreezer = GameObject.Find("ScreenFreezer").GetComponent<ScreeneFreezer>();
        }
        
        /**
         * instantiates particles, destroys object of player and raises playerDeath event
         */
        public void TakeDamage(int damage)
        {
            AudioManager.Instance.PlaySound("Hurt");
            screenFreezer.Freeze(0.1f);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            playerDeath.Raise();
        }
    }
}
