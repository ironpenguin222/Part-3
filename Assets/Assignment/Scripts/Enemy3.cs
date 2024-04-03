using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy3 : Enemy
{
    float timer = 0;
    protected override void Start() // Overrides start
    {
        base.Start(); // Keeps start function of normal enemy
        currentHealth = 40; // Changes health
    }

    // Update is called once per frame
    protected override void Shoot()
    {
        float Direction = Random.Range(-1, 1); // Get random direction
        timer += 1;
        if (timer == 5) // Uses counter to check times
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            StartCoroutine(StrafeCoroutine(Direction));
            timer = 0; //resets counter
        }
        else
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator StrafeCoroutine(float horizontal)
    {
        float strafeDirection = Mathf.Sign(horizontal); // Finds direction
        float startTime = Time.time;
        while (Time.time < startTime + 2) // while loop to make sure strafing is going on for its assigned duration
        {
            transform.Translate(Vector2.right * strafeDirection * 8 * Time.deltaTime); // strafes the enemy to the side
            yield return null;
        }
    }
}

    


