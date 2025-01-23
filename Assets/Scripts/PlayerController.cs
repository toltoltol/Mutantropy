using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Custom input handling for WASD
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical += 1f; // Move up
        if (Input.GetKey(KeyCode.S)) vertical -= 1f; // Move down
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f; // Move left
        if (Input.GetKey(KeyCode.D)) horizontal += 1f; // Move right

        // Combine into one vector
        Vector2 inputVector = new Vector2(horizontal, vertical).normalized;

        // Move the character
        rb.velocity = inputVector * moveSpeed;
    }
}
