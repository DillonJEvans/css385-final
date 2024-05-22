using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform; // Reference to the player's transform
    public float moveSpeed = 2f; // Speed at which the enemy moves towards the player
    public int initialHealth = 4; // Initial health of the enemy
    public int currentHealth; // Current health of the enemy

    //Dps Revision
    private float last_damage_time;
    public float damage_cooldown = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = initialHealth;

        // Find the player GameObject and get its transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Stop the agent from rotating to face the player,
        // which would cause them to not be visible to the camera.
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        // Move towards the player
        if (playerTransform != null)
        {
            // Vector3 direction = (playerTransform.position - transform.position).normalized;
            // transform.position += direction * moveSpeed * Time.deltaTime;
            agent.destination = playerTransform.position;
        }
    }

    public void TakeDamage()
    {
        if (Time.time > last_damage_time + damage_cooldown)
        {
            last_damage_time = Time.time;
            currentHealth--;

            // Check if health is depleted
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
