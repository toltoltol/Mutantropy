using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    // Player stats with boundaries
    public float maxHealth = 12f;
    public float minHealth = 0f;
    public float currentHealth = 3f;

    public float attackPower = 1f;
    public float minAttackPower = 0.1f;
    public float maxAttackPower = 1000f;

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

    // New boolean to track if player is moving
    public bool isMoving;

    void Start()
    {
        // Initialize player health
        currentHealth = Mathf.Clamp(maxHealth, minHealth, maxHealth);
        attackPower = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
        attackSpeed = Mathf.Clamp(attackSpeed, minAttackSpeed, maxAttackSpeed);
        attackRange = Mathf.Clamp(attackRange, minAttackRange, maxAttackRange);
        attackProjectileSpeed = Mathf.Clamp(attackProjectileSpeed, minAttackProjectileSpeed, maxAttackProjectileSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
        
        //TODO decide if this should be moved
        DontDestroyOnLoad(gameObject); 
    }

    // Handle taking damage
    void Update()
    {
        // Example: set isMoving based on input
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        isMoving = (Mathf.Abs(horiz) > 0.1f || Mathf.Abs(vert) > 0.1f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0) Die();
    }

    // Handle dealing damage
    public void DealDamage(EnemyAttributes enemy)
    {
        if (enemy != null)
        {
            float clampedAttack = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
            enemy.TakeDamage(clampedAttack);
            Debug.Log($"Player dealt {clampedAttack} damage to {enemy.name}.");
        }
    }

    public void SetAttackSpeed(float amount)
    {
        attackSpeed = Mathf.Clamp(attackSpeed + amount, 
            minAttackSpeed, 
            maxAttackSpeed);
    }
    
    public void SetAttackPower(float amount)
    {
        attackPower = Mathf.Clamp(attackPower + amount, 
            minAttackPower, 
            maxAttackPower);
    }
    
    public void SetAttackProjectileSpeed(float amount)
    {
        attackProjectileSpeed = Mathf.Clamp(attackProjectileSpeed + amount, 
            minAttackProjectileSpeed, 
            maxAttackProjectileSpeed);
    }
    
    public void SetAttackRange(float amount)
    {
        attackRange = Mathf.Clamp(attackRange + amount, 
            minAttackRange, 
            maxAttackRange);
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
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(other.GetComponent<BulletProjectile>().damage);
            Destroy(other.gameObject);
        } else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Room2");
        }
    }
}
