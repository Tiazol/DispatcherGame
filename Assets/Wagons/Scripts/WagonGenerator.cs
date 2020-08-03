using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonGenerator : MonoBehaviour
{
    public Wagon wagonPrefab;
    public float wagonGeneratorInterval;
    public float wagonSpeed;
    public RailroadSegment startRailroadSegment;

    private void Start()
    {
        InvokeRepeating("GenerateWagon", 0f, wagonGeneratorInterval);
    }

    private void GenerateWagon()
    {
        //var index = Random.Range(0, wagonPrefabs.Length); // warning! max int is EXCLUSIVE!

        var typeCount = System.Enum.GetNames(typeof(WagonType)).Length;
        var typeIndex = Random.Range(0, typeCount); // warning! max int is EXCLUSIVE!
        var wagon = Instantiate(wagonPrefab, startRailroadSegment.startPoint, Quaternion.identity, transform);
        wagon.startSegment = startRailroadSegment;
        wagon.Speed = wagonSpeed;
        wagon.SetWagonType(typeIndex);
    }
}
