using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Stolen from COSC360 SpaceInvadors lab 
public class EnemyAttack : MonoBehaviour {

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
    //Boolean to disable auto shooting for ranged (duck and cover) enemy

    public bool isRangedEnemy = false;

    public bool isMeleeEnemy = false;

    // Variable storing projectile object
    // prefab
    public GameObject projectilePrefab;

    public Transform player;

    private EnemyPeek enemyPeek;
   
    // Probability of auto-shoot (0 if
    // no autoshoot)
    public float autoShootProbability;
   
    // Cooldown time for firing
    public float fireCooldownTime;
   
    // How much time is left until able to fire again 
    //TODO use attack speed from enemyattributes instead
    float fireCooldownTimeLeft = 0;
    
    
    private EnemyAttributes _enemyAttributes;


    private void Start()
    {
        enemyPeek = GetComponent<EnemyPeek>();
        _enemyAttributes = GetComponent<EnemyAttributes>();
        animRenderer = GetComponent<SpriteRenderer>();
    }

    // Per every frame...
    void Update () {
        if (!isMeleeEnemy)
        {
            // If still some time left until can fire again
            // reduce the time by the time since last
            // frame 
            if (fireCooldownTimeLeft > 0)
            {
                fireCooldownTimeLeft -= Time.deltaTime;
            }

            //lol skip everything else if it is ranged enemy
            if (isRangedEnemy)
            {
                if (transform.position == enemyPeek.openPosition1.position || transform.position == enemyPeek.openPosition2.position) { Shoot(); }
                return; // Exit point if ranged enenemy
            }

            // Generate number a random number between 0 and 1
            float randomSample = Random.Range(0f, 1f);
            // If auto-shoot probability is more than zero...
            if (randomSample < autoShootProbability)
            {
                // If that random number is less than the 
                // probability of shooting, then try to shoot
                Shoot();
            }
        }
        StartAttackAnimation();
    }

    // Method for firing a projectile
    public void Shoot() {
        // Shoot only if the fire cooldown period
        // has expired
        if(fireCooldownTimeLeft <= 0) {
            // Create a projectile object from 
            // the shot prefab
            
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
            if (bulletProjectile != null)
            {
                bulletProjectile.Init(_enemyAttributes.attackRange, CalculateShootDirection(), _enemyAttributes.attackProjectileSpeed, _enemyAttributes.attackPower);

            }

            // Set time left until next shot
            // to the cooldown time
            fireCooldownTimeLeft = fireCooldownTime;   
        }

    }

    public Vector2 CalculateShootDirection()
    {
        // Get the direction from the enemy (this.transform) to the player
        Vector2 direction = player.position - transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        return direction;
    }

    public void StartAttackAnimation()
    {
        animRunning = true;
        timeAtAnimStart = Time.timeSinceLevelLoad;
    }

    void FixedUpdate()
    {
        if (animRunning)
        {
            float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
            int frameIndex = (int)(timeSinceAnimStart * framesPerSecond);

            if (frameIndex < animSprites.Length)
            {
                animRenderer.sprite = animSprites[frameIndex];
            }
            else
            {
                animRenderer.sprite = animSprites[0];
                animRunning = false;
                
                
            }
        }
    }
}

