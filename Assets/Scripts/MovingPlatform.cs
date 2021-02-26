using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public float platSpeed;

    private bool Returning;
    private PlayerMovement pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(pm.transform.parent == gameObject && !pm.isGrounded)
            pm.transform.SetParent(null);
    }

    void FixedUpdate()
    {
        if (Returning)
        {
            transform.position = Vector2.MoveTowards(transform.position, position1.transform.position, platSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, position2.transform.position, platSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(position2.transform.position, transform.position) < .2f)
        {
            Returning = true;
        }
        else if (Vector2.Distance(position1.transform.position, transform.position) < .2f)
        {
            Returning = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && pm.isGrounded)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}