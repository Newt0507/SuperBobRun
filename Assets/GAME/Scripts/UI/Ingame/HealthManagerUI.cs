using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _heartTemplate;
    [SerializeField] private Sprite _emptyHeartSprite;
    [SerializeField] private Sprite _fullyHeartSprite;

    private Player _player;
    
    private void Awake()
    {
        _heartTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Player.Instance.OnHealthChanged += Player_OnHealthChanged;

        UpdateVisual();
    }

    private void Player_OnHealthChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _heartTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _player.GetMaxHealth(); i++)
        {
            Transform heartTransform = Instantiate(_heartTemplate, transform);
            heartTransform.gameObject.SetActive(true);
            
            if (i < _player.GetHealth())
                heartTransform.GetComponent<Image>().sprite = _fullyHeartSprite;
            else
                heartTransform.GetComponent<Image>().sprite = _emptyHeartSprite;
        }
    }
}
