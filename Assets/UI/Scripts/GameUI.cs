using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }
    public GameObject quitConfirmation;
    public GameObject levelCompletedDialog;
    private ProgressManager scoreManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckpointsManager.Instance.AllWagonsPassed += ShowLevelCompletedDialog;
        scoreManager = GetComponent<ProgressManager>();
    }

    public void ShowQuitConfirmation()
    {
        GameManager.Instance.Pause();
        quitConfirmation.SetActive(true);
    }

    public void CloseQuitConfirmation()
    {
        GameManager.Instance.Unpause();
        quitConfirmation.SetActive(false);
    }

    public void ShowLevelCompletedDialog()
    {
        GameManager.Instance.Pause();
        scoreManager.GenerateScore();
        levelCompletedDialog.SetActive(true);
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.Unpause();
        GameManager.Instance.LoadNextLevel();
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.Unpause(); // а точно нужно?
        GameManager.Instance.LoadLevel(0);
    }
}
