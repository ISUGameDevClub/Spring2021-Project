using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public SpriteRenderer[] HealthIcons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(Health h)
    {
        for(int i = 0; i < HealthIcons[].length; i++)
        {
            if (HealthIcons[i] > h.curHealth)
                HealthIcons[i].enabled = false;
        }
    }
}
