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

    public SpriteRenderer srPoint1;
    public SpriteRenderer srPoint2;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void Update()
    {
        if (!PlayerData.unlockedGrapple)
        {
            srPoint1.enabled = false;
            srPoint2.enabled = false;
        }
        else
        {
            srPoint1.enabled = true;
            srPoint2.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && !swinging && PlayerData.unlockedGrapple)
        {
            if(Vector2.Distance(point1.position,player.transform.position) < startDistance)
            {
                anim.SetTrigger("Swing1");
                player.GetComponent<PlayerMovement>().FaceLeft();
                attachPlayer();
            }else if(Vector2.Distance(point2.position,player.transform.position) < startDistance)
            {
                anim.SetTrigger("Swing2");
                player.GetComponent<PlayerMovement>().FaceRight();
                attachPlayer();
            }
        }
        else if(swinging)
        {
            player.transform.localPosition = Vector3.zero;
        }
    }

    public void attachPlayer()
    {
        swinging = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<PlayerMovement>().enableGravity = false;
        player.GetComponent<PlayerMovement>().DisableMovement(1);
        player.transform.SetParent(swingPoint.transform);
        player.transform.localPosition = Vector3.zero;
    }

    public void detachPlayer()
    {
        swinging = false;
        player.transform.localPosition = Vector3.zero;
        player.transform.SetParent(null);
        player.GetComponent<PlayerMovement>().enableGravity = true;
    }

}
