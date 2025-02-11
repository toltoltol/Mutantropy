using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO this code is absolutely traash I would like to redo it before final submission
//Instead use camera view to detect enemies?? or some way that will detect gates when disabled
// Probably gonna have bugs where gates get disabled when they shouldnt and arent re enabled
namespace transition
{
    public class DoorControl : MonoBehaviour
    {
        public Camera _cam;
        public BoxCollider2D _boxCollider;

        public List<GameObject> enemiesInRange = new List<GameObject>();
        public List<GameObject> closedGatesInRange = new List<GameObject>();

        void Start()
        {
            _cam = Camera.main;
            _boxCollider = GetComponent<BoxCollider2D>();
            ResizeCollider();
        }

        void LateUpdate()
        {
            // No need for anything in LateUpdate for now, as it's not part of the current logic
        }

        void ResizeCollider()
        {
            if (_cam == null || _boxCollider == null) return;

            float camHeight = 2f * _cam.orthographicSize;
            float camWidth = camHeight * _cam.aspect;

            _boxCollider.size = new Vector2(camWidth, camHeight);
            _boxCollider.offset = Vector2.zero;
        }

        // Trigger when an object enters the collider
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Add the enemy to the list if not already added
                if (!enemiesInRange.Contains(other.gameObject))
                {
                    enemiesInRange.Add(other.gameObject);
                }
            }

            if (other.gameObject.name == "ClosedGate")
            {
                // Add the ClosedGate object to the list if not already added
                if (!closedGatesInRange.Contains(other.gameObject))
                {
                    closedGatesInRange.Add(other.gameObject);
                }
            }
        }
        
        // Trigger when an object exits the collider
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Remove the enemy from the list when it leaves the collider
                if (enemiesInRange.Contains(other.gameObject))
                {
                    enemiesInRange.Remove(other.gameObject);
                }
            }

            if (other.gameObject.name == "ClosedGate")
            {
                // Remove the ClosedGate object from the list when it leaves the collider
                if (closedGatesInRange.Contains(other.gameObject))
                {
                    closedGatesInRange.Remove(other.gameObject);
                }
            }
        }

        // Check the number of enemies and disable gates if no enemies are in range
        void Update()
        {
            if (enemiesInRange.Count == 0)
            {
                foreach (var gate in closedGatesInRange)
                {
                    gate.SetActive(false);  // Disable the gate if no enemies are in range
                }
            }
        }
    }
}
