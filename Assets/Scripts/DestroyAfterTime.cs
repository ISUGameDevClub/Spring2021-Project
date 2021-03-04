using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int timeUntilDestroyed;

    void Start()
    {
        Destroy(gameObject, timeUntilDestroyed);
    }
}
