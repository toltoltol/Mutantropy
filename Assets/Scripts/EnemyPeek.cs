using UnityEngine;
using System.Collections;

public class EnemyPeek : MonoBehaviour
{
    public Transform coverPosition;    // The position where the enemy stays in cover
    public Transform openPosition1;   // The first position where the enemy moves to peek out
    public Transform openPosition2;   // The second position where the enemy moves to peek out

    public float minCoverTime = 2f;    // Minimum time spent in cover
    public float maxCoverTime = 5f;    // Maximum time spent in cover
    public float minOpenTime = 2f;     // Minimum time spent in the open
    public float maxOpenTime = 5f;     // Maximum time spent in the open

    private Transform targetPosition;  // Current target position (cover or open)
    private EnemyAttributes enemyAttributes;

    private void Start()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();
        if (enemyAttributes == null)
        {
            Debug.LogError("EnemyAttributes component is missing on this enemy.");
            enabled = false;
            return;
        }

        // Start in cover by default
        targetPosition = coverPosition;
        StartCoroutine(PeekBehavior());
    }

    private void Update()
    {
        // Move towards the current target position
        if (targetPosition != null && enemyAttributes != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, enemyAttributes.moveSpeed * GameMaster.GetEnemyMoveSpeedMultiplier() * Time.deltaTime);
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
