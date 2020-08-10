using PathCreation;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.U2D;

public class RailroadSegment : MonoBehaviour
{
    public RailroadSegment PrevSegment { get; set; }
    public RailroadSegment NextSegment1 { get; set; }
    public RailroadSegment NextSegment2 { get; set; }
    public RailroadSegment SelectedRailroadSegment
    {
        get => selectedRailroadSegment;
        set
        {
            selectedRailroadSegment = value;
            SelectedRailroadSegmentChanged?.Invoke(value == NextSegment1);
        }
    }

    public bool IsVisible
    {
        get => isVisible;
        set
        {
            isVisible = value;
            VisibilityChanged?.Invoke(isVisible);
        }
    }

    public float Length => pathCreator.path.length;
    public Vector3 GetPoint(int index) => pathCreator.path.GetPoint(index);
    public Vector3 GetPointAtDistance(float distance) => pathCreator.path.GetPointAtDistance(distance);
    public Quaternion GetRotationAtDistance(float distance) => pathCreator.path.GetRotationAtDistance(distance);

    public event Action<bool> VisibilityChanged;
    public event Action<bool> SelectedRailroadSegmentChanged;

    [SerializeField] private bool isVisible;
    private RailroadSegment selectedRailroadSegment;
    private SpriteShapeRenderer ssr;
    private PathCreator pathCreator;

    private void Awake()
    {
        ssr = GetComponent<SpriteShapeRenderer>();
        pathCreator = GetComponentInChildren<PathCreator>();
    }

    private void OnDrawGizmos()
    {
        if (false)
        {
            var startP = pathCreator.path.GetPoint(0);
            var endP = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(startP, 0.125f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endP, 0.125f);
        }
    }

    public void Show()
    {
        if (PrevSegment.IsVisible)
        {
            if (PrevSegment.SelectedRailroadSegment == this)
            {
                var color = new Color(ssr.color.r, ssr.color.g, ssr.color.b, 1.0f);
                ssr.color = color;

                IsVisible = true;

                if (NextSegment1 != null)
                {
                    NextSegment1.Show();
                }

                if (NextSegment2 != null)
                {
                    NextSegment2.Show();
                }
            }
        }
    }

    public void Hide()
    {
        var color = new Color(ssr.color.r, ssr.color.g, ssr.color.b, 0.25f);
        ssr.color = color;

        IsVisible = false;

        if (NextSegment1 != null)
        {
            NextSegment1.Hide();
        }

        if (NextSegment2 != null)
        {
            NextSegment2.Hide();
        }
    }

    public void SwitchSelectedRailroadSegment()
    {
        if (NextSegment2 == null)
        {
            Debug.LogError("This segment doesn't have Next Segment 2");
            return;
        }

        SelectedRailroadSegment = SelectedRailroadSegment == NextSegment1 ? NextSegment2 : NextSegment1;

        if (SelectedRailroadSegment == NextSegment1)
        {
            NextSegment1.Show();
            NextSegment2.Hide();
        }
        else
        {
            NextSegment1.Hide();
            NextSegment2.Show();
        }
    }

    public RailroadSegment GetNextRailroadSegment()
    {
        return SelectedRailroadSegment;
    }
}
