using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public int unlockedAbility;
    public float timeFreeze;
    public GameObject particle;

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
                FindObjectOfType<NotificationController>().ShowNotification("You can now shoot with right click", 3);
            }
            else if (unlockedAbility == 2)
            {
                PlayerData.unlockedDash = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now dash with shift", 3);
            }
            else if (unlockedAbility == 3)
            {
                PlayerData.unlockedCannon = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now use cannons with E", 3);
            }
            else if (unlockedAbility == 4)
            {
                PlayerData.unlockedGrapple = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now use grapples with Q", 3);
            }
            else if (unlockedAbility == 5)
            {
                PlayerData.unlockedWallJump = true;
                FindObjectOfType<NotificationController>().ShowNotification("You can now wall slide and wall jump", 3);
            }
            Zelda();
        }
    }


    public void Zelda()
    {
        StartCoroutine(ZeldaTime());
    }

    IEnumerator ZeldaTime()
    {
        Destroy(transform.GetChild(0).gameObject);
        Time.timeScale = 0;
        transform.position = FindObjectOfType<PlayerMovement>().transform.position + (Vector3.up * 1.85f);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        FindObjectOfType<PauseMenu>().cantPause = true;
        FindObjectOfType<PlayerMovement>().myAnim.SetBool("Zelda", true);
        GetComponent<AudioSource>().Play();
        if(FindObjectOfType<MusicManager>())
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().Pause();
        yield return new WaitForSecondsRealtime(timeFreeze);
        if (FindObjectOfType<MusicManager>())
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().Play();
        FindObjectOfType<PlayerMovement>().myAnim.SetBool("Zelda", false);
        FindObjectOfType<PauseMenu>().cantPause = false;
        Destroy(gameObject);
        Time.timeScale = 1;
        GameObject p = Instantiate(particle, FindObjectOfType<PlayerMovement>().gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        p.transform.SetParent(null);
    }
}
