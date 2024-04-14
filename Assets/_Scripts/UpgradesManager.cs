using SuperPupSystems.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this;

    public UpgradeHandler[] upgradeHandlers;

    bool medCheck = false;
    bool workCheck = false;
    bool chronoCheck = false;

    public GameObject WinScreen;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 3);
        Methods.UpgradeCheck(Controller.instance.data.productionUpgradeLevel, 3);
        Methods.UpgradeCheck(Controller.instance.data.chronostasisUpgradeLevel, 3);

        upgradeHandlers[0].upgradeName = new string[] 
        { 
            "+1 WP Per Click", 
            "+5 WP Per Click", 
            "+10 WP Per Click" 
        };
        upgradeHandlers[1].upgradeName = new string[] 
        {
            "+1 Meditation/s",
            "+2 Meditation/s",
            "+10 Meditation/s"
        };
        upgradeHandlers[2].upgradeName = new string[]
        {
            "+30 Sec Slowdown to Meteor",
            "+60 Sec Slowdown to Meteor",
            "+90 Sec Slowdown to Meteor"
        };

        upgradeHandlers[0].upgradeTitle = "Workout Power";
        upgradeHandlers[1].upgradeTitle = "Meditation Power";
        upgradeHandlers[2].upgradeTitle = "Chronostasis";

        //Workout
        upgradeHandlers[0].upgradeBaseCost = new double[] { 10, 50, 100 };
        upgradeHandlers[0].upgradeCostMult = new double[] { 1.25, 1.35, 1.55 };
        upgradeHandlers[0].upgradesBasePower = new double[] { 1, 5, 10 };

        //Meditation
        upgradeHandlers[1].upgradeBaseCost = new double[] { 25, 100, 1000 };
        upgradeHandlers[1].upgradeCostMult = new double[] { 1.5, 1.75, 2 };
        upgradeHandlers[1].upgradesBasePower = new double[] { 1, 2, 10 };

        //Chronostasis
        upgradeHandlers[2].upgradeBaseCost = new double[] { 1, 10, 25 };
        upgradeHandlers[2].upgradeCostMult = new double[] { 1.5, 1.75, 2 };
        upgradeHandlers[2].upgradesBasePower = new double[] { 30, 60, 90 };

        CreateUpgrades(Controller.instance.data.clickUpgradeLevel, 0);
        CreateUpgrades(Controller.instance.data.productionUpgradeLevel, 1);
        CreateUpgrades(Controller.instance.data.chronostasisUpgradeLevel, 2);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
        UpdateUpgradeUI("chronostasis");

    }

    void CreateUpgrades<T>(List<T> _level, int _index)
    {
        for (int i = 0; i < _level.Count; i++)
        {
            Upgrades upgrade = Instantiate(upgradeHandlers[_index].upgradePrefab, upgradeHandlers[_index].upgradesPanel.transform);
            upgrade.upgradeID = i;
            upgradeHandlers[_index].upgrades.Add(upgrade);
        }
        upgradeHandlers[_index].upgradeScroll.normalizedPosition = new Vector2(0, 0);
    }

    public void UpdateUpgradeUI(string _type, int _upgradeID = -1)
    {
        var data = Controller.instance.data;
        int totalWork = 0;
        int totalMed = 0;
        int totalChrono = 0;
        switch (_type)
        {
            case "click":
                UpdateAllUI(upgradeHandlers[0].upgrades, data.clickUpgradeLevel, upgradeHandlers[0].upgradeName, upgradeHandlers[0].upgradeTitle, 0);
                for (int i = 0; i < upgradeHandlers[0].upgrades.Count; i++)
                {
                    totalWork += data.clickUpgradeLevel[i];
                }
                CheckWorkoutLevel(totalWork);
                break;
            case "production":
                UpdateAllUI(upgradeHandlers[1].upgrades, data.productionUpgradeLevel, upgradeHandlers[1].upgradeName, upgradeHandlers[1].upgradeTitle, 1);
                for (int i = 0; i < upgradeHandlers[1].upgrades.Count; i++)
                {
                    totalMed += data.productionUpgradeLevel[i];
                }
                CheckMeditationLevel(totalMed);
                break;
            case "chronostasis":
                UpdateAllUI(upgradeHandlers[2].upgrades, data.chronostasisUpgradeLevel, upgradeHandlers[2].upgradeName, upgradeHandlers[2].upgradeTitle, 2);
                for (int i = 0; i < upgradeHandlers[2].upgrades.Count; i++)
                {
                    totalChrono += data.chronostasisUpgradeLevel[i];
                }
                //CheckChronoLevel(totalChrono);
                break;
        }

        void UpdateAllUI(List<Upgrades> _upgrades, List<int> upgradeLevels, string[] upgradeNames, string upgradeTitle, int _index)
        {
            if (_upgradeID == -1)
                for (int i = 0; i < upgradeHandlers[_index].upgrades.Count; i++)
                {
                    UpdateUI(i);
                }
            else UpdateUI(_upgradeID);
            

            void UpdateUI(int _ID)
            {
                _upgrades[_ID].levelText.text = upgradeLevels[_ID] + " " + upgradeTitle;
                if(_index == 2)
                    _upgrades[_ID].costText.text = $"Cost: {UpgradeCost(_type, _ID):F2} Time Shards";
                else
                    _upgrades[_ID].costText.text = $"Cost: {UpgradeCost(_type, _ID):F2} Power";
                _upgrades[_ID].nameText.text = upgradeNames[_ID];
            }
            
        }
        
    }

    public double UpgradeCost(string _type, int _upgradeID)
    {
        var data = Controller.instance.data;
        switch (_type)
        {
            case "click":
                return UpgradeCost(0, data.clickUpgradeLevel, _upgradeID);
            case "production":
                return UpgradeCost(1, data.productionUpgradeLevel, _upgradeID);
            case "chronostasis":
                return UpgradeCost(2, data.chronostasisUpgradeLevel, _upgradeID);

        }

        return 0;   
    }

    private double UpgradeCost(int _index, List<int> _levels, int _upgradeID)
    {
        return upgradeHandlers[_index].upgradeBaseCost[_upgradeID] * Math.Pow(upgradeHandlers[_index].upgradeCostMult[_upgradeID], _levels[_upgradeID]);
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
            case "chronostasis": BuyTimeShard(data.chronostasisUpgradeLevel);
                Controller.instance.prestigeTimer.countDownTime = (float)Controller.instance.Chronostasis() + Controller.instance.prestigeTimer.timeLeft;
                Controller.instance.prestigeTimer.AddTime((float)Controller.instance.Chronostasis());
                Controller.instance.SetMaxPrestigeProgress();
                break;
        
        
        }
        
        void BuyTimeShard(List<int> upgradeLevels)
        {
            if(data.timeShard >= UpgradeCost(_type, _upgradeID))
            {
                data.timeShard -= UpgradeCost(_type, _upgradeID);
                data.chronostasisUpgradeLevel[_upgradeID] += 1;
            }
            UpdateUpgradeUI(_type, _upgradeID);
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
            case 30:
                medCheck = true;
                break;
        }
    }

    public void CheckWorkoutLevel(int _workoutLevel)
    {
        switch (_workoutLevel)
        {
            case 7:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite1;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite1;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite1;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite1;
                break;
            case 17:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite2;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite2;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite2;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite2;
                break;
            case 21:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite3;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite3;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite3;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite3;
                break;
            case 28:
                Controller.instance.currentIdleSprite = Controller.instance.idleSprite4;
                Controller.instance.currentLeftPunch = Controller.instance.leftPunchSprite4;
                Controller.instance.currentRightPunch = Controller.instance.rightPunchSprite4;
                Controller.instance.sourceImage.sprite = Controller.instance.idleSprite4;
                break;
            case 30:
                workCheck = true;
                break;
        }
    }

    public void CheckChronoLevel(int _chronoLevel)
    {
        if (_chronoLevel == 3)
            chronoCheck = true;
    }

    private void Win()
    {
        if(medCheck && workCheck && chronoCheck)
        {
            WinScreen.SetActive(true);
        }
    }
    public int FindTotalUpgrades()
    {
        var data = Controller.instance.data;
        int total = 0;
        for (int i = 0; i < upgradeHandlers[0].upgrades.Count; i++)
        {
            total += data.clickUpgradeLevel[i];
        }
        for (int i = 0; i < upgradeHandlers[1].upgrades.Count; i++)
        {
            total += data.clickUpgradeLevel[i];
        }
        for (int i = 0; i < upgradeHandlers[2].upgrades.Count; i++)
        {
            total += data.clickUpgradeLevel[i];
        }

        return total;
    }


}
