using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Reward
{
    private void Start()
    {
        int coinValue = 50;
        GameManager.Instance.SetCoin(coinValue);

        StartCoroutine(Aminate());
    }
}
