﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int numberToDrop;
    public GameObject itemToDrop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateItem()
    {
        for(int i = 0; i < numberToDrop; i++)
            Instantiate(itemToDrop, transform.position,Quaternion.identity);
    }
}
