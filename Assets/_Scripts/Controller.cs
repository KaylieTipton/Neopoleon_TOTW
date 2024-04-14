using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperPupSystems.Manager;
using SuperPupSystems.Helper;
using System;

public class Controller : MonoBehaviour
{

    public static Controller instance;
    public void Awake() => instance = this;

    public Data data;

    //Neo's Timer
    public Timer spriteTimer;

    public TMP_Text coinsText;
    public TMP_Text perSecondText;

    //Neo's Sprites
    public Sprite currentIdleSprite;
    public Sprite currentLeftPunch;
    public Sprite currentRightPunch;

    public Image sourceImage;
    public Sprite idleSprite0;
    public Sprite rightPunchSprite0;
    public Sprite leftPunchSprite0;

    public Sprite idleSprite1;
    public Sprite leftPunchSprite1;
    public Sprite rightPunchSprite1;

    public Sprite idleSprite2;
    public Sprite leftPunchSprite2;
    public Sprite rightPunchSprite2;

    public Sprite idleSprite3;
    public Sprite leftPunchSprite3;
    public Sprite rightPunchSprite3;

    public Sprite idleSprite4;
    public Sprite leftPunchSprite4;
    public Sprite rightPunchSprite4;

    //Eye Sprites
    public Image eyeSourceImage;
    public Sprite eyeStage1;
    public Sprite eyeStage2;
    public Sprite eyeStage3;
    public Sprite eyeStage4;
    public Sprite eyeStage5;

    private bool isRightPunch = true;

    //Prestige Timer
    public Slider prestigeBar;
    public Timer prestigeTimer;
    
    public double ClickPower()
    {
        double total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.upgradeHandlers[0].upgradesBasePower[i] * data.clickUpgradeLevel[i];
        }

        return total;
    }

    public double CoinsPerSecond()
    {
        double total = 0;
        for( int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.upgradeHandlers[1].upgradesBasePower[i] * data.productionUpgradeLevel[i];
        }
        return total;
    }

    public double Chronostasis()
    {
        double total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.upgradeHandlers[2].upgradesBasePower[i] * data.chronostasisUpgradeLevel[i];
        }
        return total;
    }

    void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
        
        currentIdleSprite = idleSprite0;
        currentLeftPunch = leftPunchSprite0;
        currentRightPunch = rightPunchSprite0;

        sourceImage.sprite = currentIdleSprite;

        SetSliderProgress();
        SetMaxPrestigeProgress();

        prestigeTimer.StartTimer(prestigeTimer.countDownTime, prestigeTimer.autoRestart);
    }

    void Update()
    {

        coinsText.text = $"{Wallet.instance.Coin:F2} Power";
        perSecondText.text = $"{CoinsPerSecond():F2}/s";

        data.coins = CoinsPerSecond() * Time.deltaTime;
        Wallet.instance.Earn(data.coins);

        SetSliderProgress();
    }

    public void SetSourceImage()
    {
        sourceImage.sprite = currentIdleSprite;
    }
    public void GenerateCoins()
    {
        spriteTimer.StartTimer(spriteTimer.countDownTime, spriteTimer.autoRestart);
        Wallet.instance.Earn(ClickPower());
        if(isRightPunch)
        {
            sourceImage.sprite = currentRightPunch;
            isRightPunch = false;
        }
        else
        {
            sourceImage.sprite = currentLeftPunch;
            isRightPunch = true; 
        }

    }

    public void SetSliderProgress()
    {
        prestigeBar.value = prestigeTimer.countDownTime - prestigeTimer.timeLeft;
    }

    public void SetMaxPrestigeProgress()
    {
        prestigeBar.maxValue = prestigeTimer.countDownTime;
        prestigeBar.value = prestigeTimer.timeLeft;
    }

}
