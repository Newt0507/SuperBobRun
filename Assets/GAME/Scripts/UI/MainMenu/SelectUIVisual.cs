using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUIVisual : MonoBehaviour
{
    [SerializeField] private RectTransform _title;
    [SerializeField] private RectTransform _menuUI;
    [SerializeField] private CanvasGroup _canvas;

    private const int TITLE_ANCHORED_POSITION_Y = 1000;
    private const int MENUUI_ANCHORED_POSITION_Y = -1000;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        var _duration = .7f;
        _canvas.DOFade(1, _duration).OnComplete(()=> 
        {
            _title.DOAnchorPosY(-150, _duration);
            _menuUI.DOAnchorPosY(-70, _duration);
        });
    }

    private void OnDisable()
    {
        _canvas.alpha = 0;
        _title.anchoredPosition = new Vector2(0, TITLE_ANCHORED_POSITION_Y);
        _menuUI.anchoredPosition = new Vector2(0, MENUUI_ANCHORED_POSITION_Y);
    }
}
