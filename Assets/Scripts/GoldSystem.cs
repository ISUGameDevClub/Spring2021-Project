using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldSystem : MonoBehaviour
{
    public int totalGold;
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        totalGold = 0;
        goldText.text = "Coins: " + totalGold;
    }

    public void AddGold(int amount)
    {
        totalGold+= amount;
        goldText.text = "Coins: " + totalGold;
    }

    public void SubtractGold(int amount)
    {
        totalGold-= amount;
        goldText.text = "Coins: " + totalGold;
    }
}
