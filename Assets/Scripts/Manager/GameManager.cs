using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string COIN = "Coin";
    
    [HideInInspector] public bool _isVictory = false;
    [HideInInspector] public bool _isGameOver = false;
    
    public event EventHandler OnVictoryChanged;
    public event EventHandler OnGameOverChanged;

    [SerializeField] private float _playingTimerMax;
    
    private float _playingTimer;
    private int _coin;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _coin = PlayerPrefs.GetInt(COIN);

    }

    private void Start()
    {
        _playingTimer = _playingTimerMax;
    }

    private void Update()
    {
        _playingTimer -= Time.deltaTime;
        if (_playingTimer < 0)
            _isGameOver = true;
        
        
        if (_isVictory)
        {
            Invoke(nameof(Victory), 1f);
            _isVictory = false;
        }

        if (_isGameOver)
        {
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

    public int GetCoin()
    {
        return _coin;
    }
        
    public void SetCoin(int coinValue)
    {
        _coin += coinValue;

        if (_coin <= 0)
            _coin = 0;

        PlayerPrefs.SetInt(COIN, _coin);
        PlayerPrefs.Save();
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
