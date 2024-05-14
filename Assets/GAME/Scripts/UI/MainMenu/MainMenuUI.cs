using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Logo")]
    [SerializeField] private RectTransform _logo;

    [Header("Button")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _quitButton;

    private const int LOGO_ANCHORED_POSITION_Y = 1000;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            UIManager.Instance.GetMainMenuContainer().GetSelectCharacterUI().gameObject.SetActive(true);
        });

        _settingButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            UIManager.Instance.GetGameSettingsUI().Show();
        });

        _quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);
            Application.Quit();
            Debug.Log("Quitting...");
        });


        Setup();
    }

    private void OnEnable()
    {
        var duration = 1f;
        _logo.DOAnchorPosY(-110, duration).SetEase(Ease.OutBack)
            .OnComplete(() => StartCoroutine(IEShowButton()));
    }

    private void OnDisable()
    {
        Setup();
    }

    private IEnumerator IEShowButton()
    {
        _playButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);

        _settingButton.gameObject.SetActive(true);
        _settingButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);

        _quitButton.gameObject.SetActive(true);
    }

    private void Setup()
    {
        _logo.anchoredPosition = new Vector2(0, LOGO_ANCHORED_POSITION_Y);

        _playButton.gameObject.SetActive(false);
        _settingButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
    }

}
