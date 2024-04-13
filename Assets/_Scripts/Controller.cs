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

    public Timer spriteTimer;
    public float time;

    public TMP_Text coinsText;
    public TMP_Text perSecondText;

    public Sprite currentIdleSprite;
    public Sprite currentLeftPunch;
    public Sprite currentRightPunch;

    public Image sourceImage;
    public Sprite idleSprite0;
    public Sprite rightPunchSprite0;
    public Sprite leftPunchSprite0;

    /*public Sprite idleSprite1;
    public Sprite leftPunchSprite1;
    public Sprite rightPunchSprite1;*/

    public Image eyeSourceImage;
    public Sprite eyeStage1;
    public Sprite eyeStage2;
    public Sprite eyeStage3;
    public Sprite eyeStage4;
    public Sprite eyeStage5;

    private bool isRightPunch = true;
    
    public double ClickPower()
    {
        double total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }

        return total;
    }

    public double coinsPerSecond()
    {
        double total = 0;
        for( int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
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
    }

    void Update()
    {

        coinsText.text = $"{Wallet.instance.Coin:F2} Power";
        perSecondText.text = $"{coinsPerSecond():F2}/s";

        data.coins = coinsPerSecond() * Time.deltaTime;
        Wallet.instance.Earn(data.coins);
    }

    public void SetSourceImage()
    {
        sourceImage.sprite = currentIdleSprite;
    }
    public void GenerateCoins()
    {
        spriteTimer.StartTimer(time, spriteTimer.autoRestart);
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
}
