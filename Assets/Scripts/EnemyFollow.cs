using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's Transform
    public float stopDistance = 1f; // Distance to stop from the player
   
    private bool facingRight;

    private EnemyAttributes enemyAttributes;
    

    private void Awake()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();

        if (enemyAttributes == null)
        {
            Debug.LogError("EnemyAttributes component is missing on this enemy.");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                Debug.LogError("Player Transform not found! Make sure the player has the correct tag.");
            }
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
            Debug.Log("Player beyond stop distance");
            // Calculate direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Flip sprite based on the horizontal direction
            if (direction.x > 0 && !facingRight)
            {
                
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                
                Flip();
            }
        }

        Vector3 scale = transform.localScale;

        

        transform.localScale = scale;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * (facingRight ? 1 : -1), transform.localScale.y, transform.localScale.z);
    }

}
