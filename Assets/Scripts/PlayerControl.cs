using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Controls how fast to change the sprites when
    // animation is running
    public float framesPerSecond;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    // Time passed since the start of animatin
    private float timeAtAnimStart;

    // Indicates whether animation is running or not
    private bool animRunning = false;

    // Direction of the movement
    private float movementDir;

    private Rigidbody2D rb;
    private PlayerAttributes playerAttributes;

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
        // cast it to a Sprite Rendere
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
    }

    // At fixed time intervals...
    void FixedUpdate()
    {
        if (!animRunning)
        {
            // The animation is triggered by user input
            float userInput = Input.GetAxis("Horizontal");
            if (userInput != 0f)
            {
                // User pressed the move left or right button

                // Animation will start playing
                animRunning = true;

                // Record time at animation start
                timeAtAnimStart = Time.timeSinceLevelLoad;

                // Get the direction of the movement from the sign
                // of the axis input (-ve is left, +ve is right)
                movementDir = Mathf.Sign(userInput);
            }
        }
    }

    // Before rendering next frame...
    void Update()
    {
        if (playerAttributes == null) return;

        if (animRunning)
        {
            // Animation is running, so we need to
            // figure out what frame to use at this point
            // in time

            // Compute number of seconds since animation started playing
            float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

            // Compute the index of the next frame
            int frameIndex = (int)(timeSinceAnimStart * framesPerSecond);


            // OVERWRITE MOVEMENT...
            // Vector3 localScale = transform.localScale;
            // localScale.x = Mathf.Abs(transform.localScale.x) * movementDir;
            // transform.localScale = localScale;

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

            if (frameIndex < animSprites.Length)
            {
                // Let the renderer know which sprite to
                // use next
                animRenderer.sprite = animSprites[frameIndex];

                // // Move sprite
                // Vector3 shift = Vector3.right * movementDir * speed * Time.deltaTime;
                // transform.Translate(shift);
            }
            else
            {
                // Animation finished, set the render
                // with the first sprite and stop the
                // animation
                animRenderer.sprite = animSprites[0];
                animRunning = false;
            }
        }
    }
}
