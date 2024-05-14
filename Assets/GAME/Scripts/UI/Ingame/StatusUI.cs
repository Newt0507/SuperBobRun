using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [Header("Status Text")]
    [SerializeField] private TextMeshProUGUI _statusText;

    [Header("Button")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _settingsButton;
    //[SerializeField] private Button[] _homeButton;

    [Header("Panel")]
    [SerializeField] private RectTransform _statusPanel;
    [SerializeField] private RectTransform _congratsUI;

    private const int PANEL_ANCHORED_POSITION_Y = 1000;

    private float _duration = 1f;
    private bool _firstEnable = true;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            _statusPanel.DOAnchorPosY(PANEL_ANCHORED_POSITION_Y, _duration / 2).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() => gameObject.SetActive(false));
       });
        
        _nextLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            if (int.TryParse(SceneManager.GetActiveScene().name, out int currentScene))
            {
                if (currentScene < 10)
                {
                    //ObjectPoolManager.Instance.ReturnAllPool();
                    UIManager.Instance.FadeImageUI(() => SceneManager.LoadScene($"{currentScene + 1}"));
                }
            }
        });
        
        _replayButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            string currentScene = SceneManager.GetActiveScene().name;
            AudioManager.Instance.StopMusic();
            UIManager.Instance.FadeImageUI(() => SceneManager.LoadScene(currentScene));
            //ObjectPoolManager.Instance.ReturnAllPool();
            //SceneManager.LoadScene(currentScene.name);
        });

        _settingsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            UIManager.Instance.GetGameSettingsUI().Show();
        });

        //_homeButton.onClick.AddListener(() =>
        //{
        //    AudioManager.Instance.PlaySFX(ESound.Click);
        //    AudioManager.Instance.StopMusic();
        //    UIManager.Instance.FadeImageUI(() =>
        //    {
        //        UIManager.Instance.GetMainMenuContainer().gameObject.SetActive(true);
        //        AudioManager.Instance.PlayMusic(ESound.MainMenu);
        //        //ObjectPoolManager.Instance.ReturnAllPool();
        //        SceneManager.LoadScene("MainMenu");
        //    });
        //});

        _nextLevelButton.gameObject.SetActive(false);

        SetUp();
    }

    public void HomeOnClick()
    {
        AudioManager.Instance.PlaySFX(ESound.Click);
        AudioManager.Instance.StopMusic();
        UIManager.Instance.FadeImageUI(() =>
        {
            UIManager.Instance.GetMainMenuContainer().gameObject.SetActive(true);
            AudioManager.Instance.PlayMusic(ESound.MainMenu);
            //ObjectPoolManager.Instance.ReturnAllPool();
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameOverChanged += GameManager_OnGameOverChanged;
        GameManager.Instance.OnVictoryChanged += GameManager_OnOnVictoryChanged;
        
        Hide();
    }

    private void GameManager_OnGameOverChanged(object sender, EventArgs e)
    {
        _statusText.text = "GAME OVER";
        _resumeButton.gameObject.SetActive(false);
        _nextLevelButton.gameObject.SetActive(false);
        Show();
    }

    private void GameManager_OnOnVictoryChanged(object sender, EventArgs e)
    {
        _statusText.text = "VICTORY";
        _nextLevelButton.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(false);
        if (int.Parse(SceneManager.GetActiveScene().name) == 10)
            _congratsUI.DOAnchorPosY(0, _duration).SetEase(Ease.OutBack).SetUpdate(true); //_congratsUI.gameObject.SetActive(true);
        
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        if(_firstEnable)
        {
            _firstEnable = false;
            return;
        }

        _statusPanel.DOAnchorPosY(0, _duration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void SetUp()
    {
        _statusPanel.anchoredPosition = new Vector2(0, PANEL_ANCHORED_POSITION_Y);
        _congratsUI.anchoredPosition = new Vector2(0, PANEL_ANCHORED_POSITION_Y);
    }
}
