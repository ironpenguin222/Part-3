using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float strafeSpeed = 10f;
    public float strafeDuration = 0.5f;
    public float strafeCooldown = 1f;
    public float maxHorizontalPosition = 2.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    private int currentHealth;
    public int maxHealth = 100;
    private float currentSpeed = 0f;
    private float nextFire = 0f;
    private static bool canStrafe = true;


    void Start()
    {
        currentHealth = maxHealth;

    }
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0f)
        {
            currentSpeed += horizontalInput * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(0f, currentSpeed);
            }
            else if (currentSpeed < 0f)
            {
                currentSpeed += deceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(0f, currentSpeed);
            }
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canStrafe)
        {
            currentSpeed = 0f; // Reset current speed
            StartCoroutine(StrafeCoroutine(horizontalInput));
            StartCoroutine(StrafeCooldown());
        }

        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator StrafeCoroutine(float horizontalInput)
    {
        float strafeDirection = Mathf.Sign(horizontalInput);
        float startTime = Time.time;
        while (Time.time < startTime + strafeDuration)
        {
            transform.Translate(Vector2.right * strafeDirection * strafeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void EndRound()
    {
        currentHealth += 40;
    }

    IEnumerator StrafeCooldown()
    {
        canStrafe = false;
        yield return new WaitForSeconds(strafeCooldown);
        canStrafe = true;
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
        
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            
            TakeDamage(projectile.damage);
            
            Destroy(other.gameObject);

        }
    }
}
