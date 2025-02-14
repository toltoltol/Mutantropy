using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttributes : MonoBehaviour
{
    public float health = 3f;
    public float attackPower = 1.0f;
    public float attackSpeed = 0.5f;
    public float moveSpeed = 0.5f;
    
    public float attackRange = 5.0f;
    public float attackProjectileSpeed = 5.0f;
    
    //This stores the possible items an enemy can drop
    public GameObject[] dropsList;

    private SpriteRenderer _spr;
    private Color _color;
    public float flashDuration = 0.1f;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _color = _spr.color;
    }

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

        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        _spr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        _spr.color = _color;
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
        if (!gameObject.CompareTag("Boss"))
        {
            Debug.Log("Enemy has died.");

            // Add death handling logic (e.g., remove enemy from the game)

            // drop an item
            DropItem();

            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);

            SceneManager.LoadScene("WinScreen");
        }

    }
    
    private void DropItem()
    {
        if (dropsList.Length == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, dropsList.Length);
        GameObject itemToSpawn = dropsList[randomIndex];

        Instantiate(itemToSpawn, transform.position, Quaternion.identity); // Spawn item at enemy's position
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