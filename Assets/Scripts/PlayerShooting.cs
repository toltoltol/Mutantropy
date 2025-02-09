using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float _fireCooldown = 0.3f;
    private float _nextFireTime = 0f;
    
    private PlayerAttributes _playerAttributes;
    public AudioSource audioSource;  // Reference to AudioSource
    public AudioClip shootSound;     // Reference to shooting sound

    // Buffer to store key presses
    public float inputBufferTime = 0.05f; // Time window for buffering inputs
    private float _inputTimer = 0f; // Timer for the input buffer
    private float _horizontalInput = 0f; // Accumulated horizontal input
    private float _verticalInput = 0f; // Accumulated vertical input

    private void Start()
    {
        _playerAttributes = GetComponent<PlayerAttributes>();
        _fireCooldown = 1 / _playerAttributes.attackSpeed;
    }

    void Update()
    {
        
        if (Time.time >= _nextFireTime)
        {
            // Accumulate input over the buffer time
            _inputTimer += Time.deltaTime;

            if (_inputTimer >= inputBufferTime)
            {
                Vector2 direction = new Vector2(_horizontalInput, _verticalInput);
                if (direction != Vector2.zero)
                {
                    FireProjectile(direction.normalized);
                    _fireCooldown = 1 / _playerAttributes.attackSpeed;
                    _nextFireTime = Time.time + _fireCooldown;
                }

                // Reset input after processing
                _horizontalInput = 0f;
                _verticalInput = 0f;
                _inputTimer = 0f;
            }

            // Update the input values during the buffer time
            _horizontalInput += (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f);
            _verticalInput += (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
        }
    }

    void FireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
        
        bulletProjectile.Init(_playerAttributes.attackRange, direction.normalized, _playerAttributes.attackProjectileSpeed, _playerAttributes.attackPower);

        // Play the shooting sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}