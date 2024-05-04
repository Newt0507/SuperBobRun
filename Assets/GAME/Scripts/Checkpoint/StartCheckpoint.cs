using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheckpoint : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Awake()
    {
        Instantiate(_player, Vector2.up * 3, Quaternion.identity);
    }
}
