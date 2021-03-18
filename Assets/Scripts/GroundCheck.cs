using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement pm;

    private int grounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            grounds++;
            if (grounds > 0)
            {
                pm.isGrounded = true;
                pm.myAnim.SetBool("Grounded", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            grounds--;
            if (grounds <= 0)
            {
                pm.isGrounded = false;
                pm.myAnim.SetBool("Grounded", false);
            }
        }
    }
}
