﻿using System.Collections;
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
        if (upgraded == false && totalAmmo > maxDefaultAmmo)
        {
            totalAmmo = maxDefaultAmmo;
            nc.ShowNotification("Ammo Pouch Full", 1);
        }
        else if (upgraded == true && totalAmmo > maxUpgradedAmmo)
        {
            totalAmmo = maxUpgradedAmmo;
            nc.ShowNotification("Ammo Pouch Full", 1);
        }
        ammoText.text = "Ammo: " + totalAmmo;

    }

    public void setUpgradeAmmo()
    {
        upgraded = true;
    }
    
}
