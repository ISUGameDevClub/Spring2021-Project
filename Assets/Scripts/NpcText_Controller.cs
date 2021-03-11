using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcText_Controller : MonoBehaviour
{
    private Text myText;


    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowText(string text, float timeShown)
    {
        StartCoroutine(TextTime(text, timeShown));
    }

    private IEnumerator TextTime(string shownText, float timeShown)
    {
        myText.text = shownText;
        yield return new WaitForSeconds(timeShown);
        myText.text = "";
    }
}
