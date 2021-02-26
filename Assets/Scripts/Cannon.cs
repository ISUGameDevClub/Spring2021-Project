using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float cannonSpeed;
    public Vector2 cannonAngle;
    private Rigidbody2D rb;
    private bool isCannon;
    private PlayerMovement pm;
    private bool shotPlayer;
    private bool loaded;


    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        rb = pm.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       if(isCannon && Input.GetKeyDown(KeyCode.E) && !shotPlayer && !loaded)
       {
            StartCoroutine(FireCannon());
       }

       if(shotPlayer)
       {
            if(rb.velocity.magnitude <=0)
            {
                shotPlayer = false;
                pm.scriptedMovement = false;
            }
       }
    }

    private IEnumerator FireCannon()
    {
        loaded = true;
        pm.scriptedMovement = true;
        yield return new WaitForSeconds(.2f);
        rb.gameObject.transform.position = transform.position;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(cannonAngle.normalized * cannonSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.2f);
        loaded = false;
        shotPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player")
       {
            isCannon = true;
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCannon = false;
        }
    }
}
