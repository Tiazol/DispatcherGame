using PathCreation;

using System;

using UnityEngine;

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
    public Vector3 FirstPoint => pathCreator.path.GetPoint(0);
    public Vector3 LastPoint => pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
    public Quaternion GetRotationAtDistance(float distance) => pathCreator.path.GetRotationAtDistance(distance);

    public event Action<bool> VisibilityChanged;
    public event Action<bool> SelectedRailroadSegmentChanged;

    private const string animator_Hide = "Hide";
    private const string animator_Show = "Show";

    private bool isVisible;
    private RailroadSegment selectedRailroadSegment;

    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private Animator animator;

    public void Show()
    {
        if (!IsVisible)
        {
            if (PrevSegment.IsVisible)
            {
                if (PrevSegment.SelectedRailroadSegment == this)
                {
                    animator.SetTrigger(animator_Show);

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
    }

    public void Hide()
    {
        if (IsVisible)
        {
            animator.SetTrigger(animator_Hide);

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
    }

    public void SwitchSelectedRailroadSegment()
    {
        SelectedRailroadSegment = SelectedRailroadSegment == NextSegment1 ? NextSegment2 : NextSegment1;

        if (SelectedRailroadSegment == NextSegment1)
        {
            SwitchToLeft();
        }
        else
        {
            SwitchToRight();
        }
    }

    public void SwitchToLeft()
    {
        if (NextSegment2 == null)
        {
            throw new Exception("This segment doesn't have Next Segment 2");
        }

        selectedRailroadSegment = NextSegment1;
        NextSegment1.Show();
        NextSegment2.Hide();
    }

    public void SwitchToRight()
    {
        if (NextSegment2 == null)
        {
            throw new Exception("This segment doesn't have Next Segment 2");
        }

        selectedRailroadSegment = NextSegment2;
        NextSegment1.Hide();
        NextSegment2.Show();
    }

    public RailroadSegment GetNextRailroadSegment()
    {
        return SelectedRailroadSegment;
    }
}
