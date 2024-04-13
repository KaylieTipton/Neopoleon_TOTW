using SuperPupSystems.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradeScroll;
    public GameObject clickUpgradesPanel;

    public string[] clickUpgradeName;

    public int[] clickUpgradeBaseCost;
    public float[] clickUpgradeCostMult;
    public float[] clickUpgradesBasePower;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 3);

        clickUpgradeName = new string[] { "+1 WP Per Click", "+5 WP Per Click", "+10 WP Per Click" };
        clickUpgradeBaseCost = new int[] { 10, 50, 100 };
        clickUpgradeCostMult= new float[] { 1.25f, 1.35f, 1.55f };
        clickUpgradesBasePower = new float[] { 1, 5, 10 };

        for(int i = 0; i <Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel.transform);
            upgrade.upgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        clickUpgradeScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI();

        


    }

    public void UpdateClickUpgradeUI(int _upgradeID = -1)
    {
        if(_upgradeID == -1)
        {
            for(int i = 0; i < clickUpgrades.Count; i++)
            {
                UpdateUI(i);
            }
        }
        else
        {
            UpdateUI(_upgradeID);
        }

        void UpdateUI(int _ID)
        {
            clickUpgrades[_ID].levelText.text = Controller.instance.data.clickUpgradeLevel[_ID] + " Workout Power Lvl";
            clickUpgrades[_ID].costText.text = "Cost: " + ClickUpgradeCost(_ID) + " Power";
            clickUpgrades[_ID].nameText.text = clickUpgradeName[_ID];
        }
        
    }

    public int ClickUpgradeCost(int _upgradeID)
    {
        return Convert.ToInt32(clickUpgradeBaseCost[_upgradeID] * Mathf.Pow(clickUpgradeCostMult[_upgradeID], Controller.instance.data.clickUpgradeLevel[_upgradeID]));
    }

    public void BuyUpgrade(int _upgradeID)
    {
        if (WalletManager.instance.ICanAfford(ClickUpgradeCost(_upgradeID)))
        {
            WalletManager.instance.Pay(ClickUpgradeCost(_upgradeID));
            Controller.instance.data.clickUpgradeLevel[_upgradeID] += 1;
        }
        UpdateClickUpgradeUI(_upgradeID);
    }
}
