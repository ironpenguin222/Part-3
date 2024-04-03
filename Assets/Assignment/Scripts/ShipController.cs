using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Ship stats and variables
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
        currentHealth = maxHealth; // Sets health at start

    }
    void Update()
    {
        if (currentHealth > maxHealth) // Makes sure health doesn't go beyond max
        {
            currentHealth = maxHealth;
        }
        float horizontalInput = Input.GetAxis("Horizontal"); // Checks for movement
        if (horizontalInput != 0f)
        {
            currentSpeed += horizontalInput * acceleration * Time.deltaTime; // adds the time and acceleration to make acceleration on ship.
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            if (currentSpeed > 0f) // Decelerating if speed is more than 0
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

        if (Input.GetButton("Fire1") && Time.time > nextFire) // Checks if button was pressed then fires
        {
            nextFire = Time.time + fireRate; // creates firerate
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canStrafe) // strafing
        {
            currentSpeed = 0f; // Reset current speed
            StartCoroutine(StrafeCoroutine(horizontalInput)); // starts coroutine to strafe
            StartCoroutine(StrafeCooldown()); // starts coroutine to cooldown strafe
        }

        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition); // Clamps player, can't leave game area
        transform.position = new Vector2(clampedX, transform.position.y);
    }


    void Shoot() // Shoots
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator StrafeCoroutine(float horizontalInput)
    {
        float strafeDirection = Mathf.Sign(horizontalInput); // Finds direction
        float startTime = Time.time;
        while (Time.time < startTime + strafeDuration) // while loop to make sure strafing is going on for its assigned duration
        {
            transform.Translate(Vector2.right * strafeDirection * strafeSpeed * Time.deltaTime); // strafes the player to the side
            yield return null;
        }
    }

    public void EndRound()
    {
        currentHealth += 40; // Regains health after each round
    }

    IEnumerator StrafeCooldown()
    {
        canStrafe = false;
        yield return new WaitForSeconds(strafeCooldown); // Waits, so that player must wait out the cooldown
        canStrafe = true;
    }

    public void TakeDamage(float damage) // Player take damage
    {
        currentHealth -= (int)damage; // Subtract health by damage
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Destroy(gameObject); // Dies
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
            // Checks if projectile hit it
        {
            
            TakeDamage(projectile.damage); // Take damage
            
            Destroy(other.gameObject);

        }
    }
}
