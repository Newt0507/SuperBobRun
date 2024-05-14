using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private MainMenuContainer _mainMenuContainer;
    [SerializeField] private GameSettingsUI _gameSettingsUI;
    [SerializeField] private Image _fadeImageUI;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    public MainMenuContainer GetMainMenuContainer()
    {
        return _mainMenuContainer;
    }
    
    
    public GameSettingsUI GetGameSettingsUI()
    {
        return _gameSettingsUI;
    }

    public void FadeImageUI(Action action)
    {
        var endValue = _fadeImageUI.color.a == 0 ? 1 : 0;
        _fadeImageUI.DOFade(endValue, 1f).SetUpdate(true).OnComplete(() => action());
    }
    
}
