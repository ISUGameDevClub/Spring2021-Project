using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private GoldSystem gs;
    private AmmoSystem ammoSys;

    public int ammoPouchUpgradeCost;
    public int goldPouchUpgradeCost;
    public int ExplosiveGunUpgradeCost;
    public GameObject LGP;
    public GameObject LAP;
    public GameObject EGU;
    // Start is called before the first frame update
    void Start()
    {
        ammoSys = FindObjectOfType<AmmoSystem>();
        gs = FindObjectOfType<GoldSystem>();
        Shop[] shops = FindObjectsOfType<Shop>();
        for(int i = 0; i < shops.Length; i++)
        {
            shops[i].shopUI = gameObject;
        }
        gameObject.SetActive(false);

        if (PlayerData.maxCoins == 50)
            LGP.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
        if (PlayerData.maxAmmo == 15)
            LAP.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
        if (PlayerData.unlockedBombShot)
            EGU.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateShop()
    {
        Time.timeScale = 0;
    }

    public void BuyAmmoPouchUpgrade()
    {
        if(gs.totalGold >= ammoPouchUpgradeCost && PlayerData.maxAmmo != 15)
        {
            gs.SubtractGold(ammoPouchUpgradeCost);
            ammoSys.setUpgradeAmmo();
            ShopUsed();
        }

    }

    public void BuyGoldPouchUpgrade()
    {
        if (gs.totalGold >= goldPouchUpgradeCost && PlayerData.maxCoins != 50)
        {
            gs.SubtractGold(goldPouchUpgradeCost);
            gs.setUpgradeGold();
            ShopUsed();
        }
    }

    public void BuyExplosiveGunUpgrade()
    {
        if(gs.totalGold >= ExplosiveGunUpgradeCost && PlayerData.unlockedBombShot == false)
        {
            gs.SubtractGold(ExplosiveGunUpgradeCost);
            PlayerData.unlockedBombShot = true;
            ShopUsed();
        }
    }

    public void DeactivateShop()
    {
        Time.timeScale = 1;
        FindObjectOfType<PauseMenu>().cantPause = false;
        gameObject.SetActive(false);
    }

    public void ShopUsed()
    {
        if (PlayerData.maxCoins == 50)
            LGP.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
        if (PlayerData.maxAmmo == 15)
            LAP.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
        if(PlayerData.unlockedBombShot)
            EGU.GetComponent<Image>().color = new Vector4(.3f, .3f, .3f, 1);
    }
}
