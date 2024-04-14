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
    public GameObject boom;
    public Animator popUp;
    
    public double PrestigeGains()
    {
        
        return Math.Sqrt(UpgradesManager.instance.FindTotalUpgrades() / (double)10);
        
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
    public void activateBoom()
    {
        boom.SetActive(true);
        Controller.instance.aSource.PlayOneShot(Controller.instance.planetExplode);
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

        Controller.instance.currentIdleSprite = Controller.instance.idleSprite0;
        Controller.instance.currentLeftPunch = Controller.instance.rightPunchSprite0;
        Controller.instance.currentRightPunch = Controller.instance.leftPunchSprite0;
        Controller.instance.eyeSourceImage.sprite = Controller.instance.defaultEye;

        Controller.instance.sourceImage.sprite = Controller.instance.idleSprite0;

        Controller.instance.prestigeTimer.StartTimer(Controller.instance.prestigeTimer.countDownTime, Controller.instance.prestigeTimer.autoRestart);

        prestigeCount++;

        UpgradesManager.instance.medCheck = false;
        UpgradesManager.instance.workCheck = false;
        UpgradesManager.instance.chronoCheck = false;

        //popUp.SetTrigger("Continue");
        TogglePrestigeInfoBox();
        Controller.instance.aSource.PlayOneShot(Controller.instance.timeReset);
        boom.SetActive(false);


    }

    public bool CheckChronoUnlock()
    {
        if (prestigeCount >= 1)
            return true;
        return false;
    }

}
