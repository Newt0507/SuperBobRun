using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private RectTransform _selectLevelPopupUI;

    private void Awake()
    {
        _selectLevelPopupUI.gameObject.SetActive(false);

        _playButton.onClick.AddListener(() =>
        {
            _selectLevelPopupUI.gameObject.SetActive(true);
        });

        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
            Debug.Log("Quitting...");
        });
    }
}
