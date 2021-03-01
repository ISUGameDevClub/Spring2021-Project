using System.Collections;
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

    [HideInInspector]
    public bool scriptedMovement;
    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public bool onLadder;

    private float canMoveTimer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptedMovement && canMoveTimer <= 0)
            canMove = true;
        else if (canMoveTimer > 0)
        {
            canMoveTimer -= Time.deltaTime;
            canMove = false;
        }
        else
            canMove = false;

        if (isGrounded)
        {
            lastGroundedPosition = transform.position;
        }

        if (canMove)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                mySprite.flipX = false;
                facingRight = true;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                mySprite.flipX = true;
                facingRight = false;
            }


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        if(!isGrounded && rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && canMove && !onLadder)
        {
            rb.gravityScale = gravity * fallSpeed;
        }
        else if (!onLadder)
        {
            rb.gravityScale = gravity;
        }
        else
        {
            rb.gravityScale = 0;
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
            myAnim.SetBool("Walking", true);
        else
            myAnim.SetBool("Walking", false);

    }

    public void DisableMovement(float timeDisabled)
    {
        if (canMoveTimer < timeDisabled)
            canMoveTimer = timeDisabled;
    }

    private void FixedUpdate()
    {
        if(canMove)
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
