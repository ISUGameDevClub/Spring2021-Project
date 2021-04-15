using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource currentSong;
    private AudioClip nextSong;
    private bool loadingNewSong;
    public float musicVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentSong = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (loadingNewSong && currentSong.volume > 0)
            currentSong.volume -= Time.deltaTime * 4 * musicVolume;
        else if (loadingNewSong)
        {
            loadingNewSong = false;
            currentSong.clip = nextSong;
            currentSong.Play();
        }
        else if(currentSong.volume < musicVolume)
        {
            currentSong.volume += Time.deltaTime * 4 * musicVolume;
        }
        else
        {
            currentSong.volume = musicVolume;
        }
    }

    public void StartNewSong(AudioClip ns)
    {
        loadingNewSong = true;
        nextSong = ns;
    }
}
