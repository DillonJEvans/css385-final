using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform; // Reference to the player's transform
    public float moveSpeed = 2f; // Speed at which the enemy moves towards the player
    public float initialAlpha = 1.0f; // Initial alpha value
    public int initialHealth = 4; // Initial health of the enemy
    private int currentHealth; // Current health of the enemy

    //Dps Revision
    private float last_damage_time;
    public float damage_cooldown = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = initialHealth;

        // Find the player GameObject and get its transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        if (Time.time > last_damage_time + damage_cooldown)
        {
            last_damage_time = Time.time;
            currentHealth--;

            // Calculate new alpha value (80% of the previous value)
            float newAlpha = Mathf.Clamp(spriteRenderer.color.a * 0.8f, 0f, 1f);

            // Update alpha value
            Color newColor = spriteRenderer.color;
            newColor.a = newAlpha;
            spriteRenderer.color = newColor;

            // Check if health is depleted
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
