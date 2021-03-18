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
        totalGold = 0;
        goldText.text = "Coins: " + totalGold;
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        if (upgraded == false && totalGold > maxDefaultGold)
        {
            totalGold = maxDefaultGold;
            nc.ShowNotification("Gold Pouch Full", 1);
        }
        else if (upgraded == true && totalGold > maxUpgradedGold)
        {
            totalGold = maxUpgradedGold;
            nc.ShowNotification("Gold Pouch Full", 1);
        }
        goldText.text = "Coins: " + totalGold;
    }

    public void SubtractGold(int amount)
    {
        totalGold-= amount;
        goldText.text = "Coins: " + totalGold;
    }

    public void setUpgradeGold()
    {
        upgraded = true;
    }
}
