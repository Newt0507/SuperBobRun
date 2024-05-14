using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheckpoint : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;

    private void Awake()
    {
        foreach (var character in _characters)
        {
            if (character.GetHashCode() == Data.GetCharacter())
            {
                Instantiate(character, Vector2.up * 3, Quaternion.identity);
                Time.timeScale = 0;
                break;
            }            
        }


        UIManager.Instance.FadeImageUI(() => Time.timeScale = 1);
    }
}
