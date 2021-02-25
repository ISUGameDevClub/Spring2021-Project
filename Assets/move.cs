using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public Vector3 dest;
    public Vector3 start;
    public float time;
    public float platSpeed;
    

    private bool Returning;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if(Returning)
        {
            transform.position = Vector2.Lerp(transform.position, start, platSpeed * Time.deltaTime);
        }
        if (!Returning)
        {
            transform.position = Vector2.Lerp(transform.position, dest, platSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(dest,transform.position) < .1f)
        {
            Returning = true;
        }
        if (Vector2.Distance(start, transform.position) < .1f)
        {
            Returning = false;
        }

    }
    private void returnToStart()
    {
        
    }

}
