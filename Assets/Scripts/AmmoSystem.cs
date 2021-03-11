using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    public int totalAmmo;
    public Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        totalAmmo = 0;
        if(ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo;
    }

    public void UseAmmo(int amount)
    {
        totalAmmo -= amount;
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo;
    }

    public void PickupAmmo(int amount)
    {
        totalAmmo += amount;
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo;
    }
    
}
