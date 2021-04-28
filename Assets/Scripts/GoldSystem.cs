using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldSystem : MonoBehaviour
{
    public int totalGold;
    public Text goldText;

    private NotificationController nc;

    // Start is called before the first frame update
    void Start()
    {
        nc = FindObjectOfType<NotificationController>();
        if(goldText != null)
            goldText.text = "Coins: " + totalGold + "/" + PlayerData.maxCoins;
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        if (totalGold > PlayerData.maxCoins)
        {
            totalGold = PlayerData.maxCoins;
            if(nc != null)
                nc.ShowNotification("Gold Pouch Full", 1);
        }

        if (goldText != null)
        {
            if (goldText != null)
                goldText.text = "Coins: " + totalGold + "/" + PlayerData.maxCoins;
        }
    }

    public void SubtractGold(int amount)
    {
        totalGold-= amount;
        if (goldText != null)
            goldText.text = "Coins: " + totalGold + "/" + PlayerData.maxCoins;
    }

    public void setUpgradeGold()
    {
        PlayerData.maxCoins = 50;
        if (goldText != null)
            goldText.text = "Coins: " + totalGold + "/" + PlayerData.maxCoins;
    }
}
