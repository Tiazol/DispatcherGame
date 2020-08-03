using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public GameObject quitConfirmation;

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score;
        GameManager.Instance.ScoreChanged += () => scoreText.text = "Score: " + GameManager.Instance.Score;
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

    public void QuitToMainMenu()
    {
        GameManager.Instance.Unpause(); // а точно нужно?
        GameManager.Instance.LoadLevel(0);
    }
}
