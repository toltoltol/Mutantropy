using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;
    CamController camControl;
    private DoorFadeEffect fadeManager;

    void Start()
    {
        camControl = Camera.main.GetComponent<CamController>();
        fadeManager = GameObject.Find("FadeCanvas").GetComponent<DoorFadeEffect>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            fadeManager.FadeOut(() => {  // Fade out before changing camera postitions
                camControl.minPos += newCamPos;
                camControl.maxPos += newCamPos;
                other.transform.position += newPlayerPos;
                fadeManager.FadeIn(); // Then fade in after arriving in the next room
            });
        }
    }
}
