using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PrestigeManager : MonoBehaviour
{ 
    public static PrestigeManager instance;

    public void Awake() => instance = this;

    public TMP_Text prestigeCurrencyText;
    public TMP_Text popupText;
    public GameObject PrestigeInfoBox;
    public int prestigeCount;
    
    public double PrestigeGains()
    {
        
        return Math.Sqrt(UpgradesManager.instance.FindTotalUpgrades());
        
    }

    public void Update()
    {
        prestigeCurrencyText.text = $"{Controller.instance.data.timeShard:F2} Time Shards";
    }

    public void TogglePrestigeInfoBox()
    {
        PrestigeInfoBox.SetActive(!PrestigeInfoBox.activeSelf);
        Controller.instance.data.timeShard += PrestigeGains();
        popupText.text = $"You have gained {Controller.instance.data.timeShard:F2} Time Shards and Time has reversed.";
       
    }

    public void Prestige()
    {
        var data = Controller.instance.data;
        

        data.coins = 0;
        Wallet.instance.ResetCoin();

        data.clickUpgradeLevel = new int[3].ToList();
        data.productionUpgradeLevel = new int[3].ToList();

        UpgradesManager.instance.UpdateUpgradeUI("click");
        UpgradesManager.instance.UpdateUpgradeUI("production");

        
        Controller.instance.prestigeTimer.StartTimer(Controller.instance.prestigeTimer.countDownTime, Controller.instance.prestigeTimer.autoRestart);

        prestigeCount++;

        TogglePrestigeInfoBox();

    }

    public bool CheckChronoUnlock()
    {
        if (prestigeCount >= 1)
            return true;
        return false;
    }

}
