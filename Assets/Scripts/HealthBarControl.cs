using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthBarControl : MonoBehaviour
{
    public GameObject restartUI;  // Reference to the GameObject tagged "Restart" (assigned in Inspector)
    public DoorFadeEffect fadeEffect;

    private GameObject player; // Reference to the player GameObject
    private PlayerAttributes playerAttributes;
    
    public RectTransform healthBorderRect; // The border (scales with max health)
    public RectTransform healthInfillRect;    // The health bar (inside the border)
    public RectTransform curHealthRect;               // The fill (scales with current health)

    public float sizeMultiplier = 32f;
    public int borderThickness = 2;

    public Sprite deathSprite;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0) player = players[0];

        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Make sure we have a valid reference to player attributes
        if (playerAttributes == null) return;

        UpdateHealthBar(playerAttributes.currentHealth, playerAttributes.maxHealth);

        // Check if player’s health is zero or below
        //TODO decide if this logic should go here or in playerAttributes or gamemaster
        if (playerAttributes.currentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());


            //Destroy(playerAttributes.gameObject);



            //Time.timeScale = 1f;

            //// Display the "Restart" UI
            //if (restartUI != null)
            //{
            //    restartUI.SetActive(true);
            //}
            //Debug.Log("Player died. Loading GameOver scene...");
            //SceneManager.LoadScene("GameOver");

            //return;
        }

    }


        private IEnumerator PlayerDeath()
        {
            if (player == null) yield break;

            // Disable movement script
            PlayerControl playerControl = player.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = false;
            }

            // Stop movement completely
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && deathSprite != null)
            {
                spriteRenderer.sprite = deathSprite;
            }
        
            yield return new WaitForSeconds(2f);
            // Trigger fade out, then load GameOver scene
            if (fadeEffect != null)
            {
                fadeEffect.FadeOut(() =>
                {
                    SceneManager.LoadScene("GameOver");
                });
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }

            yield return null;
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        var infillWidth = maxHealth * sizeMultiplier;
        var borderWidth = infillWidth + borderThickness;
        var curWidth = currentHealth * sizeMultiplier;
        
        
        healthBorderRect.sizeDelta = new Vector2(borderWidth, healthBorderRect.sizeDelta.y);

        // Ensure the inner health bar matches the border size (padding of -10 for aesthetics)
        healthInfillRect.sizeDelta = new Vector2(infillWidth, healthInfillRect.sizeDelta.y);

        // Scale the fill amount based on current health
        curHealthRect.sizeDelta = new Vector2(curWidth, curHealthRect.sizeDelta.y);
    }
}
