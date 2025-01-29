using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;

    private Transform player;
    private EnemyAttributes enemyAttributes;
    private float detectionRadius = 10f;
    private float chaseStopDistance = 15f;
    private float searchDuration = 5f;
    private float searchTimer;

    void Start()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Patrol:
                UpdatePatrol();
                break;
            case EnemyState.Chase:
                UpdateChase();
                break;
            case EnemyState.Search:
                UpdateSearch();
                break;
            case EnemyState.Retreat:
                UpdateRetreat();
                break;
        }
    }

    private void UpdateIdle()
    {
        // Do idle behavior (e.g., stand still or idle animation)
        // Transition example: after some idle time, go to Patrol
        if (Time.frameCount % 300 == 0) // pretend "timer"
        {
            TransitionToState(EnemyState.Patrol);
        }

        // If player is in detection radius, transition to chase
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            TransitionToState(EnemyState.Chase);
        }
    }

    private void UpdatePatrol()
    {
        // Basic patrolling logic:
        // Move along waypoints or random roam. Example code:
        PatrolBehavior();

        // If player is detected, transition to chase
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            TransitionToState(EnemyState.Chase);
        }
    }

    private void UpdateChase()
    {
        // Move toward player
        ChaseBehavior();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > chaseStopDistance)
        {
            // Lost sight, transition to Search
            TransitionToState(EnemyState.Search);
        }
        // Could also add transition to Retreat if health is low, etc.
    }

    private void UpdateSearch()
    {
        // Enemy has lost sight of player, so it searches
        SearchBehavior();

        searchTimer += Time.deltaTime;
        if (searchTimer >= searchDuration)
        {
            // Stop searching; go idle or back to patrol
            TransitionToState(EnemyState.Idle);
        }

        // If the player is detected again, go to chase
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            TransitionToState(EnemyState.Chase);
        }
    }

    private void UpdateRetreat()
    {
        // The enemy retreats (e.g., runs to a safe spot)
        RetreatBehavior();

        // Could check if the safe spot is reached, then go idle
        if (ReachedSafeSpot())
        {
            TransitionToState(EnemyState.Idle);
        }
    }

    //===========================
    // Helper Methods
    //===========================

    private void TransitionToState(EnemyState newState)
    {
        // OnExit code for current state (if needed)
        OnExitCurrentState(currentState);

        currentState = newState;

        // OnEnter code for new state (if needed)
        OnEnterNewState(newState);
    }

    private void OnEnterNewState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search:
                searchTimer = 0f;
                break;
                // More OnEnter logic per state if needed
        }
    }

    private void OnExitCurrentState(EnemyState state)
    {
        // Handle any cleanup or reset from the old state
    }

    private void PatrolBehavior()
    {
        // Insert your movement logic. Could reference a waypoint system, etc.
    }

    private void ChaseBehavior()
    {
        float speed = enemyAttributes.moveSpeed * GameMaster.GetEnemyMoveSpeedMultiplier();

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            Time.deltaTime * speed
        );
    }

    private void SearchBehavior()
    {
        // Could be wandering around the last known player position
    }

    private void RetreatBehavior()
    {
        // For example, move to a “safe spot” or away from player
    }

    private bool ReachedSafeSpot()
    {
        // Example of a check
        return false;
    }
}
