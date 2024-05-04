using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private const string COIN = "Coin";
    private const string SOUND = "Sound";
    private const string VIBRATION = "Vibration";

    public static int GetCoin()
    {
        return PlayerPrefs.GetInt(COIN, 0);
    }

    public static void SetCoin(int coinValue)
    {
        var _coin = GetCoin();

        _coin += coinValue;

        if (_coin <= 0)
            _coin = 0;

        PlayerPrefs.SetInt(COIN, _coin);
        PlayerPrefs.Save();
    }

    public static bool IsSoundMuted()
    {
        return PlayerPrefs.GetInt(SOUND, 0) != 0;
    }

    public static void SetSound(bool mute)
    {
        PlayerPrefs.SetInt(SOUND, mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool IsVibrationMuted()
    {
        return PlayerPrefs.GetInt(VIBRATION, 0) != 0;
    }

    public static void SetVibration(bool mute)
    {
        PlayerPrefs.SetInt(VIBRATION, mute ? 1 : 0);
        PlayerPrefs.Save();
    }
}
