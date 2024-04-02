using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile

{
    public float homingSpeed = 5f;
    public GameObject playerShip;

    protected override void Update()
    {
        if (playerShip != null)
        {
            
            Vector2 directionToPlayer = (playerShip.transform.position - transform.position).normalized;

           
            Vector2 newVelocity = Vector3.RotateTowards(transform.up, -directionToPlayer, homingSpeed * Time.deltaTime, 0f);
            transform.up = newVelocity; 
        }

        
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
