using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldSystem : MonoBehaviour
{
    public int totalGold;
    public Text goldText;
    public bool upgraded;
    public int maxDefaultGold;
    public int maxUpgradedGold;

    private NotificationController nc;

    // Start is called before the first frame update
    void Start()
    {
        nc = FindObjectOfType<NotificationController>();
        upgraded = false;
        if(goldText != null && !upgraded)
            goldText.text = "Coins: " + totalGold + "/" + maxDefaultGold;
        else if (goldText != null && upgraded)
            goldText.text = "Coins: " + totalGold + "/" + maxUpgradedGold;
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        if (upgraded == false && totalGold > maxDefaultGold)
        {
            totalGold = maxDefaultGold;
            if(nc != null)
                nc.ShowNotification("Gold Pouch Full", 1);
        }
        else if (upgraded == true && totalGold > maxUpgradedGold)
        {
            totalGold = maxUpgradedGold;
            if (nc != null)
                nc.ShowNotification("Gold Pouch Full", 1);
        }

        if (goldText != null)
        {
            if (goldText != null && !upgraded)
                goldText.text = "Coins: " + totalGold + "/" + maxDefaultGold;
            else if (goldText != null && upgraded)
                goldText.text = "Coins: " + totalGold + "/" + maxUpgradedGold;
        }
    }

    public void SubtractGold(int amount)
    {
        totalGold-= amount;
        if (goldText != null && !upgraded)
            goldText.text = "Coins: " + totalGold + "/" + maxDefaultGold;
        else if (goldText != null && upgraded)
            goldText.text = "Coins: " + totalGold + "/" + maxUpgradedGold;
    }

    public void setUpgradeGold()
    {
        upgraded = true;
    }
}
