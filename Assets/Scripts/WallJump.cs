using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool touchWall;
    public float wallJumpPower;
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !pm.isGrounded)
        {
            int wallDirection = checkForWall();
            if (wallDirection==1)
            {
                rb.velocity = new Vector2(rb.velocity.x, wallJumpPower);
                rb.AddForce(new Vector2(0, wallJumpPower), ForceMode2D.Impulse);
                transform.Translate(Vector2.left * 5 * Time.fixedDeltaTime);
            }

            else if(wallDirection==0)
            {
                rb.velocity = new Vector2(rb.velocity.x, wallJumpPower);
                rb.AddForce(new Vector2(0, wallJumpPower), ForceMode2D.Impulse);
                transform.Translate(Vector2.right * 5 * Time.fixedDeltaTime);
            }
        }
    }

    private int checkForWall()
    {
        int layermask = (LayerMask.GetMask("Ground"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1, layermask);

        if(hit.collider != null)
        {
            return 1;
        }

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 1, layermask);

        if (hit2.collider != null)
        {
            return 0;
        }

        return -1;
    }

}
