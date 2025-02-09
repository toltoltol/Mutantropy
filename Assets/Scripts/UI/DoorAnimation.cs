using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimtion : MonoBehaviour
{

    private Animator anim;

    public string animName = "Door Animation";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim != null)
        {
            anim.Play(animName);

        }
        else {
            Debug.LogError("Animator component not found.");
        }
    }
}