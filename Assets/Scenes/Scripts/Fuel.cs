using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fuel : MonoBehaviour
{
    public int myFuelNumber;
    public GameObject particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject p = Instantiate(particle, transform.position, new Quaternion(0, 0, 0, 0));
            p.transform.SetParent(null);
            PlayerData.collectedFuel[myFuelNumber] = true;
            FindObjectOfType<PlayerData>().CollectFuel();
            if (PlayerData.fuel == 3)
            {
                FindObjectOfType<NotificationController>().ShowNotification("You have enough fuel to go to the next city!", 5);
            }
            else if (PlayerData.fuel == 6)
            {
                FindObjectOfType<NotificationController>().ShowNotification("You have enough fuel to go to the next city!", 5);
            }
            else
            {
                FindObjectOfType<NotificationController>().ShowNotification("You found fuel for the ship!", 3);
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerData.collectedFuel[myFuelNumber])
            Destroy(gameObject);       
    }
}
