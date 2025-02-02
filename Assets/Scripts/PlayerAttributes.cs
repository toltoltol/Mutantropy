using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
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

    // New boolean to track if player is moving
    public bool isMoving;

    void Start()
    {
        currentHealth = Mathf.Clamp(maxHealth, minHealth, maxHealth);
        attackPower = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
        attackSpeed = Mathf.Clamp(attackSpeed, minAttackSpeed, maxAttackSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    void Update()
    {
        // Example: set isMoving based on input
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        isMoving = (Mathf.Abs(horiz) > 0.1f || Mathf.Abs(vert) > 0.1f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0) Die();
    }

    public void DealDamage(EnemyAttributes enemy)
    {
        if (enemy != null)
        {
            float clampedAttack = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
            enemy.TakeDamage(clampedAttack);
            Debug.Log($"Player dealt {clampedAttack} damage to {enemy.name}.");
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Handle player death logic
    }
}
