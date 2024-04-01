using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelChoosingManagerUI : MonoBehaviour
{
    [SerializeField] private int _levelSceneNumber;
    [SerializeField] private Button _levelButton;
    
    private void Awake()
    {
        _levelButton.gameObject.SetActive(false);
        
        for (int i = 0; i < _levelSceneNumber; i++)
        {
            Button button = Instantiate(_levelButton, transform);
            button.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            
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



[Serializable]
public class CountryData
{
    public Vector3 position;
    public 
}