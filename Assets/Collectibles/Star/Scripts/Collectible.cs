using System;
using UnityEngine;

namespace Collectibles.Star.Scripts
{
    /**
     * class representing collectible object
     */
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private GameObject collectedEffect;

        [SerializeField] private GameEvent onCollected;

        [SerializeField] private ScreeneFreezer screenFreezer;

        /**
         * initializes classes screenFreezer field
         */
        public void Initialize(ScreeneFreezer screenFreezer)
        {
            this.screenFreezer = screenFreezer;
        }

        /**
         * if colliding object is player star is collected and particle effect is spawned
         */
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            AudioManager.Instance.PlaySound("Collect");
            screenFreezer.Freeze(0.03f);
            onCollected.Raise();
            Instantiate(collectedEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
