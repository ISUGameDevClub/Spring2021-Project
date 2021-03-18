using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotFollow : MonoBehaviour
{
    public float speed;
    private PlayerMovement pm;
    private Transform target;
    private Vector2 tar;


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        target = pm.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tar = target.position;
        tar[1] = tar[1] + 1.25f;
        if (pm.facingRight)
        {

            tar[0] = tar[0] - 1.5f;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }
        else
        {
            tar[0] = tar[0] + 1.5f;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }
    }
}
