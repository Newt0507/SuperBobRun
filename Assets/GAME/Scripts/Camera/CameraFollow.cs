using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
        
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }


    private void LateUpdate()
    {
        if (_player == null) return;

        Vector3 cameraPosition = transform.position;
        cameraPosition.x = _player.transform.position.x;
        transform.position = cameraPosition;
    }
}
