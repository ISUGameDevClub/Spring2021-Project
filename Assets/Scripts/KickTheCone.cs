using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTheCone : MonoBehaviour
{
    private Animator myAnim;
    private bool kicked;
    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !kicked)
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
                transform.eulerAngles = new Vector2(0, 180);
            myAnim.SetTrigger("Kick");
            kicked = true;
        }
    }
}
