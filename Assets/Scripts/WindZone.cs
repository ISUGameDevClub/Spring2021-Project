using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float notActiveSeconds;
    public float activeSeconds;
    public bool constantWind = true;
    public float windPower;
    private Coroutine timerCoroutine;
    private bool inWindZone;
    private GameObject player;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(notActiveSeconds);
            active = true;
            yield return new WaitForSeconds(activeSeconds);
            active = false;
        }
    }

    private void FixedUpdate()
    {
        if (constantWind || active)
        {
            if (inWindZone)
            {
                player.transform.Translate(new Vector2(1 * Time.deltaTime * windPower, 0));
            }
        }
        else
        {
            if(timerCoroutine == null) timerCoroutine = StartCoroutine(Timer());
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
