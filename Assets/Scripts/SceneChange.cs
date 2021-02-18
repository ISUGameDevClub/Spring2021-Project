using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public int scene;

    public float transitionTime = 1f;

    public Animator transition;
    private void OnTriggerEnter2D()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("start");

        SceneManager.LoadScene(scene);

        yield return new WaitForSeconds(transitionTime);

        
    }
}
