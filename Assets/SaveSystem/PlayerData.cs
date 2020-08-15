using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int[] levels;
    public bool[] statuses;
    public int[] stars;

    public int LevelsCount => levels.Length;

    public PlayerData(Dictionary<int, (bool, int)> progress)
    {
        levels = new int[progress.Count];
        statuses = new bool[progress.Count];
        stars = new int[progress.Count];

        //for (int i = 0; i < progress.Count; i++)
        //{
        //    int key = i + 1;
        //    levels[i] = key;
        //    statuses[i] = progress[key].Item1;
        //    stars[i] = progress[key].Item2;
        //}
        int index = 0;
        foreach (var level in progress)
        {
            levels[index] = level.Key;
            statuses[index] = progress[level.Key].Item1;
            stars[index] = progress[level.Key].Item2;
            index++;
        }
    }
}
