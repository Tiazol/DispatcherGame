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
    public Toggle soundToggle;

    private void Start()
    {
        CreateLevelButtons();
        soundToggle.isOn = AudioManager.Instance.CurrentSoundState == SoundState.On;
        soundToggle.onValueChanged.AddListener(isOn => OnClickSwitchSoundButton(isOn));
    }

    private void CreateLevelButtons()
    {
        for (int i = 1; i <= GameManager.Instance.TotalLevelsCount; i++)
        {
            var level = i; // замыкание
            var button = Instantiate(levelButtonPrefab, levelButtons.transform);
            button.GetComponentInChildren<Text>().text = level.ToString();
            button.GetComponent<LevelButton>().LevelNumber = level;
            button.onClick.AddListener(() => OnClickLevelButton(level));
        }
    }

    #region MainMenu
    public void OnClickPlayButton()
    {
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
    public void OnClickSwitchSoundButton(bool isOn)
    {
        AudioManager.Instance.SwitchSoundToState(isOn);
        Debug.Log(isOn);
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
