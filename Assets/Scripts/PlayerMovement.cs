﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 6;
    public float jumpPower;
    [HideInInspector]
    public bool isGrounded;
    public float gravity;
    public float fallSpeed;
    [HideInInspector]
    public bool facingRight;
    public Vector2 lastGroundedPosition;
    public SpriteRenderer mySprite;
    public Animator myAnim;
    public AudioSource jumpSound;

    [HideInInspector]
    public bool scriptedMovement;
    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public bool onLadder;
    [HideInInspector]
    public bool enableGravity;

    [HideInInspector]
    public bool wallSlide;

    private float canMoveTimer;
    private Rigidbody2D rb;
    public bool boat;

    // Start is called before the first frame update
    void Start()
    {
        enableGravity = true;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (!scriptedMovement && canMoveTimer <= 0)
            {
                myAnim.SetBool("Hurt", false);
                canMove = true;
                GetComponent<Health>().invincible = false;
            }
            else if (canMoveTimer > 0)
            {
                canMoveTimer -= Time.deltaTime;
                canMove = false;
            }
            else
                canMove = false;

            if (isGrounded && !boat)
            {
                lastGroundedPosition = transform.position;
            }

            if (canMove)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    FaceRight();
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    FaceLeft();
                }


                if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !onLadder)
                {
                    Jump();
                }
            }

            if (!isGrounded && rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && canMove && !onLadder && enableGravity && !wallSlide)
            {
                myAnim.SetBool("Wall Sliding", false);
                rb.gravityScale = gravity * fallSpeed;
            }
            else if (!onLadder && enableGravity && !wallSlide)
            {
                myAnim.SetBool("Wall Sliding", false);
                rb.gravityScale = gravity;
            }
            else if (wallSlide && !onLadder)
            {
                myAnim.SetBool("Wall Sliding", true);
                rb.gravityScale = 0;
                rb.velocity = new Vector3(rb.velocity.x, -2, 0);
            }
            else
            {
                myAnim.SetBool("Wall Sliding", false);
                rb.gravityScale = 0;
            }

            myAnim.speed = 1;

            if ((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) && canMove)
                myAnim.SetBool("Walking", true);
            else
                myAnim.SetBool("Walking", false);

            if (onLadder)
            {
                if (Input.GetAxisRaw("Vertical") == 0)
                    myAnim.speed = 0;
                myAnim.SetBool("Climbing", true);
            }
            else
                myAnim.SetBool("Climbing", false);

            transform.eulerAngles = Vector3.zero;
        }
    }

    public void FaceRight()
    {
        mySprite.flipX = false;
        facingRight = true;
    }

    public void FaceLeft()
    {
        mySprite.flipX = true;
        facingRight = false;
    }

    public void DisableMovement(float timeDisabled)
    {
        if (canMoveTimer < timeDisabled)
            canMoveTimer = timeDisabled;
    }

    private void FixedUpdate()
    {
        if ((isGrounded && rb.velocity.y < .01f) || canMove)
            rb.velocity = rb.velocity * new Vector2(0, 1);
        if (canMove)
            Movement();
    }

    private void Movement()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0));
    }

    private void Jump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }
}
