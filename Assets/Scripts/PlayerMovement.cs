using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public float jumpPower = 8;
    public bool isGrounded;
    public float gravity;
    public float fallSpeed;

    public bool facingRight;
    public Vector2 lastGroundedPosition;


    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            lastGroundedPosition = transform.position;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            facingRight = true;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            facingRight = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(!isGrounded && rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = gravity * fallSpeed;
        }
        else
        {
            rb.gravityScale = gravity;
        }

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0));
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }
}
