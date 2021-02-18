using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSceipt : MonoBehaviour
{

    public int climbSpeed;
    public bool TouchingLadder;
    Rigidbody2D rb;
    private void Start()
    {
        rb = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();


    }
    private void Update()
    {
        if (TouchingLadder)
        {
            rb.gravityScale = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.gameObject.transform.Translate(Vector2.up * Time.deltaTime* climbSpeed);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.gameObject.transform.Translate(Vector2.down * Time.deltaTime * climbSpeed);

            }
        }
        else
        {
            rb.gravityScale = 1;
        }
    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TouchingLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TouchingLadder = false;
        }
    }

}
