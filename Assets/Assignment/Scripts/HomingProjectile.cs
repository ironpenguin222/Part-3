using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile

{
    // Variables
    public float homingSpeed = 5f;
    public GameObject playerShip;

    protected override void Update()
    {
        if (playerShip != null)
        {
            
            Vector2 directionToPlayer = (playerShip.transform.position - transform.position).normalized; // Find out direction towards player

           
            Vector2 newVelocity = Vector3.RotateTowards(transform.up, -directionToPlayer, homingSpeed * Time.deltaTime, 0f); // Rotate towards player and gain velocity towards
            transform.up = newVelocity; 
        }

        
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        
        if (!GetComponent<Renderer>().isVisible) // When offscreen delete
        {
            Destroy(gameObject);
        }
    }
}
