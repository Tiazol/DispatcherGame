using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string pp_unlockedLevels = "unlockedLevels";

    public static GameManager Instance { get; private set; }
    public Dictionary<GameObject, Checkpoint> Checkpoints { get; private set; }
    public Dictionary<GameObject, RailroadSegment> RailroadSegments { get; private set; }

    public int UnlockedLevels { get; private set; }
    public int TotalLevelsCount { get; } = 1;

    public bool IsPaused { get; private set; }

    public event Action ScoreChanged;
    private int score;
    public int Score
    {
        get => score;
        set
        {
            if ((score != value) && (Mathf.Abs(value) <= 1000))
            {
                score = value;
                ScoreChanged?.Invoke();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        Checkpoints = new Dictionary<GameObject, Checkpoint>();
        RailroadSegments = new Dictionary<GameObject, RailroadSegment>();
        UnlockedLevels = PlayerPrefs.HasKey(pp_unlockedLevels) ? PlayerPrefs.GetInt(pp_unlockedLevels) : 1;
    }

    private void Start()
    {
        CheckpointsManager.Instance.AllWagonsPassed += RecalculateUnlockedLevels;
    }

    private void RecalculateUnlockedLevels()
    {
        var scene = SceneManager.GetActiveScene().handle;
        if (scene > UnlockedLevels)
        {
            UnlockedLevels = scene;
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().handle == 0)
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
        var currentScene = SceneManager.GetActiveScene().handle;
        var newScene = currentScene < TotalLevelsCount ? currentScene + 1 : 0;
        SceneManager.LoadScene(newScene);
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
