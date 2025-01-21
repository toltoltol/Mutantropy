using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    // Set this angle to 45 if your isometric tiles are rotated by 45 degrees
    public float rotationAngle = -45f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get standard input
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D keys
        float vertical = Input.GetAxisRaw("Vertical");     // W/S keys

        // Combine into one vector
        Vector2 inputVector = new Vector2(horizontal, vertical).normalized;

        // Move the character
        rb.velocity = inputVector * moveSpeed;
    }

}
