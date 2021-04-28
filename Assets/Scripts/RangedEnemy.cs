using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public float timeBetweenShots;
    public GameObject bullet;
    private GameObject player;
    private float shotTimer;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        myAnim = GetComponent<Animator>();
        shotTimer = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;
        else
        {
            myAnim.SetTrigger("Attack");
            shotTimer = timeBetweenShots;
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

    public void ResetAttack()
    {
        shotTimer = timeBetweenShots;
    }

    public void ResetAttack(float extraTime)
    {
        shotTimer = timeBetweenShots + extraTime;
    }
}
