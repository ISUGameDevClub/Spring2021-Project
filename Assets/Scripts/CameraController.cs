using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 playerPos;

    public GameObject player;
    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    void Start()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = playerPos;
    }
    void LateUpdate()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if (playerPos.y >= yMax)
        { 
            if (playerPos.x >= xMax)
                transform.position = new Vector3(xMax, yMax, -10);
            else if (playerPos.x <= xMin)
                transform.position = new Vector3(xMin, yMax, -10);
            else 
                transform.position = new Vector3(playerPos.x, yMax, -10);
        }
        else if (playerPos.y <= yMin)
        {
            if (playerPos.x >= xMax)
                transform.position = new Vector3(xMax, yMin, -10);
            else if (playerPos.x <= xMin)
                transform.position = new Vector3(xMin, yMin, -10);
            else
                transform.position = new Vector3(playerPos.x, yMin, -10);
        }
        else if (playerPos.x >= xMax)
        {
            if (playerPos.y >= yMax)
                transform.position = new Vector3(xMax, yMax, -10);
            else if (playerPos.y <= yMin)
                transform.position = new Vector3(xMax, yMin, -10);
            else
                transform.position = new Vector3(xMax, playerPos.y, -10);
        }
        else if (playerPos.x <= xMin)
        {
            if (playerPos.y >= yMax)
                transform.position = new Vector3(playerPos.x, yMax, -10);
            else if (playerPos.y <= yMin)
                transform.position = new Vector3(playerPos.x, yMin, -10);
            else
                transform.position = new Vector3(xMin, playerPos.y, -10);
        }
        else
            transform.position = playerPos;
    }
}
