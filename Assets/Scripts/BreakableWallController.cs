using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallController : MonoBehaviour
{
    public GameObject sound;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosive")
        {
            sound.GetComponent<AudioSource>().Play();
            sound.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
