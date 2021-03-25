using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    //How fast the player will dash
    public float dashSpeed;

    //The duration of the dash
    public float startDashTime;

    public bool dashAvailable;
    public float cooldownTimer;
    private float dashTime;
    private Rigidbody2D rb;
    private int direction;
    private bool canDash;
    private PlayerMovement pm;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        canDash = true;
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the direction is not defined, define direction
        if (direction == 0)
        {
            if (!dashAvailable)
            {
                if (pm.isGrounded)
                    dashAvailable = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && pm.canMove && direction == 0 && dashAvailable)
            {
                pm.DisableMovement(startDashTime);
                dashAvailable = false;
                StartCoroutine(DashCooldown());
                if(!pm.facingRight)
                {
                    direction = 1;
                }
                else
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
            }
            else
            {
                dashTime -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (direction != 0 && dashTime > 0)
        {
            rb.velocity = Vector2.zero;
            if (direction == 1)
            {
                transform.Translate(Vector2.left * dashSpeed * Time.fixedDeltaTime);
            }
            else if (direction == 2)
            {
                transform.Translate(Vector2.right * dashSpeed * Time.fixedDeltaTime);
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
