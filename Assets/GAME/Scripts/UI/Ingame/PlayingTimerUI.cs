using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timerImage;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    private void Update()
    {
        if (GameManager.Instance.GetRemainTimer() <= 10)
        {
            _timerText.color = Color.red;
            _timerImage.color = Color.red;
            _anim.enabled = true;
        }
        
        _timerText.text = GameManager.Instance.GetRemainTimer().ToString();
        _timerImage.fillAmount = GameManager.Instance.GetPlayingTimer();
    }
}
