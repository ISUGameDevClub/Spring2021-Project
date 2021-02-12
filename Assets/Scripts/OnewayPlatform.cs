using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    private GameObject player;
    private Collider2D col;
    private PlatformEffector2D effector;
    private float waitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        effector = GetComponent<PlatformEffector2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                waitTime = 0.5f;
            }
           
        }

        if (player.transform.position.y > transform.position.y + 1.45f && waitTime <= 0)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
