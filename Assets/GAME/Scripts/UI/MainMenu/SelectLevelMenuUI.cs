using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelMenuUI : MonoBehaviour
{
    [SerializeField] private int _levelSceneNumber;
    [SerializeField] private Button _levelTemplateButton;

    private void Awake()
    {
        _levelTemplateButton.gameObject.SetActive(false);

        for (int i = 0; i < _levelSceneNumber; i++)
        {
            Button button = Instantiate(_levelTemplateButton, transform);
            button.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            TextMeshProUGUI levelNumber = button.GetComponentInChildren<TextMeshProUGUI>();
            levelNumber.text = $"{i + 1}";

            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(ESound.Click);

                AudioManager.Instance.StopMusic();

                UIManager.Instance.FadeImageUI(() => 
                {
                    UIManager.Instance.GetMainMenuContainer().GetSelectCharacterUI().SetActive(false);
                    UIManager.Instance.GetMainMenuContainer().GetSelectLevelUI().SetActive(false);
                    UIManager.Instance.GetMainMenuContainer().gameObject.SetActive(false);
                    SceneManager.LoadScene($"{levelNumber.text}");
                });
            });

            button.gameObject.SetActive(true);
        }
    }
}
