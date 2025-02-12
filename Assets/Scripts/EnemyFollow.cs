using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's Transform
    public float stopDistance = 1f; // Distance to stop from the player
    public bool flip;

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

        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }
}
