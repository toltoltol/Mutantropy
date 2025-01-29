using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    // How long the projectile exists before being destroyed
    public float lifetime = 5f;
    
    private Vector2 direction;
    public float speed;
    
    // Function to inform projectile what way it is going lol 
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void Start()
    {
        // Destroy the projectile after a certain time
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        // Move the projectile in the set direction
        transform.Translate( speed * Time.deltaTime * direction);
    }
}