using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    static bool inCannon;

    public bool autoFire;
    public float cannonSpeed;
    public Vector2 cannonAngle;
    private Rigidbody2D rb;
    private bool isCannon;
    private PlayerMovement pm;
    private bool shotPlayer;
    private bool loaded;
    public GameObject Smoke;
    public GameObject SmokeSpawn;
    private AudioSource fireSound;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        rb = pm.GetComponent<Rigidbody2D>();
        fireSound = GetComponent<AudioSource>();
    }

    void Update()
    {
       if(isCannon && PlayerData.unlockedCannon && ((!autoFire && Input.GetKeyDown(KeyCode.E)) || autoFire) && !shotPlayer && !loaded)
       {
            StartCoroutine(FireCannon());
       }

       if(shotPlayer)
       {
            if(rb.velocity.magnitude <= .05f && !inCannon)
            {
                shotPlayer = false;
                pm.scriptedMovement = false;
            }
       }
    }

    private IEnumerator FireCannon()
    {
        if (cannonAngle.x > 0)
            pm.FaceRight();
        else
            pm.FaceLeft();

        inCannon = true;
        rb.velocity = new Vector2(0, 0);
        loaded = true;
        pm.scriptedMovement = true;
        pm.myAnim.SetTrigger("Fire Cannon");
        yield return new WaitForSeconds(.2f);
        fireSound.Play();
        rb.gameObject.transform.position = transform.position;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(cannonAngle.normalized * cannonSpeed, ForceMode2D.Impulse);
        GameObject s = Instantiate(Smoke, SmokeSpawn.transform.position, new Quaternion(0, 0, 0, 0));
        s.transform.SetParent(null);
        yield return new WaitForSeconds(.2f);
        inCannon = false;
        loaded = false;
        shotPlayer = true;
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player")
       {
            isCannon = true;
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCannon = false;
        }
    }
}
