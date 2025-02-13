using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace UI
{
    // Credit: COSC360 User Interface Practical
    public class HUDManager : MonoBehaviour
    {
        // // References to UI elements on the canvas
        // public Text hudScore = null;
        // public Slider hudHealth = null;
        //
        // // References to objects that provide
        // // information about score, health and
        // // the game over condition
        // Score scoreInfoProvider;
        // PlayerHealth healthInfoProvider;
        // Remover gameoverInfoProvider;

        
        // New health retriever
        PlayerAttributes _playerAttributes;

        // Health value currently displayed
        public TextMeshProUGUI currentHealthText;
        
        //Other stats text elements
        public TextMeshProUGUI dmgText;
        public TextMeshProUGUI atkSpeedText;
        public TextMeshProUGUI atkRangeText;
        public TextMeshProUGUI atkProjectileSpeedText;
        public TextMeshProUGUI speedText;




        // Use this for initialization
        void Start()
        {
            // // Initilaise references to the game objects
            // // that provide informaiton about the score,
            // // health and game over condition
            // scoreInfoProvider = FindObjectOfType<Score>();
            _playerAttributes = FindObjectOfType<PlayerAttributes>();
            // GameObject[] objArray = GameObject.FindGameObjectsWithTag("gameoverTrigger");
            // gameoverInfoProvider = objArray[0].GetComponent<Remover>();
            //
            // // Set the starting health value for display
            // health = healthInfoProvider.health;

           
        }

        // Update is called once per frame
        void Update()
        {
            currentHealthText.text = _playerAttributes.currentHealth.ToString("F1");
            dmgText.text = _playerAttributes.attackPower.ToString("F2");
            atkSpeedText.text = _playerAttributes.attackSpeed.ToString("F2");
            atkRangeText.text = _playerAttributes.attackRange.ToString("F2");
            atkProjectileSpeedText.text = _playerAttributes.attackProjectileSpeed.ToString("F2");
            speedText.text = _playerAttributes.moveSpeed.ToString("F2");
            // // Display the score
            // hudScore.text = "Score: " + scoreInfoProvider.score;
            //
            // // Display health - but rather than doing it in one go, change the value
            // // gradually (over certain period of time)   
            // health = Mathf.MoveTowards(health, healthInfoProvider.health, 20 * Time.deltaTime);
            // hudHealth.value = health;

            // if (gameoverInfoProvider.gameover)
            // {
            //     // If gameover state detected, show the pause menu in gameover mode   
            //     pauseMenu.ShowGameOver();
            // }
            //if (Input.GetKey(KeyCode.Escape))
            //{
            //    // If user presses ESC, show the pause menu in pause mode
            //    pauseMenu.ShowPause();
            //}
        }
    }
}