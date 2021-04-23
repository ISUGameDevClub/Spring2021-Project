using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParrotFollow : MonoBehaviour
{
    public float speed;
    private PlayerMovement pm;
    private Transform target;
    private Vector2 tar;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainHub")
        {
            Destroy(gameObject);
        }

        sr = GetComponent<SpriteRenderer>();
        pm = FindObjectOfType<PlayerMovement>();
        target = pm.GetComponent<Transform>();
        transform.position = pm.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tar = target.position;
        tar[1] = tar[1] + 1.25f;
        if (pm.facingRight)
        {

            tar[0] = tar[0] - 1.5f;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }
        else
        {
            tar[0] = tar[0] + 1.5f;
            transform.position = Vector2.Lerp(transform.position, tar, speed * Time.deltaTime);
        }

        if (tar.x > transform.position.x)
        {
            sr.flipX = true;
        }
        else
            sr.flipX = false;
    }
}
