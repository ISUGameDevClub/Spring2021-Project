using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject notification;
    public string notificationText;
    public int timeOfNotification;

    private Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NotificationTime();
    }

    private IEnumerator NotificationTime()
    {
        notification.SetActive(true);
        yield return new WaitForSeconds(timeOfNotification);
        notification.SetActive(false);
    }

    
}
