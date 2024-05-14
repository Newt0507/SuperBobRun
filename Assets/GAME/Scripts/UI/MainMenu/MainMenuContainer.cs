using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuContainer : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private GameObject _selectCharacterUI;
    [SerializeField] private GameObject _selectLevelUI;

    //private CanvasGroup _canvas;
    
    private void Awake()
    {
        //_canvas = GetComponent<CanvasGroup>();

        //_mainMenuUI.gameObject.SetActive(false);
        //_selectCharacterUI.SetActive(false);
        //_selectLevelUI.gameObject.SetActive(false);
        //_canvas.alpha = 0;
    }

    private void OnEnable()
    {
        UIManager.Instance.FadeImageUI(() => _mainMenuUI.gameObject.SetActive(true));
    }

    private void OnDisable()
    {
        _mainMenuUI.gameObject.SetActive(false);
    }

    public MainMenuUI GetMainMenuUI()
    {
        return _mainMenuUI;
    }
    
    public GameObject GetSelectCharacterUI()
    {
        return _selectCharacterUI;
    }

    public GameObject GetSelectLevelUI()
    {
        return _selectLevelUI;
    }

    //public CanvasGroup GetCanvasGroup()
    //{
    //    return _canvas;
    //}
}
