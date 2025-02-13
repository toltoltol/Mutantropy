using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace transition
{
    public class EnemyDisabliser : MonoBehaviour
    {
        public List<GameObject> enemiesInRange = new List<GameObject>();

        private void Start()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                UpdateMonoBehavoirs(false, enemy.GetComponents<MonoBehaviour>());
            }
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Add the enemy to the list if not already added
                if (!enemiesInRange.Contains(other.gameObject))
                {
                    StartCoroutine(DelayedReenableEnemies(other));
                    enemiesInRange.Add(other.gameObject);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Remove the enemy from the list when it leaves the collider
                if (enemiesInRange.Contains(other.gameObject))
                {
                    UpdateMonoBehavoirs(false,other.GetComponents<MonoBehaviour>());
                    enemiesInRange.Remove(other.gameObject);
                }
            }
        }

        private void UpdateMonoBehavoirs(bool enableScripts, MonoBehaviour[] scripts)
        {
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = enableScripts;
            }
        }

        private IEnumerator DelayedReenableEnemies(Collider2D other)
        {
            // Wait for 0.2 seconds
            yield return new WaitForSeconds(0.4f);

            // Call UpdateMonoBehaviors after delay
            UpdateMonoBehavoirs(true, other.GetComponents<MonoBehaviour>());
        }
        
    }
}