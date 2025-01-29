using System;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public int health = 3;
    public float attackPower = 1.0f;
    public float attackSpeed = 0.5f;
    public float moveSpeed = 0.5f;

    // Handle taking damage
    public void TakeDamage(float damage)
    {
        health -= (int)Math.Ceiling(damage);
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
}
