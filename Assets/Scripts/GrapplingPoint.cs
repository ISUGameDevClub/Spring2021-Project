using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingPoint : MonoBehaviour
{
    private Animator anim;
    public Transform point1;
    public Transform point2;
    public GameObject swingPoint;
    private GameObject player;
    public float startDistance;
    private bool swinging;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !swinging)
        {
            if(Vector2.Distance(point1.position,player.transform.position) < startDistance)
            {
                anim.SetTrigger("Swing1");
                attachPlayer();
            }else if(Vector2.Distance(point2.position,player.transform.position) < startDistance)
            {
                anim.SetTrigger("Swing2");
                attachPlayer();
            }
        }
    }

    public void attachPlayer()
    {
        swinging = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<PlayerMovement>().DisableMovement(1);
        player.transform.SetParent(swingPoint.transform);
        player.transform.localPosition = Vector3.zero;
    }

    public void detachPlayer()
    {
        swinging = false;
        player.transform.SetParent(null);
    }

}
