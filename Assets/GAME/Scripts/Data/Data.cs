using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private const string COIN = "Coin";
    private const string MUSIC_VOLUME = "Music_Volume";
    private const string SFX_VOLUME = "SFX_Volume";
    private const string VIBRATION = "Vibration";
    private const string CHARACTER = "Character";

    #region Character
    public static int GetCharacter()
    {
        return PlayerPrefs.GetInt(CHARACTER);
    }

    public static void SetCharacter(int value)
    {
        PlayerPrefs.SetInt(CHARACTER, value);
        PlayerPrefs.Save();
    }
    #endregion

    #region Coin
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
    #endregion

    #region Settings
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME, 1);
    }

    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME, 1);
    }

    public static void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME, volume);
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
    #endregion

}
