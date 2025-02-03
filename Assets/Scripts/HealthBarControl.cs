using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarControl : MonoBehaviour
{
    public Sprite[] animSprites; // Array of sprites representing health states
    public GameObject player; // Reference to the player GameObject
    public GameObject restartUI;  // Reference to the GameObject tagged "Restart" (assigned in Inspector)

    private SpriteRenderer spriteRenderer;
    private PlayerAttributes playerAttributes;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
        }

        // Ensure the restart UI is hidden at game start
        if (restartUI != null)
        {
            restartUI.SetActive(false);
        }
    }

    void Update()
    {
        // Make sure we have a valid reference to player attributes
        if (playerAttributes == null) return;


        // Check if player’s health is zero or below
        if (playerAttributes.currentHealth <= 0)
        {
            Destroy(gameObject);

            // Freeze the game
            Time.timeScale = 0f;

            // Display the "Restart" UI
            if (restartUI != null)
            {
                restartUI.SetActive(true);
            }

            return;
        }

        if (animSprites.Length > 0)
        {
            int index = Mathf.Clamp(Mathf.FloorToInt(playerAttributes.currentHealth) - 1, 0, animSprites.Length - 1);
            spriteRenderer.sprite = animSprites[index];
        }
    }
}
