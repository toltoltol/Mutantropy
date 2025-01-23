using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    public float projectileSpeed = 10f; // Speed of the projectile
    public Transform firePoint; // Empty child GameObject marking the spawn position
    
    public float fireCooldown = 0.2f; // Cooldown time in seconds
    private float nextFireTime = 0f;


    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Vector2 direction = GetInputDirection();

            if (direction != Vector2.zero)
            {
                FireProjectile(direction);
                nextFireTime = Time.time + fireCooldown;
            }
        }
    }

    Vector2 GetInputDirection()
    {
        float horizontal = (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f);
        float vertical = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);

        // Create and normalize the direction vector
        Vector2 direction = new Vector2(horizontal, vertical);

        // Normalize if diagonal movement is detected
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        return direction;
    }

    void FireProjectile(Vector2 direction)
    {
        // Instantiate the projectile at the firePoint position
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        // Try to get the BulletProjectile script on the instantiated projectile
        BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
        if (bulletProjectile != null)
        {
            // Call the method to set the direction
            bulletProjectile.SetDirection(direction);
        }
    }
}