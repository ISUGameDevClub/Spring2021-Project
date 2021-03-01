using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 playerPos;
    private GameObject player;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
  
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, -10);
        }
        else if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, -10);
        }

        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, -10);
        }
        else if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMin, transform.position.y, -10);
        }
    }
}
