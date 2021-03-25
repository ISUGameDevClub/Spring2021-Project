using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingPoint : MonoBehaviour
{
    private Animator anim;
    public Transform point1;
    public Transform point2;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
    
    }

    //anim.SetTrigger("Swing1");
}
