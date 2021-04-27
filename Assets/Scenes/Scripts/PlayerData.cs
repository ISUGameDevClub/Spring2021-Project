using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    public bool debugGun;
    public bool debugStartWithAmmo;
    public bool debugStartWithGold;
    public bool debugCannonUnlock;
    public bool debugDashUnlock;
    public bool debugGrappleUnlock;
    public bool debugWallJumpUnlock;
    public bool debugBombShot;
    public Text fuelText;
    public static int playerSpawn;
    public static int coins = 20;
    public static int maxCoins = 20;
    public static int ammo;
    public static int maxAmmo = 10;
    public static int fuel = 0;
    public static bool[] collectedFuel = new bool[30];
    public static bool[] collectedChest = new bool[30];
    public static bool unlockedGun;
    public static bool unlockedDash;
    public static bool unlockedCannon;
    public static bool unlockedGrapple;
    public static bool unlockedWallJump;
    public static bool unlockedBombShot;



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

        if (debugBombShot)
        {
            unlockedBombShot = true;
        }

        if (debugGun)
        {
            unlockedGun = true;
        }

        if (debugStartWithGold)
            FindObjectOfType<GoldSystem>().AddGold(20);

        if (debugStartWithAmmo)
            FindObjectOfType<AmmoSystem>().PickupAmmo(5);

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

    public static void ResetChests()
    {
        for(int x = 0; x < collectedChest.Length; x++)
        {
            collectedChest[x] = false;
        }
    }
}
