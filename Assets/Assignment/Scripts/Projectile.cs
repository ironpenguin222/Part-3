using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile stats
    public float speed = 10f;
    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        // Check if collided with a projectile
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            Destroy(gameObject); // Projectiles are destroyed on collision with eachother

        }
    }

    protected virtual void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (!GetComponent<Renderer>().isVisible) // Checks if visible, if not it is destroyed
        {
            Destroy(gameObject);
        }
    }

}
