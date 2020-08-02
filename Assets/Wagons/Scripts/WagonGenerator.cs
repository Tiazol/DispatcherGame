using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public Wagon[] wagonPrefabs;
    public float wagonGeneratorInterval;
    public Vector3 wagonStartPosition;
    public RailroadSegment startRailroadSegment;

    private void Start()
    {
        InvokeRepeating("GenerateWagon", 0f, wagonGeneratorInterval);
    }

    private void GenerateWagon()
    {
        var index = Random.Range(0, wagonPrefabs.Length); // warning! max int is EXCLUSIVE!
        var wagon = Instantiate(wagonPrefabs[index], wagonStartPosition, Quaternion.identity, transform);
        wagon.startSegment = startRailroadSegment;
    }
}
