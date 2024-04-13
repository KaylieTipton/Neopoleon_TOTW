using SuperPupSystems.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this; 
    public int clickUpgradeBaseCost;
    public float clickUpgradeCostMult;

    public Upgrades clickUpgrade;
    public string clickUpgradeName;

    public void StartUpgradeManager()
    {
        
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5f;
        UpdateClickUpgradeUI();


    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.levelText.text = Controller.instance.data.clickUpgradeLevel + " Workout Power";
        clickUpgrade.costText.text = "Cost: " + Cost() + " Power Level";
        clickUpgrade.nameText.text = "+1 " + clickUpgradeName;
    }

    public int Cost()
    {
        return Convert.ToInt32(clickUpgradeBaseCost * Mathf.Pow(clickUpgradeCostMult, Controller.instance.data.clickUpgradeLevel));
    }

    public void BuyUpgrade()
    {
        if (WalletManager.instance.ICanAfford(Cost()))
        {
            WalletManager.instance.Pay(Cost());
            Controller.instance.data.clickUpgradeLevel += 1;
        }
        UpdateClickUpgradeUI();
    }
}
