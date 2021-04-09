using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    public bool debugCannonUnlock;
    public bool debugDashUnlock;
    public bool debugGrappleUnlock;
    public bool debugWallJumpUnlock;
    public Text fuelText;
    public static int playerSpawn;
    public static int coins;
    public static int ammo;
    public static int fuel = 0;
    public static bool[] collectedFuel = new bool[30];
    public static bool unlockedDash;
    public static bool unlockedCannon;
    public static bool unlockedGrapple;
    public static bool unlockedWallJump;
    


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

        if (debugWallJumpUnlock)
        {
            unlockedWallJump = true;
        }

        if (fuelText != null)
            fuelText.text = "Fuel: " + fuel;
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public static void UpdatePlayerData(int sp, int co, int am)
    {
        playerSpawn = sp;
        coins = co;
        ammo = am;
    }

    public static void SetPlayerData()
    {
        FindObjectOfType<GoldSystem>().AddGold(coins);
        FindObjectOfType<AmmoSystem>().PickupAmmo(ammo);

    }

    public void CollectFuel()
    {
        fuel++;
        if(fuelText != null)
            fuelText.text = "Fuel: " + fuel;
    }
}
