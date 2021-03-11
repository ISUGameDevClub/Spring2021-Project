﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcText : MonoBehaviour
{
    public string textShown;
    public float lengthShown;
    public Text myText;
    public GameObject textBox;
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
        if (collision.gameObject.tag == "Player")
        {
            ShowText(textShown, lengthShown);
        }
    }
    public void ShowText(string text, float timeShown)
    {
        StartCoroutine(TextTime(text, timeShown));
    }

    private IEnumerator TextTime(string shownText, float timeShown)
    {
        myText.text = shownText;
        textBox.SetActive(true);
        yield return new WaitForSeconds(timeShown);
        textBox.SetActive(false);
        myText.text = "";
    }
}
