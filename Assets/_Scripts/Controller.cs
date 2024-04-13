using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{

    public int coins = 0;
    public TMP_Text coinsText;


    void Update()
    {
        coinsText.text = "$" + coins;
    }

    public void GenerateCoins()
    {
        coins += 1;
    }
}
