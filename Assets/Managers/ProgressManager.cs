using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    public Image[] scoreStars;
    public Sprite[] starSprites;
    public Text scoreText;

    public int TotalLevelsCount { get; private set; }
    public Dictionary<int, (bool, int)> Progress { get; private set; }
    public int UnlockedLevels { get; private set; }
    public int WrongWagons { get; set; }
    public int StarsCount { get; private set; }

    private void Awake()
    {
        Instance = this;
        TotalLevelsCount = SceneManager.sceneCountInBuildSettings - 1;
        LoadProgress();
        CalculateUnlockedLevels();
    }

    private void Start()
    {
        if (CheckpointsManager.Instance != null)
        {
            CheckpointsManager.Instance.AllWagonsPassed += GenerateScore;
        }
    }

    private void LoadProgress()
    {
        Progress = new Dictionary<int, (bool, int)>();
        var data = SaveSystem.LoadData();

        if (data == null)
        {
            Progress.Add(1, (true, 0));
            for (int i = 2; i <= TotalLevelsCount; i++)
            {
                Progress.Add(i, (false, 0));
            }
            SaveProgress();
        }
        else
        {
            for (int i = 0; i < data.LevelsCount; i++)
            {
                Progress.Add(data.levels[i], (data.statuses[i], data.stars[i]));
            }
        }
    }

    public void CalculateUnlockedLevels()
    {
        int k = 0;
        foreach (var level in Progress)
        {
            if (level.Value.Item1)
            {
                k++;
            }
        }
        UnlockedLevels = k;
    }

    public void GenerateScore()
    {
        var successfulWagons = WagonGenerator.Instance.wagonsToLaunch - WrongWagons;
        var result = (float)successfulWagons / WagonGenerator.Instance.wagonsToLaunch;

        if (result < 0.5f)
        {
            StarsCount = 0;
        }
        else if (result >= 0.5f && result < 0.75f)
        {
            StarsCount = 1;
        }
        else if (result >= 0.75f && result < 1.0f)
        {
            StarsCount = 2;
        }
        else
        {
            StarsCount = 3;
        }

        var currentLevel = GameManager.Instance.GetCurrentLevelNumber();

        if (Progress.ContainsKey(currentLevel))
        {
            if (Progress[currentLevel].Item2 < StarsCount)
            {
                Progress.Remove(currentLevel);
                Progress.Add(currentLevel, (true, StarsCount));
            }
        }
        else
        {
            Progress.Add(currentLevel, (true, StarsCount));
        }

        if (Progress.ContainsKey(currentLevel + 1))
        {
            Progress.Remove(currentLevel + 1);
            Progress.Add(currentLevel + 1, (true, 0));
        }
        else
        {
            if (currentLevel + 1 <= TotalLevelsCount)
            {
                Progress.Add(currentLevel + 1, (true, 0));
            }
        }

        SaveProgress();
        ShowProgress(StarsCount);
    }

    private void SaveProgress()
    {
        SaveSystem.SaveData(new PlayerData(Progress));
    }

    private void ShowProgress(int score)
    {
        var currentLevel = GameManager.Instance.GetCurrentLevelNumber();
        var savedStars = Progress[currentLevel].Item2;

        var successfulWagons = WagonGenerator.Instance.wagonsToLaunch - WrongWagons;
        scoreText.text = successfulWagons + " / " + WagonGenerator.Instance.wagonsToLaunch;



        scoreStars[2].sprite = score == 3 ? starSprites[1] : savedStars == 3 ? starSprites[2] : starSprites[0];
        scoreStars[1].sprite = score >= 2 ? starSprites[1] : savedStars >= 2 ? starSprites[2] : starSprites[0];
        scoreStars[0].sprite = score >= 1 ? starSprites[1] : savedStars >= 1 ? starSprites[2] : starSprites[0];



        //switch (score)
        //{
        //    case 3:
        //        scoreStars[0].sprite = scoreStars[1].sprite = scoreStars[2].sprite = starSprites[1];
        //        break;
        //    case 2:
        //        scoreStars[0].sprite = scoreStars[1].sprite = starSprites[1];
        //        scoreStars[2].sprite = (score >= savedStars) ? starSprites[0] : starSprites[2];
        //        break;
        //    case 1:
        //        scoreStars[0].sprite = starSprites[1];
        //        scoreStars[2].sprite = scoreStars[1].sprite = starSprites[0];
        //        break;
        //    case 0:
        //        scoreStars[0].sprite = scoreStars[1].sprite = scoreStars[2].sprite = starSprites[0];
        //        break;
        //}
    }

    public int GetScoreOfLevel(int level)
    {
        return Progress.ContainsKey(level) ? Progress[level].Item2 : 0;
    }
}
