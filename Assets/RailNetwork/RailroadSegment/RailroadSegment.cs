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

    public bool isActive;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        isActive = true;
        ActiveRailroadSegment = nextSegment1;
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
        isActive = true;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
    }

    public void Disable()
    {
        isActive = false;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
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
