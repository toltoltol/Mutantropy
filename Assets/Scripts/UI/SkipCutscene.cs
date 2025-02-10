using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    public GameObject cutsceneCanvas;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Skip();
        }

    }

    void Skip() {
        if (cutsceneCanvas != null) {
            //cutsceneCanvas.SetActive(false);
            SceneManager.LoadScene("Facility");
        }
    }
}
