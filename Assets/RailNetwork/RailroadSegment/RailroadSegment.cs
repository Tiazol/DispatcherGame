using PathCreation;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.U2D;

public class RailroadSegment : MonoBehaviour
{
    public RailroadSegment prevSegment;
    public RailroadSegment nextSegment1;
    public RailroadSegment nextSegment2;
    public RailroadSegment SelectedRailroadSegment { get; set; }

    private bool isSelected;
    private bool isShowing;

    private SpriteShapeRenderer ssr;

    public event Action<bool> StatusChanged;
    public event Action<bool> SelectedRailroadSegmentChanged;

    public PathCreator pathCreator;

    private void Awake()
    {
        ssr = GetComponent<SpriteShapeRenderer>();
    }

    private void Start()
    {
        GameManager.Instance.RailroadSegments.Add(gameObject, this);

        // можно упростить?
        isSelected = prevSegment == null ? true : false;
        isShowing = prevSegment == null ? true : false;

        SelectedRailroadSegment = nextSegment1;
        SelectedRailroadSegmentChanged?.Invoke(false);

        // 1. Подписка
        if (prevSegment != null)
        {
            prevSegment.StatusChanged += isActive =>
            {
                if (!isActive)
                {
                    Disable();
                }
                else
                {
                    if (this == prevSegment.SelectedRailroadSegment)
                    {
                        Enable();
                    }
                }
            };

        }
        // 2. Отключение второй ветки в стрелке
        if (nextSegment2 != null)
        {
            nextSegment2.Disable();
        }

        // 3. Сообщение о своем состоянии
        StatusChanged?.Invoke(isSelected);
    }

    private void OnDrawGizmos()
    {
        var startP = pathCreator.path.GetPoint(0);
        var endP = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(startP, 0.125f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endP, 0.125f);
    }

    public void Enable()
    {
        if (prevSegment != null && prevSegment.isShowing || prevSegment == null)
        {
            var color = ssr.color;
            color.a = 1.0f;
            ssr.color = color;

            isShowing = true;
        }

        isSelected = true;
        StatusChanged?.Invoke(isSelected);
    }

    public void Disable()
    {
        isSelected = false;
        isShowing = false;

        var color = ssr.color;
        color.a = 0.25f;
        ssr.color = color;

        StatusChanged?.Invoke(isSelected);
    }

    public void SwitchActiveRailroadSegment()
    {
        if (nextSegment2 == null)
        {
            Debug.LogError("This segment doesn't have Next Segment 2");
            return;
        }

        SelectedRailroadSegment = SelectedRailroadSegment == nextSegment1 ? nextSegment2 : nextSegment1;

        if (SelectedRailroadSegment == nextSegment1)
        {
            SelectedRailroadSegmentChanged?.Invoke(false);
        }
        else
        {
            SelectedRailroadSegmentChanged?.Invoke(true);
        }

        if (SelectedRailroadSegment == nextSegment1)
        {
            nextSegment1.Enable();
            nextSegment2.Disable();
        }
        else
        {
            nextSegment1.Disable();
            nextSegment2.Enable();
        }
    }

    public RailroadSegment GetNextRailroadSegment()
    {
        return SelectedRailroadSegment;
    }
}
