using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public int mySpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.playerSpawn == mySpawn)
            FindObjectOfType<PlayerMovement>().gameObject.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
