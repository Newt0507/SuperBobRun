using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementBackgroundUI : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    
    private RawImage _image;
    
    private void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + _offset * Time.deltaTime, _image.uvRect.size);
    }
}
