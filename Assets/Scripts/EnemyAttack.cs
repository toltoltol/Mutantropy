using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stolen from COSC360 SpaceInvadors lab 
public class EnemyAttack : MonoBehaviour {
    
    //Boolean to disable auto shooting for ranged (duck and cover) enemy

    public bool isRangedEnemy = false;

    // Variable storing projectile object
    // prefab
    public GameObject projectilePrefab;

    public Transform player;
   
    // Probability of auto-shoot (0 if
    // no autoshoot)
    public float autoShootProbability;
   
    // Cooldown time for firing
    public float fireCooldownTime;
   
    // How much time is left until able to fire again 
    float fireCooldownTimeLeft = 0;
      
    // Per every frame...
    void Update () { 
        // If still some time left until can fire again
        // reduce the time by the time since last
        // frame 
        if(fireCooldownTimeLeft > 0) {
            fireCooldownTimeLeft -= Time.deltaTime;
        }
        
        //lol skip everything else if it is ranged enemy
        if (isRangedEnemy) {return;}
      
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
                bulletProjectile.SetDirection(CalculateShootDirection());
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
    
}