using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public float spawnForce;
    public float timeUntilActive;
    public float range;
    public float speed;
    private Rigidbody2D rb;
    private GameObject player;
    private bool moveTowardsPlayer;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        StartCoroutine(DelayMoving());
        circleCollider.isTrigger = false;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-spawnForce,spawnForce), .5f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < range && moveTowardsPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator DelayMoving()
    {
        yield return new WaitForSeconds(timeUntilActive);
        moveTowardsPlayer = true;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        circleCollider.isTrigger = true;
    }
}
