using System;
using System.Collections.Generic;

using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    public static CheckpointsManager Instance { get; private set; }

    public Dictionary<GameObject, Checkpoint> Checkpoints { get; private set; }
    public List<WagonType> UsedWagonTypes { get; set; }
    public event Action AllWagonsPassed;

    private int passedWagonsCount;

    private void Awake()
    {
        Instance = this;

        Checkpoints = new Dictionary<GameObject, Checkpoint>();
        UsedWagonTypes = new List<WagonType>();

        var checkpoints = GetComponentsInChildren<Checkpoint>();
        foreach (var checkpoint in checkpoints)
        {
            Checkpoints.Add(checkpoint.gameObject, checkpoint);
            checkpoint.WagonPassed += WagonPassed;
        }
    }

    private void WagonPassed()
    {
        passedWagonsCount++;

        if (passedWagonsCount == WagonGenerator.Instance.WagonsToLaunch)
        {
            AllWagonsPassed?.Invoke();
        }
    }
}
