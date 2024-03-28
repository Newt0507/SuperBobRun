using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _statusUI;


    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            _statusUI.SetActive(true);
        });
    }
}
