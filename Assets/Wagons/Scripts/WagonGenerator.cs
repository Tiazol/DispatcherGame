using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public Wagon wagonPrefab;
    public float wagonGeneratorInterval;
    public float wagonSpeed;
    public RailroadSegment startRailroadSegment;
    public int totalWagonsCount;

    private void Start()
    {
        InvokeRepeating("GenerateWagon", 0f, wagonGeneratorInterval);
    }

    private void GenerateWagon()
    {
        if (totalWagonsCount < 1)
        {
            return;
        }
        var typeCount = System.Enum.GetNames(typeof(WagonType)).Length;
        var typeIndex = Random.Range(0, typeCount); // warning! max int is EXCLUSIVE!
        var wagon = Instantiate(wagonPrefab, startRailroadSegment.startPoint, Quaternion.identity, transform);
        wagon.startSegment = startRailroadSegment;
        wagon.Speed = wagonSpeed;
        wagon.SetWagonType(typeIndex);
        totalWagonsCount--;
    }
}
