using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float cannonSpeed;
    public Vector2 cannonAngle;
    private Rigidbody2D rb;
    private bool isCannon;


    void Start()
    {
        rb = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       if(isCannon && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FireCannon());
        }
    }

    private IEnumerator FireCannon()
    {
        yield return new WaitForSeconds(.2f);
        rb.gameObject.transform.position = new Vector2(0, 1);
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(cannonAngle.normalized * cannonSpeed, ForceMode2D.Impulse);
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
