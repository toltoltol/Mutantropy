using System;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public float health = 3f;
    public float attackPower = 1.0f;
    public float attackSpeed = 0.5f;
    public float moveSpeed = 0.5f;
    
    public float attackRange = 5.0f;
    public float attackProjectileSpeed = 5.0f;


    // Get final move speed
    public float FinalMoveSpeed
    {
        get
        {
            return moveSpeed * GameMaster.GetEnemyMoveSpeedMultiplier();
        }
    }

    // Handle taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    // Handle dealing damage
    public void DealDamage(PlayerAttributes player)
    {
        if (player != null)
        {
            player.TakeDamage((int)Math.Ceiling(attackPower));
            Debug.Log($"Enemy dealt {(int)Math.Ceiling(attackPower)} damage to {player.name}.");
        }
    }

    // Handle enemy death
    private void Die()
    {
        Debug.Log("Enemy has died.");
        // Add death handling logic (e.g., remove enemy from the game)
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            TakeDamage(other.GetComponent<BulletProjectile>().damage);
            Destroy(other.gameObject);
        }    
    }
}