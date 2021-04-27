using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    public int totalAmmo;
    public Text ammoText;

    private NotificationController nc;

    // Start is called before the first frame update
    void Start()
    {
        nc = FindObjectOfType<NotificationController>();
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo + "/" + PlayerData.maxAmmo;
    }

    public void UseAmmo(int amount)
    {
        totalAmmo -= amount;
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo + "/" + PlayerData.maxAmmo;
    }

    public void PickupAmmo(int amount)
    {
        totalAmmo += amount;
        if (totalAmmo > PlayerData.maxAmmo)
        {
            totalAmmo = PlayerData.maxAmmo;
            if(nc)
                nc.ShowNotification("Ammo Pouch Full", 1);
        }
        
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo + "/" + PlayerData.maxAmmo;
    }

    public void setUpgradeAmmo()
    {
        PlayerData.maxAmmo = 15;
        if (ammoText != null)
            ammoText.text = "Ammo: " + totalAmmo + "/" + PlayerData.maxAmmo;
    }
    
}
