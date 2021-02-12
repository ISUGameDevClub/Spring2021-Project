using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    private GameObject player;
    private Collider2D col;
    private float waitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            waitTime = 0.5f;         
        }

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (player.transform.position.y > transform.position.y + 1.35f && waitTime <= 0)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
}
