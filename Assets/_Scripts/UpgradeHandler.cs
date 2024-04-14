using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public List<Upgrades> upgrades;
    public Upgrades upgradePrefab;

    public ScrollRect upgradeScroll;
    public GameObject upgradesPanel;

    public string[] upgradeName;
    public string upgradeTitle;

    public double[] upgradeBaseCost;
    public double[] upgradeCostMult;
    public double[] upgradesBasePower;

}
