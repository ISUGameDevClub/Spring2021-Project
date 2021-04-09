using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float wallJumpYPower;
    public float wallJumpXPower;
    public float wallJumpTime;
    private PlayerMovement pm;
    private int wallJumpDirection;
    private Coroutine wj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !pm.isGrounded && PlayerData.unlockedWallJump)
        {
            int wallDirection = checkForWall();
            if (wallDirection==1)
            {
                wallJumpDirection = 1;
                pm.FaceLeft();
                if (wj != null)
                    StopCoroutine(wj);
                wj = StartCoroutine(WallJumpEnum());
            }
            else if(wallDirection==0)
            {
                wallJumpDirection = 2;
                pm.FaceRight();
                if (wj != null)
                    StopCoroutine(wj);
                wj = StartCoroutine(WallJumpEnum());
            }
        }
    }

    private void FixedUpdate()
    {
        if(wallJumpDirection == 1)
        {
            transform.Translate(new Vector2(Time.deltaTime * -wallJumpXPower, 0));
        }
        else if (wallJumpDirection == 2)
        {
            transform.Translate(new Vector2(Time.deltaTime * wallJumpXPower, 0));
        }
    }

    private IEnumerator WallJumpEnum()
    {
        pm.DisableMovement(wallJumpTime);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, wallJumpYPower), ForceMode2D.Impulse);
        yield return new WaitForSeconds(wallJumpTime);
        wallJumpDirection = 0;
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
