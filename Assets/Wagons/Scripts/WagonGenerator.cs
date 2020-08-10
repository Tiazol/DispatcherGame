using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public static WagonGenerator Instance { get; private set; }
    public Wagon wagonPrefab;
    public float wagonGeneratorInterval;
    public float wagonSpeed;
    public RailroadSegment startRailroadSegment;
    public int totalWagonsCount;
    private int passedWagonsCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("GenerateWagon", 0f, wagonGeneratorInterval);
    }

    private void GenerateWagon()
    {
        if (passedWagonsCount == totalWagonsCount)
        {
            return;
        }
        var typeCount = System.Enum.GetNames(typeof(WagonType)).Length;
        int typeIndex;
        do
        {
            typeIndex = Random.Range(0, typeCount); // warning! max int is EXCLUSIVE!
        } while (!CheckpointsManager.Instance.UsedWagonTypes.Contains((WagonType)typeIndex));
        var wagon = Instantiate(wagonPrefab, startRailroadSegment.GetPoint(0), Quaternion.identity, transform);
        wagon.startSegment = startRailroadSegment;
        wagon.Speed = wagonSpeed;
        wagon.SetWagonType(typeIndex);
        passedWagonsCount++;
    }
}
