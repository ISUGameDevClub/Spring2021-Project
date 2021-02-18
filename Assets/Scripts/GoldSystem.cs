using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    private int totalGold;

    // Start is called before the first frame update
    void Start()
    {
        totalGold = 0;
    }

    private void AddGold(int amount)
    {
        totalGold+= amount;
    }

    private void SubtractGold(int amount)
    {
        totalGold-= amount;
    }
}
