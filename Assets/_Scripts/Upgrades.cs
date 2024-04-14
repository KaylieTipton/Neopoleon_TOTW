using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int upgradeID;
    public Image upgradeButton;
    public TMP_Text levelText;
    public TMP_Text nameText;
    public TMP_Text costText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade("click", upgradeID);
    public void BuyProductionUpgrade() => UpgradesManager.instance.BuyUpgrade("production", upgradeID);
    public void BuyChronostasisUpgrade() => UpgradesManager.instance.BuyUpgrade("chronostasis", upgradeID);
}
