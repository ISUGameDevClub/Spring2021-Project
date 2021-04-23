using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public bool canChange;
    public int currentSceneSelection;
    public float displayTime;
    public string[] Scenes;
    public GameObject SceneTransition;
    public int fuelToUnlockWaterway = 3;
    public int fuelToUnlockCrayolaCity = 6;
    //make sure to enter scenes in editor or this will not work. The size of the array can be changed for adding additional levels
    void Start()
    {
        canChange = false;
        SceneTransition.GetComponent<SceneTransition>().newScene = Scenes[0];
        currentSceneSelection = 0;
    }
    void Update()
    {
        if(canChange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(currentSceneSelection == Scenes.Length)
                    currentSceneSelection = 0;
                if (currentSceneSelection == 1 && PlayerData.fuel < fuelToUnlockWaterway)
                {
                    FindObjectOfType<NotificationController>().ShowNotification("Need " + fuelToUnlockWaterway + " fuel to unlock " + Scenes[currentSceneSelection], displayTime);
                }
                else if (currentSceneSelection == 2 && PlayerData.fuel < fuelToUnlockCrayolaCity)
                {
                    FindObjectOfType<NotificationController>().ShowNotification("Need " + fuelToUnlockCrayolaCity + " fuel to unlock " + Scenes[currentSceneSelection], displayTime);
                }
                else
                {
                    SceneTransition.GetComponent<SceneTransition>().newScene = Scenes[currentSceneSelection];
                    FindObjectOfType<NotificationController>().ShowNotification("City Set To " + Scenes[currentSceneSelection], displayTime);
                }
                currentSceneSelection++;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<NotificationController>().ShowNotification("Press E to select city", 3);
            canChange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canChange = false;
            FindObjectOfType<NotificationController>().ShowNotification("Jump off the boat to enter the city", 3);
        }
    }
}
