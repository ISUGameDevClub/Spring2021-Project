using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    public int totalGold;

    // Start is called before the first frame update
    void Start()
    {
        totalGold = 0;
    }

    public void AddGold(int amount)
    {
        totalGold+= amount;
    }

    public void SubtractGold(int amount)
    {
        totalGold-= amount;
    }
}
