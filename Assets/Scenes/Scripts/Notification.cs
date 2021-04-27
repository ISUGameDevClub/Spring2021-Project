using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public string textShown;
    public float lengthShown;

    private NotificationController nc;

    private void Start()
    {
        nc = FindObjectOfType<NotificationController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            nc.ShowNotification(textShown, lengthShown);
        }
    }
}
