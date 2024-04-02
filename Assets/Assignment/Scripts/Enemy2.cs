using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2 : Enemy
{
    float timer;
    protected override void Start()
    {
        base.Start();
        currentHealth = 20;
    }

    // Update is called once per frame
    protected override void Shoot()
    {
        {
            timer += 1;
            if (timer == 2)
            {

                Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
