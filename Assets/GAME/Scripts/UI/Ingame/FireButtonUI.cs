using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costValueText;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _costValueText.text = _player.GetCostFireValue().ToString("N0");
    }

    private void Update()
    {
        if (Data.GetCoin() < _player.GetCostFireValue())
            _costValueText.color = Color.red;
        else
            _costValueText.color = Color.white;
    }
}
