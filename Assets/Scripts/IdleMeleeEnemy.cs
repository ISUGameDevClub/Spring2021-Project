using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMeleeEnemy : MonoBehaviour
{
    public SpriteRenderer sr;

    private GameObject player;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            myAnim.SetTrigger("Attack");
        }
    }
}
