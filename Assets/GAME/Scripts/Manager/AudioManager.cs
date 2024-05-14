using DG.Tweening;
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

    private void Start()
    {
        PlayMusic(ESound.MainMenu);
    }

    #region Music
    public void PlayMusic(ESound sound)
    {
        bool soundFound = false;
        foreach (var musicSound in _musicSounds.soundList)
        {
            if (sound == musicSound.sound)
            {
                soundFound = true;
                _musicSource.clip = musicSound.clip;
                _musicSource.Play();
                break;
            }
        }

        if (!soundFound)
            Debug.LogError("Sound " + sound + "does not found!");
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
        bool soundFound = false;
        foreach (var sfxSound in _sfxSounds.soundList)
        {
            if (sound == sfxSound.sound)
            {
                soundFound = true;
                _sfxSource.PlayOneShot(sfxSound.clip);
                break;
            }
        }

        if (!soundFound)
            Debug.LogError("Sound " + sound + "does not found!");
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
