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
        
        //Other stats text elements
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI atkSpeedText;
        public TextMeshProUGUI dmgText;


        // Reference to UI panel that is our pause menu
        public GameObject pauseMenuPanel;

        // Reference to panel's script object 
        PauseMenuManager pauseMenu;

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

            // Initialise the reference to the script object, which is a
            // component of the pause menu panel game object
            pauseMenu = pauseMenuPanel.GetComponent<PauseMenuManager>();
            pauseMenu.Hide();
        }

        // Update is called once per frame
        void Update()
        {
            speedText.text = _playerAttributes.moveSpeed.ToString("F1");
            atkSpeedText.text = _playerAttributes.attackSpeed.ToString("F1");
            dmgText.text = _playerAttributes.attackPower.ToString("F1");
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
            if (Input.GetKey(KeyCode.Escape))
            {
                // If user presses ESC, show the pause menu in pause mode
                pauseMenu.ShowPause();
            }
        }
    }
}