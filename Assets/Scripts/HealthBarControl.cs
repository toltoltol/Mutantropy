using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarControl : MonoBehaviour
{
    public Sprite[] animSprites; // Array of sprites representing health states
    public GameObject player; // Reference to the player GameObject

    private SpriteRenderer spriteRenderer;
    private PlayerAttributes playerAttributes;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
        }
    }

    void Update()
    {
        if (playerAttributes != null && animSprites.Length > 0)
        {
            int index = Mathf.Clamp(Mathf.FloorToInt(playerAttributes.currentHealth) - 1, 0, animSprites.Length - 1);
            spriteRenderer.sprite = animSprites[index];
        }
    }
}