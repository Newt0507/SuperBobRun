using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            Vector3 cameraPosition = transform.position;
            //cameraPosition.x = MathF.Max(_player.position.x, cameraPosition.x);
            cameraPosition.x = _player.position.x;
            transform.position = cameraPosition;
        }
    }
}
