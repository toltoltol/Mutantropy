using UnityEngine;
using System.Collections;

public class EnemyPeek : MonoBehaviour
{
    public Transform coverPosition;    // The position where the enemy stays in cover
    public Transform openPosition1;   // The first position where the enemy moves to peek out
    public Transform openPosition2;   // The second position where the enemy moves to peek out
    public float moveSpeed = 3f;       // Speed at which the enemy moves

    public float minCoverTime = 2f;    // Minimum time spent in cover
    public float maxCoverTime = 5f;    // Maximum time spent in cover
    public float minOpenTime = 1f;     // Minimum time spent in the open
    public float maxOpenTime = 3f;     // Maximum time spent in the open

    private Transform targetPosition;  // Current target position (cover or open)
    private bool isPeeking = false;    // Whether the enemy is currently peeking

    private void Start()
    {
        // Start in cover by default
        targetPosition = coverPosition;
        StartCoroutine(PeekBehavior());
    }

    private void Update()
    {
        // Move towards the current target position
        if (targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator PeekBehavior()
    {
        while (true)
        {
            // Stay in cover for a random amount of time
            targetPosition = coverPosition;
            yield return new WaitForSeconds(Random.Range(minCoverTime, maxCoverTime));

            // Randomly choose between the two open positions
            targetPosition = Random.value > 0.5f ? openPosition1 : openPosition2;
            yield return new WaitForSeconds(Random.Range(minOpenTime, maxOpenTime));
        }
    }
}
