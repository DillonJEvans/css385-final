using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public GameObject projectilePrefab; // Prefab of the projectile
    public CameraFollower CameraFollower_;
    public SpawnManager SpawnManager_;

    private Rigidbody2D rb;
    private Vector2 moveDirection; // Direction to move

    public float initialAlpha = 1.0f; // Initial alpha value
    public int initialHealth = 4; // Initial health of the enemy
    public int currentHealth; // Current health of the enemy
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = initialHealth;
    }

    void Update()
    {
        // Get movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Shoot when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Shoot();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        // rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * speed * 2; // Adjust speed as needed
    }

    public void TakeDamage()
    {
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
            CameraFollower_.enabled = false;
            SpawnManager_.stop = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (other.gameObject.CompareTag("Egg"))
        {
            TakeDamage();
        }
    }
}
