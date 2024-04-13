using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public List<int> clickUpgradeLevel;
    public Data()
    {
        clickUpgradeLevel = Methods.CreateList<int>(capacity: 3);
    }
}
