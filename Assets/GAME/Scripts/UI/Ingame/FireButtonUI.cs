using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (_player != null)
            _timerText.text = _player.GetCostFireValue().ToString("N0");
    }

    private void Update()
    {
        if (_player != null)
        {
            if(Data.GetCoin() < _player.GetCostFireValue())
            {
                _timerText.color = Color.red;
            }
            else
            {
                _timerText.color = Color.white;
            }
        }
    }
}