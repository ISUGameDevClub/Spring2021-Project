using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private GoldSystem gs;
    private AmmoSystem ammoSys;

    public int ammoPouchUpgradeCost;
    public int goldPouchUpgradeCost;
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
        if(gs.totalGold >= ammoPouchUpgradeCost && ammoSys.upgraded == false)
        {
            gs.SubtractGold(ammoPouchUpgradeCost);
            ammoSys.setUpgradeAmmo();
        }
        
    }

    public void BuyGoldPouchUpgrade()
    {
        if (gs.totalGold >= goldPouchUpgradeCost && gs.upgraded == false)
        {
            gs.SubtractGold(goldPouchUpgradeCost);
            gs.setUpgradeGold();
        }
    }

    public void DeactivateShop()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
