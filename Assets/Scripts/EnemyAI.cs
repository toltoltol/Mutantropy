using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;

    private PlayerAttributes playerAttributes;
    private Transform player;
    private EnemyAttributes enemyAttributes;

    public float detectionRadius = 10f;
    public float chaseStopDistance = 15f;
    public float searchDuration = 5f;
    private float searchTimer;

    public float leftRightDuration = 2f;
    public float boxEdgeDuration = 1f;
    private float patrolTimer;
    private bool doingBoxPattern;
    private int boxStepIndex;

    public float hearingRange = 15f;
    public float investigateDistance = 2f;
    private bool isInvestigating;
    private Vector3 investigateTarget;

    // Animation properties
    public Sprite[] animSprites;  // Assign enemy animation frames here
    public float framesPerSecond = 10f;
    private SpriteRenderer animRenderer;
    private float timeAtAnimStart;
    private bool animRunning = false;
    private float movementDir;

    void Start()
    {
        animRenderer = GetComponent<SpriteRenderer>();
        enemyAttributes = GetComponent<EnemyAttributes>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj)
        {
            playerAttributes = playerObj.GetComponent<PlayerAttributes>();
            player = playerObj.transform;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle: UpdateIdle(); break;
            case EnemyState.Patrol: UpdatePatrol(); break;
            case EnemyState.Chase: UpdateChase(); break;
            case EnemyState.Search: UpdateSearch(); break;
            case EnemyState.Retreat: UpdateRetreat(); break;
        }

        if (animRunning) {
            AnimateSprite();
        }
    }

    void UpdateIdle()
    {
        animRunning = false;  // Stop animation
        animRenderer.sprite = animSprites[0]; // Default idle frame
        if (Time.frameCount % 300 == 0) TransitionToState(EnemyState.Patrol);
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            if (CheckForPlayerLineOfSightRaycast()) TransitionToState(EnemyState.Chase);
        }
    }

    void UpdatePatrol()
    {
        animRunning = true;
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            if (CheckForPlayerLineOfSightRaycast())
            {
                TransitionToState(EnemyState.Chase);
                return;
            }
        }

        if (!isInvestigating)
        {
            PatrolBehavior();
            CheckFootstepsAndInvestigate();
        }
        else
        {
            InvestigateFootsteps();
        }
    }

    void PatrolBehavior()
    {
        if (!doingBoxPattern)
        {
            patrolTimer += Time.deltaTime;
            float cycle = Mathf.PingPong(Time.time, 1f) * 2f - 1f;
            Vector3 offset = new Vector3(cycle * 0.5f, 0, 0);
            float speed = enemyAttributes.FinalMoveSpeed * 0.5f;
            transform.position += offset * speed * Time.deltaTime;

            if (patrolTimer > leftRightDuration)
            {
                patrolTimer = 0f;
                doingBoxPattern = true;
                boxStepIndex = 0;
            }
        }
        else MoveBoxPattern();
    }

    void MoveBoxPattern()
    {
        patrolTimer += Time.deltaTime;
        Vector3 direction = Vector3.zero;
        switch (boxStepIndex)
        {
            case 0: direction = Vector3.right; break;
            case 1: direction = Vector3.forward; break;
            case 2: direction = Vector3.left; break;
            case 3: direction = Vector3.back; break;
        }

        float speed = enemyAttributes.FinalMoveSpeed * 0.5f;
        transform.position += direction * speed * Time.deltaTime;

        if (patrolTimer >= boxEdgeDuration)
        {
            patrolTimer = 0f;
            boxStepIndex++;
            if (boxStepIndex > 3)
            {
                boxStepIndex = 0;
                doingBoxPattern = false;
            }
        }
    }

    void CheckFootstepsAndInvestigate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= hearingRange && playerAttributes && playerAttributes.isMoving)
        {
            isInvestigating = true;
            investigateTarget = transform.position +
                                (player.position - transform.position).normalized * investigateDistance;
        }
    }

    void InvestigateFootsteps()
    {
        float speed = enemyAttributes.FinalMoveSpeed * 0.5f;
        transform.position = Vector3.MoveTowards(transform.position, investigateTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, investigateTarget) < 0.1f)
            isInvestigating = false;
    }

    void UpdateChase()
    {
        animRunning = true;

        float speed = enemyAttributes.FinalMoveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) > chaseStopDistance)
            TransitionToState(EnemyState.Search);
    }

    void UpdateSearch()
    {
        searchTimer += Time.deltaTime;
        if (searchTimer >= searchDuration) TransitionToState(EnemyState.Idle);

        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            if (CheckForPlayerLineOfSightRaycast()) TransitionToState(EnemyState.Chase);
        }
    }

    void UpdateRetreat()
    {
        if (ReachedSafeSpot()) TransitionToState(EnemyState.Idle);
    }

    bool ReachedSafeSpot() { return false; }

    void TransitionToState(EnemyState newState)
    {
        OnExitCurrentState(currentState);
        currentState = newState;
        OnEnterNewState(newState);
        if (newState == EnemyState.Idle) animRunning = false;
    }

    void OnEnterNewState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search: searchTimer = 0f; break;
            case EnemyState.Patrol:
                patrolTimer = 0f;
                isInvestigating = false;
                doingBoxPattern = false;
                boxStepIndex = 0;
                break;
        }
    }

    void OnExitCurrentState(EnemyState state) { }

    bool CheckForPlayerLineOfSightRaycast()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
        {
            if (hit.transform.CompareTag("Player")) return true;
        }
        return false;
    }

    void AnimateSprite()
    {
        // Cycle through sprite frames
        int index = (int)((Time.timeSinceLevelLoad - timeAtAnimStart) * framesPerSecond) % animSprites.Length;
        animRenderer.sprite = animSprites[index];

        // Flip sprite based on movement direction
        animRenderer.flipX = movementDir < 0;
    }
}
