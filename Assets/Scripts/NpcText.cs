﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcText : MonoBehaviour
{
    public string[] textsShown;
    public float lengthShown;
    private int currentText;
    public Text myText;
    public Text myTextBox;
    public GameObject textBox;
    public Coroutine currentCor;

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
            ShowText(textsShown[currentText], lengthShown);
            currentText++;
            if (currentText >= textsShown.Length)
                currentText = 0;

        }
    }

    public void ShowText(string text, float timeShown)
    {
        if(currentCor != null)
            StopCoroutine(currentCor);
        currentCor = StartCoroutine(TextTime(text, timeShown));
    }

    private IEnumerator TextTime(string shownText, float timeShown)
    {
        myTextBox.text = shownText;
        myText.text = shownText;
        textBox.SetActive(true);
        yield return new WaitForSeconds(timeShown);
        textBox.SetActive(false);
        myText.text = "";
        myTextBox.text = "";
    }
}
