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

    public int UnlockedLevels { get; private set; }
    public int TotalLevels { get; } = 15;

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
    }

    private void Start()
    {
        UnlockedLevels = PlayerPrefs.HasKey(pp_unlockedLevels) ? PlayerPrefs.GetInt(pp_unlockedLevels) : 1;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
