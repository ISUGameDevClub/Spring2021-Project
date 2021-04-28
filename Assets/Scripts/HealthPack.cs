using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthHealed;
    public GameObject sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().HealDamage(healthHealed);
            sound.transform.parent = null;
            sound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}


