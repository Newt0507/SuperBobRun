using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Win
            GameManager.Instance._isVictory = true;
            Player.Instance._playerControls.Disable();
            Player.Instance._direction = 0;
        }
    }
}
