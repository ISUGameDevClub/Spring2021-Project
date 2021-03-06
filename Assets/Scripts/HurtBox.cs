﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public bool isPlayer;
    public int damage;
    public bool canMultiHit;
    public GameObject[] AttackedArray;
    public float knockbackPower;
    public float knockbackTime;


    private void Start()
    {
        AttackedArray = new GameObject[10];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if ((collision.gameObject.tag == "Player" && !isPlayer) || (collision.gameObject.tag == "Enemy" && isPlayer))
        {
            bool onArray = false;
            if (!canMultiHit)
            {
                for (int i = 0; i < AttackedArray.Length; i++)
                {
                    if (collision.gameObject == AttackedArray[i])
                    {
                        onArray = true;
                    }
                }

                if (collision.gameObject.GetComponent<Health>() != null && !onArray)
                {
                    for (int i = 0; i < AttackedArray.Length; i++)
                    {
                        if (AttackedArray[i] == null)
                        {
                            AttackedArray[i] = collision.gameObject;
                            break;
                        }

                    }
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage, transform.position.x, knockbackPower, knockbackTime);
                }
            }
            else
                collision.gameObject.GetComponent<Health>().TakeDamage(damage, transform.position.x, knockbackPower, knockbackTime);
        }
    }
    public void ClearArray()
    {
        for (int i = 0; i < AttackedArray.Length; i++)
        {
            AttackedArray[i] = null;
        }
    }
}
