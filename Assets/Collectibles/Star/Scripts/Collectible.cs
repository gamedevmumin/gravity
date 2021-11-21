using System;
using UnityEngine;

namespace Collectibles.Star.Scripts
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private GameObject collectedEffect;

        [SerializeField] private GameEvent onCollected;

        [SerializeField] private ScreeneFreezer screenFreezer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            screenFreezer.Freeze(0.03f);
            onCollected.Raise();
            Instantiate(collectedEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
