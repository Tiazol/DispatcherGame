using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class RailroadManager : MonoBehaviour
{
    public static RailroadManager Instance { get; private set; }

    private RailroadSegment[] segments;

    private void Awake()
    {
        Instance = this;

        segments = GetComponentsInChildren<RailroadSegment>();

        AssignSegments();
        SetProperties();
    }

    private void AssignSegments()
    {
        foreach (var segment in segments)
        {
            foreach (var otherSegment in segments)
            {
                if (otherSegment == segment)
                {
                    continue;
                }

                if (segment.FirstPoint == otherSegment.LastPoint)
                {
                    segment.PrevSegment = otherSegment;
                }

                if (segment.LastPoint == otherSegment.FirstPoint)
                {
                    if (segment.NextSegment1 == null)
                    {
                        segment.NextSegment1 = otherSegment;
                    }
                    else
                    {
                        if (segment.NextSegment1.LastPoint.x < otherSegment.LastPoint.x)
                        {
                            segment.NextSegment2 = otherSegment;
                        }
                        else
                        {
                            segment.NextSegment2 = segment.NextSegment1;
                            segment.NextSegment1 = otherSegment;
                        }
                    }
                }
            }
        }
    }

    private void SetProperties()
    {
        // set start conditions

        foreach (var segment in segments)
        {
            segment.IsVisible = true;
            segment.SelectedRailroadSegment = segment.NextSegment1;
        }

        // hide all next2

        foreach (var segment in segments)
        {
            if (segment.NextSegment2 != null)
            {
                segment.NextSegment2.Hide();
            }
        }
    }

    public RailroadSegment GetFirstRailroadSegment()
    {
        return segments.FirstOrDefault(segment => segment.PrevSegment == null);
    }

    public RailroadSegment GetRailroadSegmentAtCenterPosition(Vector3 position)
    {
        foreach(var segment in segments)
        {
            if (segment.transform.position == position)
            {
                return segment;
            }
        }

        return null;
    }

    public RailroadSegment GetRailroadSegmentAtTopPosition(Vector3 position)
    {
        foreach (var segment in segments)
        {
            if (segment.LastPoint == position)
            {
                return segment;
            }
        }

        return null;
    }
}
