using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class RailroadSegment : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public RailroadSegment nextSegment;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, endPoint);
    }
}
