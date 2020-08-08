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
        var cp = GetComponentsInChildren<Checkpoint>();
        for (int i = 0; i < cp.Length; i++)
        {
            cp[i].WagonCatched += WagonCatched;
        }
    }

    private void WagonCatched()
    {
        passedWagonsCount++;

        if (passedWagonsCount == WagonGenerator.Instance.totalWagonsCount)
        {
            AllWagonsPassed?.Invoke();
        }
    }
}
