using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleCharacterButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESound.Click);

            //Save prefab ID
            Data.SetCharacter(_characterPrefab.GetHashCode());
            UIManager.Instance.GetMainMenuContainer().GetSelectLevelUI().gameObject.SetActive(true);
        });
    }
}
