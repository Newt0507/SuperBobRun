using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Reward
{
    private void OnEnable()
    {
        int coinValue = 50;
        Data.SetCoin(coinValue);

        StartCoroutine(Aminate());
    }
}
