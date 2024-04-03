using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    // Enemy Stats
    public float maxSpeed = 5f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float areaWidth = 10f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    public int currentHealth;
    public int maxHealth = 60;
    private float currentSpeed = 0f;
    private float nextFire = 0f;
    public Vector3 targetPosition;
    private Levels levels;
    protected virtual void Start()
    {
        // Setup The Base Enemy
        currentHealth = maxHealth;
        targetPosition = GetRandomPosition();
        levels = FindObjectOfType<Levels>();
    }

    void Update()
    {
        // Movement
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {

            targetPosition = GetRandomPosition(); // Enemy Moves Towards Random Destination
        }

        UpdateSpeed();

        if (Time.time > nextFire) // Checks To Make Enemy Fire
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    protected virtual void Shoot() // Shoots
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity); // Instantiate Bullet
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(leftBoundary, rightBoundary); // Gets x to move to randomly
        float randomY = transform.position.y; // Gets y to move to randomly
        return new Vector2(randomX, randomY); // Returns the vector
    }

    void UpdateSpeed()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition); // Checks distance to target

        // Acceleration
        if (distanceToTarget > 1f)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        }
        // Deceleration
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage; // Reduces health by damage amount
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Die
        Destroy(gameObject);
        levels.EnemyDestroyed(); // Tells levels that an enemy has been destroyed
    }

    void OnTriggerEnter2D(Collider2D other) // Collision
    {

        //Checks projectile to see if it's player's
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile.CompareTag("Player"))
        {
            TakeDamage(projectile.damage); // Takes damage
            Destroy(other.gameObject);
        }




    }
}

