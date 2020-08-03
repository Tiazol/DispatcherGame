using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelection;
    public GameObject levelButtons;
    public Button levelButtonPrefab;

    private void Start()
    {
        for (int i = 1; i <= GameManager.Instance.TotalLevels; i++)
        {
            var level = i; // замыкание
            var button = Instantiate(levelButtonPrefab, levelButtons.transform);
            button.GetComponentInChildren<Text>().text = level.ToString();
            button.onClick.AddListener(() => GameManager.Instance.LoadLevel(level));
            button.interactable = level <= GameManager.Instance.UnlockedLevels;
        }
    }

    public void ShowLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void ShowMainMenu()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }
}
