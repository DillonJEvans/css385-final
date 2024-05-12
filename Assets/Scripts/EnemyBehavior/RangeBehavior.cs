using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBehavior : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float projectileCooldown = 2f;
    public float mediumRange = 15f;
    private Transform playerTransform;
    private float lastShotTime;
    private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastShotTime = Time.time;
        firePoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within medium range
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= mediumRange)
        {
            // Check if enough time has passed since the last shot
            if (Time.time >= lastShotTime + projectileCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        // Calculate direction towards the player
        Vector2 shootDirection = (playerTransform.position - transform.position).normalized;

        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Shoot in the direction of the player
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
    }
}
