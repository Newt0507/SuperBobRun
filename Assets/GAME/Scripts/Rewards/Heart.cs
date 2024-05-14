using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Reward
{
    private bool _isFirstEnable = true;
    private void OnEnable()
    {
        if (_isFirstEnable)
        {
            _isFirstEnable = false;
            return;
        }

        StartCoroutine(Aminate());
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.SetHealth(player.GetHealth());
    }
}
