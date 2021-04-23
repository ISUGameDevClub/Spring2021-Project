using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbBird : MonoBehaviour
{
    private bool tp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tp = true;
            FindObjectOfType<NotificationController>().ShowNotification("Press E to pet the parrot", 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            tp = false;
    }

    private void Update()
    {
        if(tp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<PlayerMovement>().transform.localPosition = new Vector3(-4.75f, 3, 0);
                FindObjectOfType<PlayerMovement>().DisableMovement(1.5f);
                FindObjectOfType<PlayerMovement>().FaceRight();
                FindObjectOfType<PlayerMovement>().myAnim.SetTrigger("Pet");
                FindObjectOfType<NotificationController>().ShowNotification("", .1f);
            }
        }
    }
}
