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
        for (int i = 0; i < GameManager.Instance.TotalLevels; i++)
        {
            var j = i; // замыкание
            var button = Instantiate(levelButtonPrefab, levelButtons.transform);
            var text = button.GetComponentInChildren<Text>();
            text.text = (j + 1).ToString();
            button.onClick.AddListener(() => GameManager.Instance.LoadScene(j + 1));
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
