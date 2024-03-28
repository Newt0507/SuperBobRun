using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _homeButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        });
        
        _nextLevelButton.onClick.AddListener(() =>
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene($"{int.Parse(currentScene.name) + 1}");
        });
        
        _replayButton.onClick.AddListener(() =>
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        });
        
        _homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });

        _nextLevelButton.gameObject.SetActive(false);
       
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
    }
    
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    
}
