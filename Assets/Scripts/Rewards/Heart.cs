using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Reward
{
    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        _player.SetHealth(_player.GetHealth());

        StartCoroutine(Aminate());
    }
}
