using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB : MonoBehaviour
{
    GameObject player;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x - 20)
        {
            if (direction == -1)
            {
                int layermask = (LayerMask.GetMask("Ground"));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right, 1, layermask);

                if (hit.collider != null)
                {
                    direction = 1;
                }

                int layermask2 = (LayerMask.GetMask("GB"));

                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right, 1, layermask);

                if (hit.collider != null)
                {
                    direction = 1;
                }
            }
            else if (direction == 1)
            {
                int layermask = (LayerMask.GetMask("Ground"));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1, layermask);

                if (hit.collider != null)
                {
                    direction = -1;
                }

                int layermask2 = (LayerMask.GetMask("GB"));

                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 1, layermask);

                if (hit.collider != null)
                {
                    direction = -1;
                }
            }

            transform.Translate(direction * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            GetComponent<Health>().TakeDamage(3, 0, 0, 0);
        }
    }
}
