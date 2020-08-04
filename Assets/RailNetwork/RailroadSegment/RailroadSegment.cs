using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.U2D;

public class RailroadSegment : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public List<Vector3> Points { get; private set; }

    public RailroadSegment prevSegment;
    public RailroadSegment nextSegment1;
    public RailroadSegment nextSegment2;
    public RailroadSegment ActiveRailroadSegment { get; set; }

    private bool isActive;
    private bool isShowing;

    private SpriteShapeRenderer ssr;
    private SpriteShapeController ssc;

    public event Action<bool> StatusChanged;
    public event Action<bool> ActiveRailroadSegmentChanged;

    private void Awake()
    {
        ssr = GetComponent<SpriteShapeRenderer>();
        ssc = GetComponent<SpriteShapeController>();
    }

    private void Start()
    {
        GameManager.Instance.RailroadSegments.Add(gameObject, this);

        Points = new List<Vector3>();
        CalculatePoints();

        isActive = true;
        isShowing = true;
        ActiveRailroadSegment = nextSegment1;
        ActiveRailroadSegmentChanged?.Invoke(false);

        // 1. Подписка
        prevSegment.StatusChanged += isActive =>
        {
            if (!isActive)
            {
                Disable();
            }
            else
            {
                if (this == prevSegment.ActiveRailroadSegment)
                {
                    Enable();
                }
            }
        };

        // 2. Отключение второй ветки в стрелке
        if (nextSegment2 != null)
        {
            nextSegment2.Disable();
        }

        // 3. Сообщение о своем состоянии
        StatusChanged?.Invoke(isActive);
    }

    //private void OnDrawGizmos()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //    ssc = GetComponent<SpriteShapeController>();

    //    CalculatePoints();

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(startPoint, 0.125f);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(endPoint, 0.125f);
    //}

    private void CalculatePoints()
    {
        var pointsCount = ssc.spline.GetPointCount();

        for (int i = 0; i < pointsCount; i++)
        {
            Points.Add(ssc.spline.GetPosition(i));
        }

        Points.Reverse();

        startPoint = Points[0];
        endPoint = Points[Points.Count - 1];
    }

    public void Enable()
    {
        if (prevSegment.isShowing)
        {
            var color = ssr.color;
            color.a = 1.0f;
            ssr.color = color;

            isShowing = true;
        }

        isActive = true;
        StatusChanged?.Invoke(isActive);
    }

    public void Disable()
    {
        isActive = false;
        isShowing = false;

        var color = ssr.color;
        color.a = 0.25f;
        ssr.color = color;

        StatusChanged?.Invoke(isActive);
    }

    public void SwitchActiveRailroadSegment()
    {
        if (nextSegment2 == null)
        {
            Debug.LogError("This segment doesn't have Next Segment 2");
            return;
        }

        ActiveRailroadSegment = ActiveRailroadSegment == nextSegment1 ? nextSegment2 : nextSegment1;

        if (ActiveRailroadSegment == nextSegment1)
        {
            ActiveRailroadSegmentChanged?.Invoke(false);
        }
        else
        {
            ActiveRailroadSegmentChanged?.Invoke(true);
        }

        if (ActiveRailroadSegment == nextSegment1)
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
        return ActiveRailroadSegment;
    }
}
