using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [HideInInspector] public bool _isVictory = false;
    [HideInInspector] public bool _isGameOver = false;
    
    public event EventHandler OnVictoryChanged;
    public event EventHandler OnGameOverChanged;

    [SerializeField] private float _playingTimerMax;
    
    private float _playingTimer;
    private int _coin;
    private bool _stopCountdown = false;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        _coin = Data.GetCoin();

        AudioManager.Instance.PlayMusic(ESound.Playing);
    }

    private void Start()
    {
        _playingTimer = _playingTimerMax;
    }

    private void Update()
    {
        if (!_stopCountdown)
        {
            _playingTimer -= Time.deltaTime;
            if (_playingTimer <= 0)
            {
                _isGameOver = true;
                _playingTimer = 0f;
                _stopCountdown = true;
            }
        }

        if (_isVictory)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySFX(ESound.NextStage);
            Invoke(nameof(Victory), 1f);
            _isVictory = false;
        }

        if (_isGameOver)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySFX(ESound.GameOver);
            Invoke(nameof(GameOver), 2f);
            _isGameOver = false;
        }
    }

    private void Victory()
    {
        OnVictoryChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameOver()
    {
        OnGameOverChanged?.Invoke(this, EventArgs.Empty);
    }        
    
    public int GetRemainTimer()
    {
        return (int)_playingTimer;
    }

    public float GetPlayingTimer()
    {
        return _playingTimer / _playingTimerMax;
    }

}
