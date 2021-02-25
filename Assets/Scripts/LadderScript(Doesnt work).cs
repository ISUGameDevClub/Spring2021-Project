using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask ladder;
    public bool isClimbing;
    public float climbSpeed;
    private void Start()
    {
        rb = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();


    }
    private void OnTriggerEnter2D(Collider2D other)
    {


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
               
            }
        
        if (isClimbing)
        {
            other.transform.Translate(new Vector2(0, Input.GetAxis("Vertical") * Time.deltaTime * climbSpeed));
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

}
