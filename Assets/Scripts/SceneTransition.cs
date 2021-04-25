using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public bool interactable;
    public string newScene;
    public int spawnPosition;
    public float transitionTime = .25f;
    public Animator transition;
    public AudioClip newSong;

    private bool touchingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !interactable)
            StartCoroutine(LoadLevel());
        else if (collision.gameObject.tag == "Player" && interactable)
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && interactable)
        {
            touchingPlayer = false;
        }
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadLevel());
        }
    }

    public IEnumerator LoadLevel()
    {
        if (newSong != null && FindObjectOfType<MusicManager>())
            FindObjectOfType<MusicManager>().StartNewSong(newSong);
        if (FindObjectOfType<GoldSystem>() && FindObjectOfType<AmmoSystem>())
            PlayerData.UpdatePlayerData(spawnPosition, FindObjectOfType<GoldSystem>().totalGold, FindObjectOfType<AmmoSystem>().totalAmmo);
        transition.SetTrigger("Change Scene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(newScene);
    }

    public void StartGame()
    {
            StartCoroutine(LoadLevel());
    }
    public void StartCredits()
    {
        newScene = "Credits";
        StartCoroutine(LoadLevel());
    }
    public void ReturnToTitle()
    {
        newScene = "Title";
        StartCoroutine(LoadLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
