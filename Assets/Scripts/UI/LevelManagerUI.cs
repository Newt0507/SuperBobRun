using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerUI : MonoBehaviour
{
    [SerializeField] private int _levelSceneNumber;
    [SerializeField] private Button _levelButton;
    
    private void Awake()
    {
        _levelButton.gameObject.SetActive(false);
        
        for (int i = 0; i < _levelSceneNumber; i++)
        {
            Button button = Instantiate(_levelButton, transform);
            TextMeshProUGUI levelNumber = button.GetComponentInChildren<TextMeshProUGUI>();
            levelNumber.text = $"{i + 1}";

            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene($"{levelNumber.text}");
            });
            
            button.gameObject.SetActive(true);
        }
    }
}
