using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleMainMenuButtonUI : MonoBehaviour
{
    private RectTransform _rect;
    private CanvasGroup _canvas;
    private Button _button;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvas = GetComponent<CanvasGroup>();
        _button = GetComponent<Button>();

        gameObject.SetActive(false);
    }
        
    private void OnDisable()
    {
        _rect.localScale = Vector2.one * 0.5f;
        _canvas.alpha = 0;
        _button.enabled = false;
    }

    private void OnEnable()
    {
        var duration = 1;
        _rect.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        _canvas.DOFade(1, duration).OnComplete(() => _button.enabled = true);
    }

}
