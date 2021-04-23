using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public static float masterVolume;
    public static float musicVolume;

    private bool paused;
    [HideInInspector]
    public bool cantPause;

    void Start()
    {
        menu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !cantPause)
        {
            if (paused)
                Unpause();
            else
                Pause();
        }
    }
    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void Unpause()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    
    public void ReturnToHub()
    {
        Unpause();
        cantPause = true;
        GetComponent<SceneTransition>().newScene = "MainHub";
        GetComponent<SceneTransition>().StartGame();
    }
    public void ReturnToTitle()
    {
        Unpause();
        cantPause = true;
        GetComponent<SceneTransition>().newScene = "Title";
        GetComponent<SceneTransition>().StartGame();
    }

    public void MasterVolumeSlider(float master)
    {
        masterVolume = master;
        AudioListener.volume = masterVolume;
    }
    public void MusicVolumeSlider(float music)
    {
        musicVolume = music;
        if (FindObjectOfType<MusicManager>())
        {
            FindObjectOfType<MusicManager>().musicVolume = musicVolume;
            FindObjectOfType<MusicManager>().currentSong.volume = musicVolume;
        }
    }
}
