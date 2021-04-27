using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject Sparkle;
    public int value;

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
            GameObject s = Instantiate(Sparkle, transform.position, new Quaternion(0, 0, 0, 0));
            collision.GetComponent<AmmoSystem>().PickupAmmo(value);
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject s = Instantiate(Sparkle, transform.position, new Quaternion(0, 0, 0, 0));
            collision.gameObject.GetComponent<AmmoSystem>().PickupAmmo(value);
            Destroy(gameObject);
        }
    }
}
