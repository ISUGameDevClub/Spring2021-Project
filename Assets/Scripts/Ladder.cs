using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private PlayerMovement pm;
    private Rigidbody2D rb;

    public bool isClimbing;
    public bool touchingLadder;
    public float climbSpeed;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        rb = pm.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(touchingLadder && !isClimbing)
        {
            if(Input.GetAxisRaw("Vertical") != 0)
            {
                isClimbing = true;
                pm.onLadder = true;
                rb.velocity = Vector2.zero;
            }
        }

        if (isClimbing)
        {
            pm.gameObject.transform.Translate(new Vector2(0, Input.GetAxis("Vertical") * Time.deltaTime * climbSpeed));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingLadder = false;
            isClimbing = false;
            pm.onLadder = false;
        }
    }
}
