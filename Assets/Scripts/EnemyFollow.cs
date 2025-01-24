using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's Transform
    public float stopDistance = 1f; // Distance to stop from the player

    private EnemyAttributes enemyAttributes;

    private void Awake()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();
        if (enemyAttributes == null)
        {
            Debug.LogError("EnemyAttributes component is missing on this enemy.");
        }
    }

    void Update()
    {
        // Check if the player reference is set
        if (player == null || enemyAttributes == null)
        {
            return;
        }

        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // Move towards the player if beyond the stop distance
        if (distance > stopDistance)
        {
            // Calculate direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy in the direction of the player
            transform.position += direction * enemyAttributes.moveSpeed * GameMaster.GetEnemyMoveSpeedMultiplier() * Time.deltaTime;
        }
    }
}
