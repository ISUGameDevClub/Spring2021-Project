using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource fs1;
    public AudioSource fs2;

    public void Footstep1()
    {
        fs1.Play();
    }

    public void Footstep2()
    {
        fs2.Play();
    }
}
