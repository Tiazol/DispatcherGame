using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class RailroadManager : MonoBehaviour
{
    public static RailroadManager Instance { get; private set; }

    private const string segmentNamePrefix = "Segment";
    private RailroadSegment[] segments;
    private Dictionary<RailroadSegment, string> names;

    private void Awake()
    {
        Instance = this;

        segments = GetComponentsInChildren<RailroadSegment>();
        names = new Dictionary<RailroadSegment, string>();

        AssignSegments2();
        SetProperties();
    }

    private void AssignSegments()
    {
        // fill dictionary

        foreach (var segment in segments)
        {
            names.Add(segment, segment.gameObject.name.Substring(segmentNamePrefix.Length));
        }

        foreach (var name in names)
        {
            // find prev

            var prevName = name.Value.Substring(0, name.Value.Length - 1);
            name.Key.PrevSegment = names.FirstOrDefault(n => n.Value == prevName).Key;

            // find nexts

            var next1Name = name.Value + "1";
            var next2Name = name.Value + "2";
            name.Key.NextSegment1 = names.FirstOrDefault(n => n.Value == next1Name).Key;
            name.Key.NextSegment2 = names.FirstOrDefault(n => n.Value == next2Name).Key;
        }
    }

    private void AssignSegments2()
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

    public RailroadSegment GetRailroadSegmentForPosition(Vector3 position)
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
}
