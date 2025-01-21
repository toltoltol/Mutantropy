using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's Transform
    public float speed = 5f;       // Speed at which the enemy moves
    public float stopDistance = 2f; // Distance to stop from the player

    void Update()
    {
        // Check if the player reference is set
        if (player == null)
        {
            Debug.LogWarning("Player not assigned to the enemy.");
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
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
