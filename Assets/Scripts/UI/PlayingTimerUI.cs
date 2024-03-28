using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timerImage;

    private void Update()
    {
        _timerText.text = GameManager.Instance.GetRemainTimer().ToString();
        _timerImage.fillAmount = GameManager.Instance.GetPlayingTimer();
    }
}
