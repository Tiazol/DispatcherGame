using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailNetworkManager : MonoBehaviour
{
    public static RailNetworkManager Instance { get; private set; }

    public RailroadSegment[] railroadSegments { get; private set; }
    public RailroadSwitch[] railroadSwitches { get; private set; }

    private void Awake()
    {
        Instance = this;

        railroadSegments = GetComponentsInChildren<RailroadSegment>();
        railroadSwitches = GetComponentsInChildren<RailroadSwitch>();
    }

    public RailroadSegment GetNewRailroadSegment(RailroadSegment current)
    {
        foreach(var rSwitch in railroadSwitches)
        {
            if (rSwitch.HasThisStartSegment(current))
            {
                return rSwitch.ActiveRailroadSegment;
            }
        }

        return current.nextSegment1;
    }
}
