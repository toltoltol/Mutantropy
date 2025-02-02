using UnityEngine;

public class Mutagen : MonoBehaviour
{

    public GameObject Player; // Reference to the player object
    public int buffAmount = 1; // Example buff value
    public int debuffAmount = -1; // Example debuff value

    SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = Player.GetComponent<SpriteRenderer>();
        if (playerSprite == null)
        {
            Debug.LogError("SpriteRenderer not found on player!");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Apply random buff or debuff
            int effect = Random.value > 0.5f ? buffAmount : debuffAmount;
            Debug.Log("Mutagen applied! Effect: " + effect);

            // Example: Update player's speed
            
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.moveSpeed += effect;
                Debug.Log("New move speed: " + player.moveSpeed);
            }

            // Make player turn green on pickup
            playerSprite.color = Color.green;

            // Destroy the mutagen after pickup
            Destroy(gameObject);
        }
    }
}