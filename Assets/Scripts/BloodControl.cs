using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BloodControl : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerAttributes playerAttributes;
    private SpriteRenderer _spriteRenderer;
    
    // Update is called once per frame
    private void Start()
    {
        if (playerAttributes == null)
        {
            gameObject.SetActive(false);  // Hide
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (playerAttributes.maxHealth != 0 && playerAttributes.currentHealth / playerAttributes.maxHealth < 0.5f)
        {
            _spriteRenderer.enabled = true;
        } else {
            _spriteRenderer.enabled = false;
        }
    }
}
