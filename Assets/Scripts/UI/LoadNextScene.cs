using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Facility", LoadSceneMode.Single);
    }
}
