using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   
    public void StartGame () {
        // Load the "Level" scene
        SceneManager.LoadScene("Room1");   
    }
   
    public void QuitGame() {
        // Quit the application
        Application.Quit();
    }
}