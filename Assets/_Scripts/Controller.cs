using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperPupSystems.Manager;
using SuperPupSystems.Helper;

public class Controller : MonoBehaviour
{

    public static Controller instance;
    public void Awake() => instance = this;
    

    public Data data;

    public Timer spriteTimer;
    public float time;

    public TMP_Text coinsText;

    public Image sourceImage;
    public Sprite idleSprite0;
    public Sprite rightPunchSprite0;
    public Sprite leftPunchSprite0;

    private bool isRightPunch = true;
    
    public int ClickPower()
    {
        return 1 + data.clickUpgradeLevel;
    }
    void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
        sourceImage.sprite = idleSprite0;
    }

    void Update()
    {
        coinsText.text = "$" + WalletManager.instance.Coin;
    }

    public void SetSourceImage()
    {
        sourceImage.sprite = idleSprite0;
    }
    public void GenerateCoins()
    {
        spriteTimer.StartTimer(time, spriteTimer.autoRestart);
        WalletManager.instance.Earn(ClickPower());
        if(isRightPunch)
        {
            sourceImage.sprite = rightPunchSprite0;
            isRightPunch = false;
        }
        else
        {
            sourceImage.sprite = leftPunchSprite0;
            isRightPunch = true; 
        }

    }
}
