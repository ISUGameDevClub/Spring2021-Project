using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float timeBetweenShots;
    public GameObject bullet;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        StartCoroutine(Shoot());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            if (player.transform.position.x > transform.position.x)
            {
                GameObject bul = Instantiate(bullet, transform.position + transform.right, new Quaternion(0, 0, 0, 0));
                bul.GetComponent<Bullet>().facingRight = true;
                bul.transform.SetParent(null);
            }
            else
            {
                GameObject bul = Instantiate(bullet, transform.position - transform.right, new Quaternion(0, 0, 0, 0));
                bul.GetComponent<Bullet>().facingRight = false;
                bul.transform.SetParent(null);
            }
            

        }
    }
}
