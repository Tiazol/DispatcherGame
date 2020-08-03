using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<GameObject, Checkpoint> Checkpoints { get; private set; }

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

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
