using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public GameObject quitConfirmation;
    public GameObject levelCompletedDialog;
    public Button nextButton;
    public Text levelCompletedText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckpointsManager.Instance.AllWagonsPassed += ShowLevelCompletedDialog;
        //LocalizationManager.Instance.LanguageChanged += LoadLocalization;
    }

    //private void LoadLocalization()
    //{
    //    levelCompletedText.text = LocalizationManager.Instance.GetLocalizedString("levelCompleted");
    //}

    public void OnNextOrRetryButtonPressed()
    {
        if (ProgressManager.Instance.StarsCount == 0)
        {
            LoadThisLevel();
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void ShowQuitConfirmation()
    {
        GameManager.Instance.PauseGame();
        quitConfirmation.SetActive(true);
    }

    public void CloseQuitConfirmation()
    {
        GameManager.Instance.ResumeGame();
        quitConfirmation.SetActive(false);
    }

    public void ShowLevelCompletedDialog()
    {
        //GameManager.Instance.PauseGame();
        ProgressManager.Instance.GenerateScore();

        if (ProgressManager.Instance.StarsCount == 0)
        {
            Debug.Log("wo", levelCompletedText);
            levelCompletedText.text = LocalizationManager.Instance.GetLocalizedString("levelNotCompleted");
            nextButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedString("retry");
        }
        else
        {
            levelCompletedText.text = LocalizationManager.Instance.GetLocalizedString("levelCompleted");
            nextButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedString("next");
        }

        levelCompletedDialog.SetActive(true);
    }

    public void LoadThisLevel()
    {
        GameManager.Instance.ResumeGame();
        GameManager.Instance.LoadThisLevel();
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.ResumeGame();
        GameManager.Instance.LoadNextLevel();
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.ResumeGame(); // а точно нужно?
        GameManager.Instance.LoadLevel(0);
    }
}
