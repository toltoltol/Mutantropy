using ItemScripts;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    //Disabled cause it breaks going to and from main menu
    // Singleton object
    // public static PlayerAttributes Instance;

    // Player stats with boundaries
    public float maxHealth = 12.0f;
    public float minHealth = 0.0f;
    public float currentHealth = 3.0f;

    public float attackPower = 1.0f;
    public float minAttackPower = 0.1f;
    public float maxAttackPower = 100f;

    //Attack cooldown = 1 over attack speed (inverse)
    public float attackSpeed = 1.0f;
    public float minAttackSpeed = 0.55f;
    public float maxAttackSpeed = 10.0f;

    //Measured in seconds the projectile exists for
    public float attackRange = 1.0f;
    public float minAttackRange = 0.55f;
    public float maxAttackRange = 10.0f;

    public float attackProjectileSpeed = 1.0f;
    public float minAttackProjectileSpeed = 0.55f;
    public float maxAttackProjectileSpeed = 25.0f;

    public float moveSpeed = 1.0f;
    public float minMoveSpeed = 0.65f;
    public float maxMoveSpeed = 3.0f;

    // New boolean to track if player is moving
    public bool isMoving;
    
    //TODO confirm and remove this as cam trigger now handles this 
    private int currentRoomNumber = 1;

    private SpriteRenderer _spr;
    private Color _color;
    public float flashDuration = 0.1f;

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else if (Instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {
        // Initialize player health
        currentHealth = Mathf.Clamp(maxHealth, minHealth, maxHealth);
        attackPower = Mathf.Clamp(attackPower, minAttackPower, maxAttackPower);
        attackSpeed = Mathf.Clamp(attackSpeed, minAttackSpeed, maxAttackSpeed);
        attackRange = Mathf.Clamp(attackRange, minAttackRange, maxAttackRange);
        attackProjectileSpeed = Mathf.Clamp(attackProjectileSpeed, minAttackProjectileSpeed, maxAttackProjectileSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
        _spr = GetComponent<SpriteRenderer>();
        _color = _spr.color;
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
 //       Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        StartCoroutine(FlashRed());

        if (currentHealth <= 0) Die();
    }

    private IEnumerator FlashRed()
    {
        _spr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        _spr.color = _color;
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

    public void IncreaseAttackSpeed(float amount)
    {
        attackSpeed = Mathf.Clamp(attackSpeed + amount,
            minAttackSpeed,
            maxAttackSpeed);
    }

    public void IncreaseAttackPower(float amount)
    {
        attackPower = Mathf.Clamp(attackPower + amount,
            minAttackPower,
            maxAttackPower);
    }

    public void IncreaseAttackProjectileSpeed(float amount)
    {
        attackProjectileSpeed = Mathf.Clamp(attackProjectileSpeed + amount,
            minAttackProjectileSpeed,
            maxAttackProjectileSpeed);
    }

    public void IncreaseAttackRange(float amount)
    {
        attackRange = Mathf.Clamp(attackRange + amount,
            minAttackRange,
            maxAttackRange);
    }

    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + amount, 
            minMoveSpeed, 
            maxMoveSpeed);
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount,
            minHealth,
            maxHealth);
    }

    public void IncreaseNothing()
    {
    }


    // Handle player death
    private void Die()
    {
        Debug.Log("Player has died.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(other.GetComponent<BulletProjectile>().damage);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().UseItem(this);
        }
        //TODO confirm and remove this as cam trigger now handles this 
        else if (other.CompareTag("Finish"))
        {
            currentRoomNumber++;
            RoomManager.LoadNextRoom();
        }
    }
}
