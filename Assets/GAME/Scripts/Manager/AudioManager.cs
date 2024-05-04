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

        _musicSource.mute = Data.IsSoundMuted();
        _sfxSource.mute = Data.IsSoundMuted();
        _isVibrationMuted = Data.IsVibrationMuted();
    }

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

    public void PlayVibration()
    {
        if (_isVibrationMuted) Handheld.Vibrate();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void ToggleSound()
    {
        var mute = !_musicSource.mute;

        _musicSource.mute = mute;
        _sfxSource.mute = mute;
        Data.SetSound(mute);
    }

    public void ToggleVibration()
    {
        _isVibrationMuted = !_isVibrationMuted;
        Data.SetVibration(_isVibrationMuted);
    }
}
