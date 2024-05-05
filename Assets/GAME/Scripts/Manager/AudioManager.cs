using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundSO _musicSounds, _sfxSounds;
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    private bool _isVibrationMuted;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _musicSource.volume = Data.GetMusicVolume();
        _sfxSource.volume = Data.GetSFXVolume();
        _isVibrationMuted = Data.IsVibrationMuted();
    }

    #region Music
    public void PlayMusic(ESound sound)
    {
        foreach (var musicSound in _musicSounds.soundList)
        {
            if (sound == musicSound.sound)
            {
                _musicSource.clip = musicSound.clip;
                _musicSource.Play();
                break;
            }
        }
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public float GetMusicVolume()
    {
        return _musicSource.volume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
        Data.SetMusicVolume(volume);
    }
    #endregion

    #region SFX
    public void PlaySFX(ESound sound)
    {
        foreach (var sfxSound in _sfxSounds.soundList)
        {
            if (sound == sfxSound.sound)
            {
                _sfxSource.PlayOneShot(sfxSound.clip);
                break;
            }
        }
    }
    public float GetSFXVolume()
    {
        return _sfxSource.volume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
        Data.SetSFXVolume(volume);
    }

    #endregion

    #region Vibration
    public void PlayVibration()
    {
        if (_isVibrationMuted) Handheld.Vibrate();
    }    

    public bool IsVibrationMuted()
    {
        return _isVibrationMuted;
    }

    public void ToggleVibration()
    {
        _isVibrationMuted = !_isVibrationMuted;
        Data.SetVibration(_isVibrationMuted);
    }
    #endregion

    

}
