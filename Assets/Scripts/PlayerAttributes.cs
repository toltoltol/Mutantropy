using System;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Player stats with boundaries
    public int maxHealth = 12;
    public int minHealth = 0;
    public int currentHealth = 3;

    public float attackPower = 1.0f;
    public float minAttackPower = 0.1f;
    public float maxAttackPower = 1000.0f;

    //Attack cooldown = 1 over attack speed (inverse)
    public float attackSpeed = 0.5f;
    public float minAttackSpeed = 3.0f;
    public float maxAttackSpeed = 10.0f;
    
    //Measured in seconds the projectile exists for
    public float attackRange = 5.0f;
    public float minAttackRange = 0.5f;
    public float maxAttackRange = 10.0f;
    
    public float attackProjectileSpeed = 5.0f;
    public float minAttackProjectileSpeed = 0.5f;
    public float maxAttackProjectileSpeed = 10.0f;

    public float moveSpeed = 1.0f;
    public float minMoveSpeed = 0.5f;
    public float maxMoveSpeed = 2.0f;
    
    private void Start()
    {
        // Initialize player health
        currentHealth = Mathf.Clamp(maxHealth, minHealth, maxHealth);
        attackPower = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
        attackSpeed = Mathf.Clamp(attackSpeed, minAttackSpeed, maxAttackSpeed);
        attackRange = Mathf.Clamp(attackRange, minAttackRange, maxAttackRange);
        attackProjectileSpeed = Mathf.Clamp(attackProjectileSpeed, minAttackProjectileSpeed, maxAttackProjectileSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    // Handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle dealing damage
    public void DealDamage(EnemyAttributes enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(Mathf.Clamp(attackPower, minAttackPower, maxAttackPower));
            Debug.Log($"Player dealt {attackPower} damage to {enemy.name}.");
        }
    }

    // Handle player death
    private void Die()
    {
        Debug.Log("Player has died.");
        // Add death handling logic (e.g., respawn or game over)
    }
    
    //I'm slapping the collision handling in here but im not entirely sure that is correct
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ProjectileBase"))
        {
            TakeDamage(1);
        }
    }
}
