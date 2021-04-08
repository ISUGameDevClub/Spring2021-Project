using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool debugCannonUnlock;
    public bool debugDashUnlock;
    public bool debugGrappleUnlock;

    public static int playerSpawn;
    public static int coins;
    public static int ammo;
    public static int fuel;
    public static bool unlockedDash;
    public static bool unlockedCannon;
    public static bool unlockedGrapple;

    // Start is called before the first frame update
    void Awake()
    {
        SetPlayerData();

        if(debugCannonUnlock)
        {
            unlockedCannon = true;
        }

        if (debugDashUnlock)
        {
            unlockedDash = true;
        }

        if (debugGrappleUnlock)
        {
            unlockedGrapple = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdatePlayerData(int sp, int co, int am, int fu)
    {
        playerSpawn = sp;
        coins = co;
        ammo = am;
        fuel = fu;
    }

    public static void SetPlayerData()
    {
        FindObjectOfType<GoldSystem>().AddGold(coins);
        FindObjectOfType<AmmoSystem>().PickupAmmo(ammo);

    }
}
