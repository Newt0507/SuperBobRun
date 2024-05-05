using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            //_selectLevelPopupUI.gameObject.SetActive(true);
        });

        _settingButton.onClick.AddListener(() =>
        {
            //_settingsPopupUI.gameObject.SetActive(true);
        });

        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
            Debug.Log("Quitting...");
        });
    }
}
