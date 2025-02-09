using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Controls how fast to change the sprites 
    public float framesPerSecond;

    // Reference to the renderer of the sprite
    SpriteRenderer animRenderer;

    // Time passed since the start of animatin
    private float timeAtAnimStart;

    private bool isMoving = false;

    private Rigidbody2D rb;
    private PlayerAttributes playerAttributes;
    // Direction of the movement
    //private float movementDir;
    //private float lastMovementDir = 0;

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

        if (isMoving)
        {
            // Compute number of seconds since animation started playing
            float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

            // Compute the index of the next frame
            int frameIndex = (int)(timeSinceAnimStart * framesPerSecond) % animSprites.Length;

            animRenderer.sprite = animSprites[frameIndex];



        }
        else {
            timeAtAnimStart = 0f;
            animRenderer.sprite = animSprites[0];
        }
    }
}