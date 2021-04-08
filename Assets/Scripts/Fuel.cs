using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public static int fuelCount;
    public Text fuelText;
    public float destroyAfter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Fuel")
        {
            fuelCount++;
            Destroy(collision.gameObject);
        }
    }
    private void Start()
    {
        if(fuelText != null)
            fuelText.text = "Fuel: " + fuelCount;
    }
    private void FixedUpdate()
    {
        if (fuelText != null)
            fuelText.text = "Fuel: " + fuelCount;
    }

}
