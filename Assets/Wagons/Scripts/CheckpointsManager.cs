using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    public static CheckpointsManager Instance { get; private set; }
    public List<WagonType> UsedWagonTypes { get; set; }
    private int passedWagonsCount;
    public event Action AllWagonsPassed;

    private void Awake()
    {
        Instance = this;
        UsedWagonTypes = new List<WagonType>();
    }

    private void Start()
    {
        //foreach (var cp in GameManager.Instance.Checkpoints)
        //{
        //    cp.Value.WagonCatched += WagonCatched;
        //}
        Debug.Log(GameManager.Instance.Checkpoints.Count); // ПОЧЕМУ ТУТ НОЛЬ???
        //var cp = GameManager.Instance.Checkpoints.Values.ToList();
        var cp = GetComponentsInChildren<Checkpoint>();
        for (int i = 0; i < cp.Length; i++)
        {
            cp[i].WagonCatched += WagonCatched;
        }
    }

    private void WagonCatched()
    {
        passedWagonsCount++;
        Debug.Log("passed " + passedWagonsCount + ", total " + WagonGenerator.Instance.totalWagonsCount);
        if (passedWagonsCount == WagonGenerator.Instance.totalWagonsCount)
        {
            AllWagonsPassed?.Invoke();
        }
    }
}
