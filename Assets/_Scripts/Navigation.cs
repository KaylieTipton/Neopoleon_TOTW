using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject clickUpgradeSelected;
    public GameObject productionUpgradeSelected;
    public GameObject chronostasisUpgradeSelected;

    public Button chronostasisButton;

    public TMP_Text clickUpgradeTitleText;
    public TMP_Text productionUpgradeTitleText;
    public TMP_Text chronostasisUpgradeTitleText;

    public GameObject homeScreen;
    public GameObject settingsScreen;
    //PrestigeManager prestige;

    private void Start()
    {
        //PrestigeManager prestige = PrestigeManager.instance;
    }

    public void SwitchUpgrades(string location)
    {
        UpgradesManager.instance.upgradeHandlers[0].upgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.upgradeHandlers[1].upgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.upgradeHandlers[2].upgradeScroll.gameObject.SetActive(false);
        clickUpgradeSelected.GetComponent<Image>().color = Color.gray;
        productionUpgradeSelected.GetComponent<Image>().color = Color.gray;

        clickUpgradeTitleText.color = Color.gray;
        productionUpgradeTitleText.color = Color.gray;

        if(chronostasisButton.interactable)
        {
            chronostasisUpgradeSelected.GetComponent<Image>().color = Color.gray;
            chronostasisUpgradeTitleText.color = Color.gray;
        }
        



        switch (location)
        {
            case "Click":
                clickUpgradeSelected.GetComponent<Image>().color = Color.white;
                clickUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.upgradeHandlers[0].upgradeScroll.gameObject.SetActive(true);
                break;
            case "Production":
                productionUpgradeSelected.GetComponent<Image>().color = Color.white;
                productionUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.upgradeHandlers[1].upgradeScroll.gameObject.SetActive(true);
                break;
            case "Chronostasis":
                if(chronostasisButton.interactable)
                {
                    chronostasisUpgradeSelected.GetComponent<Image>().color = Color.white;
                    chronostasisUpgradeTitleText.color = Color.white;
                    UpgradesManager.instance.upgradeHandlers[2].upgradeScroll.gameObject.SetActive(true);
                }
                break;

        }
    }

    public void Navigate(string location)
    {
        homeScreen.SetActive(false);
        settingsScreen.SetActive(false);

        switch (location)
        {
            case "Home":
                homeScreen.SetActive(true);
                break;
            case "Settings":
                settingsScreen.SetActive(true);
                break;


        }
    }

    private void Update()
    {
        if (PrestigeManager.instance.prestigeCount >= 1)
            chronostasisButton.interactable = true;
    }

}
