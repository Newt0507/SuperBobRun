using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSliderUI : MonoBehaviour
{
    public event EventHandler<OnVolumeChangedEventArgs> OnMusicVolumeChanged;
    public event EventHandler<OnVolumeChangedEventArgs> OnSFXVolumeChanged;
    public class OnVolumeChangedEventArgs : EventArgs
    {
        public float volume;
    }

    [Header("Music & SFX")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Space]
    [Header("Vibration")]
    [SerializeField] private Button _vibrationButton;
    [SerializeField] private Image _onBackground, _handle;
    [SerializeField] private Sprite _onHandle, _offHandle;

    [Space]
    [Header("OK")]
    [SerializeField] private Button _OKButton;


    private const int LEFT_POSITION_X = -60;
    private const int RIGHT_POSITION_X = 60;

    private bool _isVibrationMuted;

    private void Awake()
    {

        _musicSlider.onValueChanged.AddListener((volume) => OnMusicChanged(volume));

        _sfxSlider.onValueChanged.AddListener((volume) => OnSFXChanged(volume));

        _vibrationButton.onClick.AddListener(() => OnVibrationChanged());

        _OKButton.onClick.AddListener(() => OnOKButtonClick());
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //SoundVolume
        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        //Vibration
        _isVibrationMuted = AudioManager.Instance.IsVibrationMuted();
        _onBackground.fillAmount = _isVibrationMuted ? 0 : 1;
        _handle.sprite = _isVibrationMuted ? _offHandle : _onHandle;
        _handle.rectTransform.anchoredPosition = _isVibrationMuted ? new Vector2(LEFT_POSITION_X, _handle.rectTransform.anchoredPosition.y)
            : new Vector2(RIGHT_POSITION_X, _handle.rectTransform.anchoredPosition.y);
    }

    private void OnMusicChanged(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
        OnMusicVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs()
        {
            volume = volume
        });
    }
    
    private void OnSFXChanged(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
        OnSFXVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs()
        {
            volume = volume
        });
    }

    private void OnVibrationChanged()
    {
        float duration = 0.2f;
        if (_isVibrationMuted)
        {
            //unmute
            _onBackground.DOFillAmount(1, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handle.rectTransform.DOAnchorPosX(RIGHT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() => _handle.sprite = _onHandle);
        }
        else
        {
            //mute
            _onBackground.DOFillAmount(0, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handle.rectTransform.DOAnchorPosX(LEFT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() => _handle.sprite = _offHandle);
        }

        AudioManager.Instance.ToggleVibration();
        _isVibrationMuted = AudioManager.Instance.IsVibrationMuted();
    }

    private void OnOKButtonClick()
    {
        AudioManager.Instance.PlaySFX(ESound.Click);
        //gameObject.SetActive(false);
    }

}
