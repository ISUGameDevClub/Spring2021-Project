using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool touchingShop;
    public GameObject shopUI;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingShop && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(true);
            shopUI.GetComponent<ShopManager>().ActivateShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingShop = false;
        }
    }

    private void OpenShop()
    {

    }

}
