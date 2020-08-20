using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu; 
    public GameObject levelSelection;
    public GameObject levelButtons;
    public GameObject settings;
    public GameObject confirmationResetProgress;
    public Button levelButtonPrefab;

    private void AssignLevelButtons()
    {
        for (int i = 1; i <= ProgressManager.Instance.TotalLevelsCount; i++)
        {
            var level = i; // замыкание
            var button = Instantiate(levelButtonPrefab, levelButtons.transform);
            button.GetComponentInChildren<Text>().text = level.ToString();
            button.GetComponent<LevelButton>().levelNumber = level;
            button.onClick.AddListener(() => OnClickLevelButton(level));
            button.interactable = level <= ProgressManager.Instance.UnlockedLevels;
        }
    }

    // TODO: optimize
    private void ClearLevelButtons()
    {
        var levels = levelButtons.GetComponentsInChildren<LevelButton>();
        for (int i = 0; i < levels.Length; i++)
        {
            Destroy(levels[i]);
        }
    }

    #region MainMenu
    public void OnClickPlayButton()
    {
        ClearLevelButtons();
        AssignLevelButtons();
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void OnClickSettingsButton()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void OnClickQuitButton()
    {
        GameManager.Instance.Quit();
    }
    #endregion MainMenu

    #region LevelSelection
    public void OnClickLevelButton(int level)
    {
        GameManager.Instance.LoadLevel(level);
    }

    public void OnClickBackFromLevelSelectionToMainMenuButton()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }
    #endregion LevelSelection

    #region Settings
    public void OnClickSwitchSoundButton()
    {

    }

    public void OnClickResetProgressButton()
    {
        settings.SetActive(false);
        confirmationResetProgress.SetActive(true);
    }

    public void OnClickBackFromSettingsToMainMenuButton()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnClickChangeLanguage(string language)
    {
        LocalizationManager.Instance.ChangeLanguageTo(language);
    }
    #endregion Settings

    #region ConfirmationResetProgress
    public void OnClickConfirmResetProgressButton()
    {
        ProgressManager.Instance.ResetProgress();
        confirmationResetProgress.SetActive(false);
        settings.SetActive(true);
    }

    public void OnClickBackFromResetProgressToSettings()
    {
        confirmationResetProgress.SetActive(false);
        settings.SetActive(true);
    }
    #endregion ConfirmationResetProgress
}
