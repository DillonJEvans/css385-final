using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public GameObject projectilePrefab; // Prefab of the projectile
    public CameraFollower CameraFollower_;
    public SpawnManager SpawnManager_;
    public GameObject AimingReticle_;

    public int initialHealth = 4; // Initial health of the enemy
    public int currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            CameraFollower_.enabled = false;
            SpawnManager_.stop = true;
            Destroy(AimingReticle_);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (other.gameObject.CompareTag("Egg"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }
}
