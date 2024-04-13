using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject clickUpgradeSelected;
    public GameObject productionUpgradeSelected;

    public TMP_Text clickUpgradeTitleText;
    public TMP_Text productionUpgradeTitleText;

    public GameObject homeScreen;
    public GameObject settingsScreen;

    public void SwitchUpgrades(string location)
    {
        UpgradesManager.instance.clickUpgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.productionUpgradeScroll.gameObject.SetActive(false);
        clickUpgradeSelected.GetComponent<Image>().color = Color.gray;
        productionUpgradeSelected.GetComponent<Image>().color = Color.gray;

        clickUpgradeTitleText.color = Color.gray;
        productionUpgradeTitleText.color = Color.gray;

        switch (location)
        {
            case "Click":
                clickUpgradeSelected.GetComponent<Image>().color = Color.white;
                clickUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.clickUpgradeScroll.gameObject.SetActive(true);
                break;
            case "Production":
                productionUpgradeSelected.GetComponent<Image>().color = Color.white;
                productionUpgradeTitleText.color = Color.white;
                UpgradesManager.instance.productionUpgradeScroll.gameObject.SetActive(true);
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

}
