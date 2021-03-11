using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string newScene;
    public int spawnPosition;
    public float transitionTime = .25f;
    public Animator transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        PlayerData.playerSpawn = spawnPosition;
        transition.SetTrigger("Change Scene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(newScene);
    }
}
