using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public int unlockedAbility;

    public void Start()
    {
        if (unlockedAbility == 1 && PlayerData.unlockedGun)
            Destroy(gameObject);
        if (unlockedAbility == 2 && PlayerData.unlockedDash)
            Destroy(gameObject);
        if (unlockedAbility == 3 && PlayerData.unlockedCannon)
            Destroy(gameObject);
        if (unlockedAbility == 4 && PlayerData.unlockedGrapple)
            Destroy(gameObject);
        if (unlockedAbility == 5 && PlayerData.unlockedWallJump)
            Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (unlockedAbility == 1)
            {
                PlayerData.unlockedGun = true;
                FindObjectOfType<NotificationController>().ShowNotification("You unlocked the gun", 3);
            }
            else if (unlockedAbility == 2)
            {
                PlayerData.unlockedDash = true;
                FindObjectOfType<NotificationController>().ShowNotification("You unlocked the dash", 3);
            }
            else if (unlockedAbility == 3)
            {
                PlayerData.unlockedCannon = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now use cannons", 3);
            }
            else if (unlockedAbility == 4)
            {
                PlayerData.unlockedGrapple = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now use grapples", 3);
            }
            else if (unlockedAbility == 5)
            {
                PlayerData.unlockedWallJump = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now wall jump", 3);
            }
            Destroy(gameObject);
        }
    }
}
