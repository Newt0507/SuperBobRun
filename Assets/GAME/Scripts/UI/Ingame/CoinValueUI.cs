using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinValueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private void Update()
    {
        _timerText.text = Data.GetCoin().ToString("N0");
    }
}
