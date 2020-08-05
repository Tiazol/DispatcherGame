using PathCreation;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public RailroadSegment railroadSegment;
    public PathCreator path;
    private float dst;

    private void Update()
    {
        //if (Mathf.Abs(dst - railroadSegment.pathCreator.path.length) < 1.0f)
        //{
        //    dst = 0;
        //}

        //transform.position = railroadSegment.pathCreator.path.GetPointAtDistance(dst);
        transform.position = path.path.GetPointAtDistance(dst);
        transform.rotation = path.path.GetRotationAtDistance(dst);
        dst += Time.deltaTime;
    }
}
