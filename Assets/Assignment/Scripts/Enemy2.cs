using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2 : Enemy
{
    float timer;
    protected override void Start() // Overrides start
    {
        base.Start(); // Keeps start function of normal enemy
        currentHealth = 20; // Changes health
    }

    // Update is called once per frame
    protected override void Shoot()
    {
        {
            timer += 1;
            if (timer == 2) // Uses counter to half attack speed
            {

                Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                timer = 0; //resets counter
            }
        }
    }
}
