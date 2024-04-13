using SuperPupSystems.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradePrefab;

    public ScrollRect clickUpgradeScroll;
    public GameObject clickUpgradesPanel;

    public ScrollRect productionUpgradeScroll;
    public GameObject productionUpgradesPanel;

    public string[] clickUpgradeName;
    public string[] productionUpgradeName;
    public string clickUpgradeTitle;
    public string productionUpgradeTitle;

    public double[] clickUpgradeBaseCost;
    public double[] clickUpgradeCostMult;
    public double[] clickUpgradesBasePower;

    public double[] productionUpgradeBaseCost;
    public double[] productionUpgradeCostMult;
    public double[] productionUpgradesBasePower;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 3);

        clickUpgradeName = new string[] 
        { 
            "+1 WP Per Click", 
            "+5 WP Per Click", 
            "+10 WP Per Click" 
        };
        productionUpgradeName = new string[] 
        {
            "+1 Meditation/s",
            "+2 Meditation/s",
            "+10 Meditation/s"
        };

        clickUpgradeTitle = "Workout Power";
        productionUpgradeTitle = "Meditation Power";

        clickUpgradeBaseCost = new double[] { 10, 50, 100 };
        clickUpgradeCostMult= new double[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new double[] { 1, 5, 10 };

        productionUpgradeBaseCost = new double[] { 25, 100, 1000 };
        productionUpgradeCostMult = new double[] { 1.5, 1.75, 2 };
        productionUpgradesBasePower = new double[] { 1, 2, 10 };

        for (int i = 0; i <Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel.transform);
            upgrade.upgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradePrefab, productionUpgradesPanel.transform);
            upgrade.upgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        clickUpgradeScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradeScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");

    }

    public void UpdateUpgradeUI(string _type, int _upgradeID = -1)
    {
        var data = Controller.instance.data;
        int total = 0;
        switch (_type)
        {
            case "click":
                if (_upgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeName, clickUpgradeTitle, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeName, clickUpgradeTitle, _upgradeID);
                for (int i = 0; i < clickUpgrades.Count; i++)
                {
                    total += data.clickUpgradeLevel[i];
                }
                CheckWorkoutLevel(total);
                break;
            case "production":
                if (_upgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeName, productionUpgradeTitle, i);
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeName, productionUpgradeTitle, _upgradeID);
                for(int i = 0; i < productionUpgrades.Count; i++)
                {
                    total += data.productionUpgradeLevel[i];
                }
                CheckMeditationLevel(total);
                Debug.Log("Meditation: " + total);
                break;
        }

        void UpdateUI(List<Upgrades> _upgrades, List<int> upgradeLevels, string[] upgradeNames, string upgradeTitle, int _ID)
        {
            _upgrades[_ID].levelText.text = upgradeLevels[_ID] + " " + upgradeTitle;
            _upgrades[_ID].costText.text = $"Cost: {UpgradeCost(_type, _ID):F2} Power";
            _upgrades[_ID].nameText.text = upgradeNames[_ID];
        }
        
    }

    public double UpgradeCost(string _type, int _upgradeID)
    {
        var data = Controller.instance.data;
        switch (_type)
        {
            case "click":
                return clickUpgradeBaseCost[_upgradeID] * Math.Pow(clickUpgradeCostMult[_upgradeID], data.clickUpgradeLevel[_upgradeID]);
            case "production":
                return productionUpgradeBaseCost[_upgradeID] * Math.Pow(productionUpgradeCostMult[_upgradeID], data.productionUpgradeLevel[_upgradeID]); ;

        }

        return 0;
        
    }

    public void BuyUpgrade(string _type, int _upgradeID)
    {
        var data = Controller.instance.data;
        switch (_type) 
        { 
            case "click": Buy(data.clickUpgradeLevel);
                break;
            case "production": Buy(data.productionUpgradeLevel);
                break;
        
        
        }
    
        void Buy(List<int> upgradeLevels)
        {
            if (Wallet.instance.ICanAfford(UpgradeCost(_type, _upgradeID)))
            {
                Wallet.instance.Pay(UpgradeCost(_type, _upgradeID));
                upgradeLevels[_upgradeID] += 1;
            }
            UpdateUpgradeUI(_type, _upgradeID);
        }
    
    }

    public void CheckMeditationLevel(int _meditationlevel)
    {
        switch(_meditationlevel)
        {
            case 5:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage1;
                break;
            case 10:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage2;
                break;
            case 15:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage3;
                break;
            case 20:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage4;
                break;
            case 25:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage5;
                break;
        }
    }

    public void CheckWorkoutLevel(int _workoutLevel)
    {
        switch (_workoutLevel)
        {
            case 1:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite1;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite1;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite1;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite1;
                break;
            case 2:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite2;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite2;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite2;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite2;
                break;
            /*case 15:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage3;
                break;
            case 20:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage4;
                break;
            case 25:
                Controller.instance.eyeSourceImage.sprite = Controller.instance.eyeStage5;
                break;*/
        }
    }


}
