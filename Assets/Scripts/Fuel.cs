using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fuel : MonoBehaviour
{
    public int myFuelNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            PlayerData.collectedFuel[myFuelNumber] = true;
            FindObjectOfType<PlayerData>().CollectFuel();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerData.collectedFuel[myFuelNumber])
            Destroy(gameObject);       
    }
}
