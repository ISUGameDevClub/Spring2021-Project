﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement pm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            pm.isGrounded = true;
        }
        if (collision.gameObject.tag == "Bounce")
        {
            pm.isGrounded = true;
            pm.Bounce();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            pm.isGrounded = false;
        }
        if (collision.gameObject.tag == "Bounce")
        {
            pm.isGrounded = false;
        }
    }

    
}
