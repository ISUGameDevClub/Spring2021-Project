using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float seconds;
    public bool constantWind = true;
    public float windPower;
    private Coroutine timerCoroutine;
    private bool inWindZone;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator Timer(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            
             for (int i = 0; i < 60; i++)
                { 
                    if (inWindZone)
                    {
                        player.transform.Translate(new Vector2(windPower / 60, 0));
                    }
                yield return new WaitForSeconds(1f / 60f);
                }
            
        }
    }

    private void FixedUpdate()
    {
        if (constantWind)
        {
            if (inWindZone)
            {
                player.transform.Translate(new Vector2(1 * Time.deltaTime * windPower, 0));
            }
        }
        else
        {
            if(timerCoroutine == null) timerCoroutine = StartCoroutine(Timer(seconds));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inWindZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inWindZone = false;
        }
    }
}
