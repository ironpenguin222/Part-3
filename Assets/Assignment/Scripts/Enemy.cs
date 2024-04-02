using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
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
    private Vector3 targetPosition;
    private Levels levels;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        targetPosition = GetRandomPosition();
        levels = FindObjectOfType<Levels>();
    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {

            targetPosition = GetRandomPosition();
        }

        UpdateSpeed();

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(leftBoundary, rightBoundary);
        float randomY = transform.position.y;
        return new Vector2(randomX, randomY);
    }

    void UpdateSpeed()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

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
        currentHealth -= (int)damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Die
        Destroy(gameObject);
        levels.EnemyDestroyed();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile.CompareTag("Player"))
        {
            TakeDamage(projectile.damage);
            Destroy(other.gameObject);
        }




    }
}

