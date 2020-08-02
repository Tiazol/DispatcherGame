using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class RailroadSegment : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public RailroadSegment prevSegment;
    public RailroadSegment nextSegment1;
    public RailroadSegment nextSegment2;
    public RailroadSegment ActiveRailroadSegment { get; set; }

    private bool isActive;
    private bool isShowing;

    private SpriteRenderer sr;

    public event Action<bool> StatusChanged;
    public event Action<bool> ActiveRailroadSegmentChanged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
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

    private void Update()
    {
        CalculatePoints();
    }

    private void OnDrawGizmos()
    {
        sr = GetComponent<SpriteRenderer>();

        CalculatePoints();

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint, 0.125f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(endPoint, 0.125f);
    }

    private void CalculatePoints()
    {
        startPoint = new Vector3(
            transform.position.x + sr.size.y / 2 * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180),
            transform.position.y - sr.size.y / 2 * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180),
            transform.position.z);
        endPoint = new Vector3(
            transform.position.x - sr.size.y / 2 * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180),
            transform.position.y + sr.size.y / 2 * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180),
            transform.position.z);
    }

    public void Enable()
    {
        if (prevSegment.isShowing)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
            isShowing = true;
        }

        isActive = true;
        StatusChanged?.Invoke(isActive);
    }

    public void Disable()
    {
        isActive = false;
        isShowing = false;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
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
