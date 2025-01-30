using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;

    private Transform player;
    private EnemyAttributes enemyAttributes;

    // Detection & chase
    public float detectionRadius = 10f;        // Basic radius check before we do any further checks
    public float chaseStopDistance = 15f;      // If player goes beyond this, enemy transitions to Search
    public float searchDuration = 5f;          // How long the enemy stays in Search
    private float searchTimer;

    // Patrol pattern
    public float patrolSpeed = 2f;            // Base movement speed during Patrol
    public float leftRightDuration = 2f;      // How long the enemy does left-right movement
    public float boxEdgeDuration = 1f;        // How long it moves on each edge of the box pattern
    private float patrolTimer;
    private bool doingBoxPattern;
    private int boxStepIndex;                 // Which edge of the box we’re on

    // Hearing footsteps / investigating
    public float hearingRange = 15f;          // How close the player must be for the enemy to hear footsteps
    public float investigateDistance = 2f;    // Small distance to move toward footstep
    private bool isInvestigating;
    private Vector3 investigateTarget;         // Where to move during footstep investigation

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

    //===========================
    // IDLE
    //===========================
    private void UpdateIdle()
    {
        if (Time.frameCount % 300 == 0) // Pretend "timer"
        {
            TransitionToState(EnemyState.Patrol);
        }

        // If player is in detection radius, do a more robust check (like a raycast)
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            // Check line of sight. If visible, chase.
            if (CheckForPlayerLineOfSightRaycast())
            {
                TransitionToState(EnemyState.Chase);
            }
        }
    }

    //===========================
    // PATROL
    //===========================
    private void UpdatePatrol()
    {
        // 1) Check for line of sight to player
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            if (CheckForPlayerLineOfSightRaycast())
            {
                TransitionToState(EnemyState.Chase);
                return;
            }
        }

        // 2) If not investigating footsteps, do normal patrol
        if (!isInvestigating)
        {
            PatrolBehavior();
            // 2a) Also check if we should investigate footsteps
            CheckFootstepsAndInvestigate();
        }
        else
        {
            // 3) Move briefly toward the investigateTarget
            InvestigateFootsteps();
        }
    }

    /// The normal patrol logic: small left-right movement, sometimes a box pattern.
    private void PatrolBehavior()
    {
        // If we aren’t currently doing the box pattern, we do left-right movement for 'leftRightDuration'
        if (!doingBoxPattern)
        {
            patrolTimer += Time.deltaTime;
            // Move left and right based on sin wave or simple direction flip
            float cycle = Mathf.PingPong(Time.time, 1f) * 2f - 1f; // cycle between -1 and 1
            Vector3 offset = new Vector3(cycle * 0.5f, 0, 0);     // half-unit sway left/right
            transform.position += offset * patrolSpeed * Time.deltaTime;

            // After leftRightDuration, switch to box pattern
            if (patrolTimer > leftRightDuration)
            {
                patrolTimer = 0f;
                doingBoxPattern = true;
                boxStepIndex = 0;
            }
        }
        else
        {
            // Move in 4 segments: right, forward, left, back (on XZ plane)
            MoveBoxPattern();
        }
    }

    private void MoveBoxPattern()
    {
        patrolTimer += Time.deltaTime;

        Vector3 direction = Vector3.zero;
        switch (boxStepIndex)
        {
            case 0: direction = Vector3.right; break;  // move right
            case 1: direction = Vector3.forward; break;  // move up/forward
            case 2: direction = Vector3.left; break;  // move left
            case 3: direction = Vector3.back; break;  // move down/back
        }

        transform.position += direction * patrolSpeed * Time.deltaTime;

        if (patrolTimer >= boxEdgeDuration)
        {
            patrolTimer = 0f;
            boxStepIndex++;

            // If we've completed all 4 edges, end the box pattern
            if (boxStepIndex > 3)
            {
                boxStepIndex = 0;
                doingBoxPattern = false;
            }
        }
    }

    //===========================
    // HEARING FOOTSTEPS
    //===========================
    /// Checks if player is within hearingRange and is actually moving.
    /// If so, enemy sets a short "investigation" movement toward the player’s direction.
    private void CheckFootstepsAndInvestigate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Here, we simply check if the player has some velocity (like a CharacterController or rigidbody).
        // Or you could have a separate event that triggers each time a footstep sound is played.
        // For example:
        bool playerIsMoving = PlayerIsMoving();

        if (distanceToPlayer <= hearingRange && playerIsMoving)
        {
            isInvestigating = true;
            // Move a small distance toward player's position
            investigateTarget = transform.position + (player.position - transform.position).normalized * investigateDistance;
        }
    }

    // Actually moves the enemy a short distance toward the investigateTarget, then returns to normal patrol.
    private void InvestigateFootsteps()
    {
        // Move toward investigateTarget
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, investigateTarget, step);

        // Once we are close, revert back to normal patrol pattern
        if (Vector3.Distance(transform.position, investigateTarget) < 0.1f)
        {
            isInvestigating = false;
        }
    }

    // Example method to check if the player is moving
    // TODO: Query your PlayerController’s velocity, or an “OnFootstep” event, etc.
    private bool PlayerIsMoving()
    {
        // If the player uses a CharacterController:
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            return cc.velocity.magnitude > 0.1f;
        }

        // Alternatively, if the player uses a Rigidbody:
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            return rb.velocity.magnitude > 0.1f;
        }

        // Fallback
        return false;
    }

    //===========================
    // CHASE
    //===========================
    private void UpdateChase()
    {
        ChaseBehavior();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > chaseStopDistance)
        {
            // Lost sight, transition to Search
            TransitionToState(EnemyState.Search);
        }
        // TODO: Retreat if enemy health is low
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

    //===========================
    // SEARCH
    //===========================
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
            if (CheckForPlayerLineOfSightRaycast())
            {
                TransitionToState(EnemyState.Chase);
            }
        }
    }

    private void SearchBehavior()
    {
        // Could be wandering around the last known player position
        // Or rotating in place, etc.
    }

    //===========================
    // RETREAT
    //===========================
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

    private void RetreatBehavior()
    {
        // For example, move to a “safe spot” or away from player
    }

    private bool ReachedSafeSpot()
    {
        // Example of a check
        return false;
    }

    //===========================
    // STATE HELPERS
    //===========================
    private void TransitionToState(EnemyState newState)
    {
        OnExitCurrentState(currentState);

        currentState = newState;

        OnEnterNewState(newState);
    }

    private void OnEnterNewState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search:
                searchTimer = 0f;
                break;
            case EnemyState.Patrol:
                // Reset any relevant patrol variables
                patrolTimer = 0f;
                isInvestigating = false;
                doingBoxPattern = false;
                boxStepIndex = 0;
                break;
                // Add more if needed
        }
    }

    private void OnExitCurrentState(EnemyState state)
    {
        // Handle any cleanup or reset from the old state if needed
    }

    //===========================
    // PLAYER DETECTION (RAYCAST)
    //===========================
    /// Example method to detect player via raycast (line-of-sight).
    /// If the ray from enemy to player is not blocked, returns true.
    private bool CheckForPlayerLineOfSightRaycast()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Raycast out to the player
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            // Check if we hit the player first
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
