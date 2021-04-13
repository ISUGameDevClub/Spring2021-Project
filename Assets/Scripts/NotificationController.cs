﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    private Text myText;
    private Coroutine sn;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNotification(string text, float timeShown)
    {
        if (sn != null)
            StopCoroutine(sn);
        sn = StartCoroutine(NotificationTime(text, timeShown));
    }

    private IEnumerator NotificationTime(string shownText, float timeShown)
    {
        myText.text = shownText;
        yield return new WaitForSeconds(timeShown);
        myText.text = "";
    }

    
}
