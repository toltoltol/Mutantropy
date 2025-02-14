using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    // An array with the sprites used for walk animation
    public Sprite[] walkSprites;

    // An array with the sprites used for attack animation
    public Sprite[] attackSprites;


    // Controls how fast to change the sprites 
    public float framesPerSecond;
    public float attackDuration = 0.3f;
    
    // Reference to the renderer of the sprite
    SpriteRenderer animRenderer;

    // Time passed since the start of animatin
    private float timeAtAnimStart;

    private bool isMoving = false;

    private Rigidbody2D rb;
    private PlayerAttributes playerAttributes;

    private bool isAttacking = false;
    private float attackEndTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttributes = GetComponent<PlayerAttributes>();

        if (playerAttributes == null)
        {
            Debug.LogError("PlayerAttributes component is missing from the player object.");
        }
    }

    // Use this for initialization
    void Start()
    {
        // Get a reference to game object renderer and
        // cast it to a Sprite Renderer
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
    }

    // At fixed time intervals...
    void FixedUpdate()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical += 1f; // Move up
        if (Input.GetKey(KeyCode.S)) vertical -= 1f; // Move down
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f; // Move left
        if (Input.GetKey(KeyCode.D)) horizontal += 1f; // Move right

        // Combine into one vector
        Vector2 inputVector = new Vector2(horizontal, vertical).normalized;

        // Move the character using speed from PlayerAttributes and GameMaster multiplier
        rb.velocity = inputVector * playerAttributes.moveSpeed * GameMaster.GetPlayerMoveSpeedMultiplier();

        isMoving = inputVector.magnitude > 0;

        // If moving, ensure animation is running
        if (horizontal != 0)
        {
            animRenderer.flipX = horizontal < 0;
        }

     
    }

    // Before rendering next frame...
    void Update()
    {
        if (playerAttributes == null) return;

        HandleAttackInput();

        if (isAttacking) return; // Prevent movement animation during attack

        if (isMoving)
        {
            PlayWalkAnimation();
        }
        else
        {
            timeAtAnimStart = 0f;
            animRenderer.sprite = walkSprites[0]; // Idle frame
        }
        //if (isMoving)
        //{
        //    // Compute number of seconds since animation started playing
        //    float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

        //    // Compute the index of the next frame
        //    int frameIndex = (int)(timeSinceAnimStart * framesPerSecond) % walkSprites.Length;

        //    animRenderer.sprite = walkSprites[frameIndex];



        //}
        //else {
        //    timeAtAnimStart = 0f;
        //    animRenderer.sprite = walkSprites[0];
        //}
    }

    private void PlayWalkAnimation()
    {
        float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
        int frameIndex = (int)(timeSinceAnimStart * framesPerSecond) % walkSprites.Length;
        animRenderer.sprite = walkSprites[frameIndex];
    }

    private void HandleAttackInput()
    {
        if (Time.time < attackEndTime) return; // Wait until previous attack is done

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;
        attackEndTime = Time.time + attackDuration;

        for (int i = 0; i < attackSprites.Length; i++)
        {
            animRenderer.sprite = attackSprites[i];
            yield return new WaitForSeconds(1f / framesPerSecond);
        }

        isAttacking = false;
    }
}