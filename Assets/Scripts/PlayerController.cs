using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Update()
    {
        if (playerAttributes == null) return;

        // Custom input handling for WASD
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
    }
}
