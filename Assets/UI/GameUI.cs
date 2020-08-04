using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }
    public Text scoreText;
    public GameObject quitConfirmation;
    public GameObject levelCompletedDialog;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score;
        GameManager.Instance.ScoreChanged += () => scoreText.text = "Score: " + GameManager.Instance.Score;
        CheckpointsManager.Instance.AllWagonsPassed += ShowLevelCompletedDialog;
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
