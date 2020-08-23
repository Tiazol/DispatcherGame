using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    #region public
    public static ProgressManager Instance { get; private set; }

    public int SuccessfulWagons { get; set; }
    public int TotalWagons => WagonGenerator.Instance.wagonsToLaunch;
    public int CurrentStarsCount => GetStarsCount(SuccessfulWagons, WagonGenerator.Instance.wagonsToLaunch);
    public int SavedStarsCount => GetSavedStarsCountOfLevel(GameManager.Instance.CurrentLevelNumber, progress);

    public event Action ProgressChanged;
    public event Action LevelCompleted;

    public bool IsLevelAvailable(int level)
    {
        return progress[level].Item1;
    }

    public int GetSavedStarsCountForLevel(int level)
    {
        return progress[level].Item2;
    }

    public void ResetProgress()
    {
        var progress = new Dictionary<int, (bool, int)>();

        int levelNumber = 1;
        progress.Add(levelNumber, (true, 0));

        for (levelNumber = 2; levelNumber <= GameManager.Instance.TotalLevelsCount; levelNumber++)
        {
            progress.Add(levelNumber, (false, 0));
        }

        this.progress = progress;
        SaveProgress(progress);
        ProgressChanged?.Invoke();
    }
    #endregion public

    #region private
    private Dictionary<int, (bool, int)> progress;

    private void Awake()
    {
        Instance = this;
        progress = LoadProgress();
    }

    private void Start()
    {
        if (CheckpointsManager.Instance != null)
        {
            CheckpointsManager.Instance.AllWagonsPassed += OnAllWagonsPassed;
        }
    }

    private void OnAllWagonsPassed()
    {
        progress = UpdateLevelProgress(GameManager.Instance.CurrentLevelNumber, CurrentStarsCount, progress);
        ProgressChanged?.Invoke();
        LevelCompleted?.Invoke();
    }

    private void SaveProgress(Dictionary<int, (bool, int)> progress)
    {
        SaveSystem.SaveData(new PlayerData(progress));
    }

    private Dictionary<int, (bool, int)> LoadProgress()
    {
        var progress = new Dictionary<int, (bool, int)>();
        var data = SaveSystem.LoadData();

        if (data == null)
        {
            progress.Add(1, (true, 0));
            for (int i = 2; i <= GameManager.Instance.TotalLevelsCount; i++)
            {
                progress.Add(i, (false, 0));
            }
        }
        else
        {
            for (int i = 0; i < data.LevelsCount; i++)
            {
                progress.Add(data.levels[i], (data.statuses[i], data.stars[i]));
            }
        }

        return progress;
    }

    private int GetStarsCount(int successfulWagons, int totalWagons)
    {
        int starsCount = 0;
        var result = (float)successfulWagons / totalWagons;

        if (result >= 0.5f && result < 0.75f)
        {
            starsCount = 1;
        }
        else if (result >= 0.75f && result < 1.0f)
        {
            starsCount = 2;
        }
        else if (result == 1.0f)
        {
            starsCount = 3;
        }

        return starsCount;
    }

    private int GetSavedStarsCountOfLevel(int level, Dictionary<int, (bool, int)> progress)
    {
        return progress.ContainsKey(level) ? progress[level].Item2 : 0;
    }

    private Dictionary<int, (bool, int)> UpdateLevelProgress(int currentLevel, int currentStarsCount, Dictionary<int, (bool, int)> progress)
    {
        if (progress.ContainsKey(currentLevel))
        {
            if (progress[currentLevel].Item2 < currentStarsCount)
            {
                progress.Remove(currentLevel);
                progress.Add(currentLevel, (true, currentStarsCount));
            }
        }
        else
        {
            progress.Add(currentLevel, (true, currentStarsCount));
        }

        if (currentStarsCount > 0)
        {
            if (progress.ContainsKey(currentLevel + 1))
            {
                progress.Remove(currentLevel + 1);
                progress.Add(currentLevel + 1, (true, 0));
            }
            else
            {
                if (currentLevel + 1 <= GameManager.Instance.TotalLevelsCount)
                {
                    progress.Add(currentLevel + 1, (true, 0));
                }
            }
        }

        SaveProgress(progress);

        return progress;
    }
    #endregion private
}
