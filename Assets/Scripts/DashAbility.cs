using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    //How fast the player will dash
    public float dashSpeed;

    //The duration of the dash
    public float startDashTime;

    public float cooldownTimer;
    public float moveInput;
    private float dashTime;
    private Rigidbody2D rb;
    private int direction;
    private bool canDash;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        //If the direction is not defined, define direction
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(DashCooldown());
                if(moveInput < 0)
                {
                    direction = 1;
                }
                else if (moveInput > 0)
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if(direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }

    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(cooldownTimer);
        canDash = true;
    }
}
