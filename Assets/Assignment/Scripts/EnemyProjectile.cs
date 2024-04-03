using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{

    protected override void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime); // Projectile moves

        if (!GetComponent<Renderer>().isVisible) // When offscreen delete
        {
            Destroy(gameObject);
        }
    }

}
