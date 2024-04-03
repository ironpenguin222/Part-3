using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //Asteroid stats
    float asteroidHP = 3;

    private void Update()
    {
        if (asteroidHP <= 0) // CHecks asteroid hp and destroys if 0
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        asteroidHP -= 1; // Asteroid takes damage on collision
    }
}
