using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Transform firePoint;
    public float fireCooldown = 0.3f;
    private float nextFireTime = 0f;

    // Buffer to store key presses
    public float inputBufferTime = 0.05f; // Time window for buffering inputs
    private float inputTimer = 0f; // Timer for the input buffer
    private float horizontalInput = 0f; // Accumulated horizontal input
    private float verticalInput = 0f; // Accumulated vertical input

    void Update()
    {
        
        if (Time.time >= nextFireTime)
        {
            // Accumulate input over the buffer time
            inputTimer += Time.deltaTime;

            if (inputTimer >= inputBufferTime)
            {
                Vector2 direction = new Vector2(horizontalInput, verticalInput);
                if (direction != Vector2.zero)
                {
                    FireProjectile(direction.normalized);
                    nextFireTime = Time.time + fireCooldown;
                }

                // Reset input after processing
                horizontalInput = 0f;
                verticalInput = 0f;
                inputTimer = 0f;
            }

            // Update the input values during the buffer time
            horizontalInput += (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f);
            verticalInput += (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
        }
    }

    void FireProjectile(Vector2 direction)
    {
        // Normalize the direction vector before firing
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
        if (bulletProjectile != null)
        {
            bulletProjectile.SetDirection(direction);
        }
    }
}
