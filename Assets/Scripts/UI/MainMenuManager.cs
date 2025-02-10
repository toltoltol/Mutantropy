using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void StartGame () {
        // Load the cutscene
        SceneManager.LoadScene("Cutscene");
    }

    public void StartLevel() {
        SceneManager.LoadScene("Facility");
    }

    public void QuitGame() {
        // Quit the application
        Application.Quit();
    }

    public void Credits() {
        Debug.Log("Credits button pressed");
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
