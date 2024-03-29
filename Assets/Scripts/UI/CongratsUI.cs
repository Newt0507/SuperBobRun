using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CongratsUI : MonoBehaviour
{
    [SerializeField] private Button _homeButton;

    private void Awake()
    {
        _homeButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });
    }
}
