using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<GameObject, RailroadSegment> RailroadSegments { get; private set; }
    public Dictionary<GameObject, Checkpoint> Checkpoints { get; private set; }

    public bool IsPaused { get; private set; }

    public int WrongWagons { get; set; }

    private void Awake()
    {
        Instance = this;
        RailroadSegments = new Dictionary<GameObject, RailroadSegment>();
        Checkpoints = new Dictionary<GameObject, Checkpoint>();
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

    public void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        var newScene = currentScene < ProgressManager.Instance.TotalLevelsCount ? currentScene + 1 : 0;
        SceneManager.LoadScene(newScene);
    }

    public int GetCurrentLevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
