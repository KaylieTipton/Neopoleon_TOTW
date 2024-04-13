using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Data
{

    public double coins;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;
    public Data()
    {
        clickUpgradeLevel = new int[3].ToList();
        productionUpgradeLevel = new int[3].ToList();

    }
}
