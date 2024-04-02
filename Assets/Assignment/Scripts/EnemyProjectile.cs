using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{

    protected override void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

}
