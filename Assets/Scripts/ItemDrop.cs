using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemToDrop;
    public bool isDying;
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
        Instantiate(itemToDrop, transform.position,Quaternion.identity);
    }
}
