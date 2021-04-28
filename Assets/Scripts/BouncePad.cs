using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bouncePower;

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
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("Bounce");
            Bounce(collision.GetComponent<Rigidbody2D>());
        }
    }

    public void Bounce(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, bouncePower), ForceMode2D.Impulse);
    }
}
