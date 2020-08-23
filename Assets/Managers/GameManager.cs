using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool GamePaused { get; private set; }
    public int TotalLevelsCount => SceneManager.sceneCountInBuildSettings - 1;
    public int CurrentLevelNumber => SceneManager.GetActiveScene().buildIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android) // TODO оптимизировать с помощью событий?
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    Quit();
                }
                else
                {
                    GameUI.Instance.ShowQuitConfirmation();
                }
            }
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadThisLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        var newScene = currentScene < TotalLevelsCount ? currentScene + 1 : 0;
        SceneManager.LoadScene(newScene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        GamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GamePaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
