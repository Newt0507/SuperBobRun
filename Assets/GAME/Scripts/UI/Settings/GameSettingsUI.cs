using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    //public event EventHandler<OnVolumeChangedEventArgs> OnMusicVolumeChanged;
    //public event EventHandler<OnVolumeChangedEventArgs> OnSFXVolumeChanged;
    //public class OnVolumeChangedEventArgs : EventArgs
    //{
    //    public float volume;
    //}

    [Header("Music")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Image _musicIconImage;
    [SerializeField] private Sprite _onMusic, _offMusic;

    [Header("SFX")]
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Image _sfxIconImage;
    [SerializeField] private Sprite _onSFX, _offSFX;

    [Header("Vibration")]
    [SerializeField] private Button _vibrationButton;
    [SerializeField] private Image _onBackgroundImage, _handleImage;
    [SerializeField] private Sprite _onHandle, _offHandle;
    [SerializeField] private Image _vibrationIconImage;
    [SerializeField] private Sprite _onVibration, _offVibration;

    [Header("OK")]
    [SerializeField] private Button _okButton;

    [Header("UI")]
    [SerializeField] private RectTransform _uiRect;

    private CanvasGroup _canvas;
    private RectTransform _okButtonRect;
    private CanvasGroup _okButtonCanvas;

    private const int LEFT_POSITION_X = -60;
    private const int RIGHT_POSITION_X = 60;
    private const int UIRECT_ANCHORED_POSITION_Y = 2000;

    private bool _isVibrationMuted;

    private float _duration = .8f;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener((volume) => OnMusicChanged(volume));

        _sfxSlider.onValueChanged.AddListener((volume) => OnSFXChanged(volume));

        _vibrationButton.onClick.AddListener(() => OnVibrationChanged());

        _okButton.onClick.AddListener(() => OnOKButtonClick());

        _canvas = GetComponent<CanvasGroup>();
        _okButtonRect = _okButton.GetComponent<RectTransform>();
        _okButtonCanvas = _okButton.GetComponent<CanvasGroup>();
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
        _onBackgroundImage.fillAmount = _isVibrationMuted ? 0 : 1;
        _handleImage.sprite = _isVibrationMuted ? _offHandle : _onHandle;
        _handleImage.rectTransform.anchoredPosition = _isVibrationMuted ? new Vector2(LEFT_POSITION_X, _handleImage.rectTransform.anchoredPosition.y)
            : new Vector2(RIGHT_POSITION_X, _handleImage.rectTransform.anchoredPosition.y);

        //Canvas fades
        _canvas.alpha = 0;
        gameObject.SetActive(false);

        //UI rect
        _uiRect.anchoredPosition = new Vector2(0, UIRECT_ANCHORED_POSITION_Y);

        //OK button
        OKButtonSetup();
    }

    private void OnMusicChanged(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);

        if (volume <= 0)
            _musicIconImage.sprite = _offMusic;
        else
            _musicIconImage.sprite = _onMusic;


        //OnMusicVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs()
        //{
        //    volume = volume
        //});
    }
    
    private void OnSFXChanged(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);

        if (volume <= 0)
            _sfxIconImage.sprite = _offSFX;
        else
            _sfxIconImage.sprite = _onSFX;

        //OnSFXVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs()
        //{
        //    volume = volume
        //});
    }

    private void OnVibrationChanged()
    {
        AudioManager.Instance.PlaySFX(ESound.Click);

        float duration = 0.2f;
        if (_isVibrationMuted)
        {
            //unmute
            _onBackgroundImage.DOFillAmount(1, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handleImage.rectTransform.DOAnchorPosX(RIGHT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() =>
                {
                    _handleImage.sprite = _onHandle;
                    _vibrationIconImage.sprite = _onVibration;
                });
        }
        else
        {
            //mute
            _onBackgroundImage.DOFillAmount(0, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handleImage.rectTransform.DOAnchorPosX(LEFT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() => 
                {
                    _handleImage.sprite = _offHandle;
                    _vibrationIconImage.sprite = _offVibration;
                });
        }

        AudioManager.Instance.ToggleVibration();
        _isVibrationMuted = AudioManager.Instance.IsVibrationMuted();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.SetUpdate(true);

        sequence.Append(_canvas.DOFade(1, _duration))
            .Join(_uiRect.DOAnchorPosY(0, _duration).SetEase(Ease.OutBack));

        sequence.Append(_okButtonRect.DOScale(Vector3.one, _duration / 2).SetEase(Ease.OutBack))
            .Join(_okButtonCanvas.DOFade(1, _duration / 2));
    }

    private void OnOKButtonClick()
    {
        AudioManager.Instance.PlaySFX(ESound.Click);

        Sequence sequence = DOTween.Sequence();

        sequence.SetUpdate(true);

        sequence.Append(_uiRect.DOAnchorPosY(UIRECT_ANCHORED_POSITION_Y, _duration).SetEase(Ease.InBack))
            .Join(_canvas.DOFade(0, _duration));

        sequence.OnComplete(() =>
        {
            OKButtonSetup();
            gameObject.SetActive(false);
        });

    }

    private void OKButtonSetup()
    {
        _okButtonRect.localScale = Vector3.one * 0.5f;
        _okButtonCanvas.alpha = 0;
    }
}
