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

    private const string retryText = "Retry";
    private const string nextText = "Next";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckpointsManager.Instance.AllWagonsPassed += ShowLevelCompletedDialog;
    }

    public void OnNextButtonPressed()
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
            nextButton.GetComponentInChildren<Text>().text = retryText;
        }
        else
        {
            nextButton.GetComponentInChildren<Text>().text = nextText;
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
