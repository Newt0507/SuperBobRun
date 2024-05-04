using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private Button _button;

    private void Awake()
    {
        _button.GetComponent<Button>().onClick.AddListener(() =>
        {
            //To do: save prefab ID -> load level
        });
    }
}
