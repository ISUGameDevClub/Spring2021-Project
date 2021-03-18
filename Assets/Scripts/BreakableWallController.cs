using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosive")
            Destroy(gameObject);
    }
}
