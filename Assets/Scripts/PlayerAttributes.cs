using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Player stats with boundaries
    public int maxHealth = 12;
    public int minHealth = 1;
    public int currentHealth = 3;

    public float attackPower = 1.0f;
    public float minAttackPower = 0.1f;
    public float maxAttackPower = 1000.0f;

    public float attackSpeed = 1.0f;
    public float minAttackSpeed = 0.5f;
    public float maxAttackSpeed = 2.0f;

    public float moveSpeed = 1.0f;
    public float minMoveSpeed = 0.5f;
    public float maxMoveSpeed = 2.0f;

    private void Start()
    {
        // Initialize player health
        currentHealth = Mathf.Clamp(maxHealth, minHealth, maxHealth);
        attackPower = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
        attackSpeed = Mathf.Clamp(attackSpeed, minAttackSpeed, maxAttackSpeed);
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
}
