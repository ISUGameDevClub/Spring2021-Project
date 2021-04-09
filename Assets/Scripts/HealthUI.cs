using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] HealthIcons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(int h)
    {
        for(int i = 0; i < HealthIcons.Length; i++)
        {
            if (i >= h)
                HealthIcons[i].enabled = false;
        }
    }
}
