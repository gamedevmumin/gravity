using System.Collections;
using Interfaces;
using UnityEngine;

namespace PlayerController.Scripts
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private ScreeneFreezer screenFreezer;
        [SerializeField] private GameEvent playerDeath;

        private void Start()
        {
            screenFreezer = GameObject.Find("ScreenFreezer").GetComponent<ScreeneFreezer>();
        }
        
        public void TakeDamage(int damage)
        {
            screenFreezer.Freeze(0.1f);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            playerDeath.Raise();
        }
    }
}
