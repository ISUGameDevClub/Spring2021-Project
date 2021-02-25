using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parrot : MonoBehaviour
{

    public float speed;
    private Transform target;
    private Vector2 tar;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tar = target.position;
        tar[1] = tar[1] + 2;
        if (FindObjectOfType<PlayerMovement>().facingRight)
        {

            tar[0] = tar[0] - 1;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }
        else
        {
            tar[0] = tar[0] + 1;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }
    }
}
