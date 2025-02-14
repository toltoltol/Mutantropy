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

    private Transform player;

    private EnemyPeek enemyPeek;
   
    // Probability of auto-shoot (0 if
    // no autoshoot)
    public float autoShootProbability;
   
    // Cooldown time for firing
    public float fireCooldownTime;
    
    //Melee Range
    public float meleeRange = 2f;
    
    //Melee shake variables
    public float meleeShakeDuration = 0.4f;

    public float meleeShakeMagnitude = 0.1f;
   
    // How much time is left until able to fire again 
    //TODO use attack speed from enemyattributes instead
    float fireCooldownTimeLeft = 0;
    
    
    private EnemyAttributes _enemyAttributes;


    private void Start()
    {
//        Debug.Log("Sprite array length at start: " + animSprites.Length);
        animRenderer = GetComponent<SpriteRenderer>();

        enemyPeek = GetComponent<EnemyPeek>();
        _enemyAttributes = GetComponent<EnemyAttributes>();
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0) player = players[0].transform;
    }

    // Per every frame...
    void Update () {
        
        // If still some time left until can fire again
        // reduce the time by the time since last
        // frame 
        if (fireCooldownTimeLeft > 0)
        {
            fireCooldownTimeLeft -= Time.deltaTime;
        }

        //lol skip everything else if it is ranged enemy (these dont exist anymore)
        if (isRangedEnemy)
        {
            if (transform.position == enemyPeek.openPosition1.position || transform.position == enemyPeek.openPosition2.position) { Shoot(); }
            return; // Exit point if ranged enenemy
        } else if (isMeleeEnemy)
        {
            if (Vector2.Distance(player.position, transform.position) <= meleeRange)
            {
                MeleeShoot();
                StartAttackAnimation();
            }

        }
        else  //Is normal enemy
        {
            // Generate number a random number between 0 and 1
            float randomSample = Random.Range(0f, 1f);
            // If auto-shoot probability is more than zero...
            if (randomSample < autoShootProbability)
            {
                // If that random number is less than the 
                // probability of shooting, then try to shoot
                Shoot();
                StartAttackAnimation();
            }
        }
    }

    // Method for firing a projectile
    public void Shoot() {
        // Shoot only if the fire cooldown period
        // has expired
        
        if(fireCooldownTimeLeft <= 0) {
            // Create a projectile object from 
            // the shot prefab
            if (gameObject.CompareTag("Boss"))
            {
                float attackChance = 0.5f;

                if (Random.value < attackChance)
                {
                    BossRingAttack();
                } else {
                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
                    if (bulletProjectile != null)
                    {
                        bulletProjectile.Init(_enemyAttributes.attackRange, CalculateShootDirection(), _enemyAttributes.attackProjectileSpeed, _enemyAttributes.attackPower);

                    }
                }
            }
            else
            {
                //Normal enemy attack
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
                if (bulletProjectile != null)
                {
                    bulletProjectile.Init(_enemyAttributes.attackRange, CalculateShootDirection(), _enemyAttributes.attackProjectileSpeed, _enemyAttributes.attackPower);

                }
            }
            // Set time left until next shot
            // to the cooldown time
            fireCooldownTimeLeft = fireCooldownTime;   
        }

    }

    private void BossRingAttack()
    {
        int bulletCount = 12; // Number of bullets in the ring
        float angleStep = 360f / bulletCount; // Angle between bullets
 

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();

            if (bulletProjectile != null)
            {
                bulletProjectile.Init(_enemyAttributes.attackRange, direction, _enemyAttributes.attackProjectileSpeed, _enemyAttributes.attackPower);
            }
        }
    }

    private void MeleeShoot()
    {
        // Melee only if the fire cooldown period
        // has expired
        if(fireCooldownTimeLeft <= 0) {
            // Create a projectile object from 
            // the shot prefab
            
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.SetParent(transform);  //very important means melee attack follows enemy
            BulletProjectile bulletProjectile = projectile.GetComponent<BulletProjectile>();
            if (bulletProjectile != null)
            {
                bulletProjectile.Init(1, CalculateShootDirection(), 0f, _enemyAttributes.attackPower);

            }

            // Set time left until next shot
            // to the cooldown time
            fireCooldownTimeLeft = fireCooldownTime;
            
            //Lil shake animation because you cannot tell they melee without animation
            StartCoroutine(ShakeTransform(transform, meleeShakeDuration, meleeShakeMagnitude));
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
        if (animSprites == null || animSprites.Length == 0)
        {
            Debug.LogError("animSprites is empty! Check if it's assigned in the Inspector.");
            return;  // Prevents the error
        }
        if (animRunning)
        {
            float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
            int frameIndex = (int)(timeSinceAnimStart * framesPerSecond);
//            Debug.Log("Frame index: " + frameIndex);
  //          Debug.Log("Sprite array length: " + animSprites.Length);
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
    
    public IEnumerator ShakeTransform(Transform target, float duration, float magnitude)
    {
        Vector3 originalPosition = target.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            target.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;

            yield return null; // Wait for next frame
        }

        // Ensure object returns to original position
        target.position = originalPosition;
    }
}

