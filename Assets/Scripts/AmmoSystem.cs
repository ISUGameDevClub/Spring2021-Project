using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    public int totalAmmo;
    public Text ammoText;
    public bool upgraded;
    public int maxDefaultAmmo;
    public int maxUpgradedAmmo;

    private NotificationController nc;

    // Start is called before the first frame update
    void Start()
    {
        nc = FindObjectOfType<NotificationController>();
        upgraded = false;
        if (ammoText != null && !upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;
        else if (ammoText != null && upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;
    }

    public void UseAmmo(int amount)
    {
        totalAmmo -= amount;
        if (ammoText != null && !upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;
        else if (ammoText != null && upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;
    }

    public void PickupAmmo(int amount)
    {
        totalAmmo += amount;
        if (upgraded == false && totalAmmo > maxDefaultAmmo)
        {
            totalAmmo = maxDefaultAmmo;
            if(nc)
                nc.ShowNotification("Ammo Pouch Full", 1);
        }
        else if (upgraded == true && totalAmmo > maxUpgradedAmmo)
        {
            totalAmmo = maxUpgradedAmmo;
            if (nc)
                nc.ShowNotification("Ammo Pouch Full", 1);
        }
        
        if (ammoText != null && !upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;
        else if (ammoText != null && upgraded)
            ammoText.text = "Ammo: " + totalAmmo + "/" + maxDefaultAmmo;

    }

    public void setUpgradeAmmo()
    {
        upgraded = true;
    }
    
}
