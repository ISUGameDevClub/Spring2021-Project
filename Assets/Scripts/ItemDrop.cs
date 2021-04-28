using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public bool oneTime;
    public int oneTimeNumber;
    public int numberToDrop;
    public GameObject itemToDrop;
    public GameObject SoundObject;

    // Start is called before the first frame update
    void Start()
    {
        if(oneTime)
        {
            if (PlayerData.collectedChest[oneTimeNumber])
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateItem()
    {
        for(int i = 0; i < numberToDrop; i++)
            Instantiate(itemToDrop, transform.position,Quaternion.identity);
        if (oneTime)
            PlayerData.collectedChest[oneTimeNumber] = true;
        SoundObject.gameObject.transform.SetParent(null);
        SoundObject.GetComponent<AudioSource>().Play();
    }
}
