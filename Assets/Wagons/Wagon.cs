using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float speed;
    private const float distanceDiff = 0.125f;

    public RailroadSegment startSegment;
    private RailroadSegment currentSegment;

    private void Start()
    {
        currentSegment = startSegment;
    }

    private void Update()
    {
        if (currentSegment != null)
        {
            if (Vector3.Distance(transform.position, currentSegment.endPoint) < distanceDiff)
            {
                currentSegment = currentSegment.GetNextRailroadSegment();
                
                if (currentSegment != null)
                {
                    transform.rotation = currentSegment.transform.rotation;
                    
                }
            }
        }
        else
        {
            currentSegment = startSegment;
            transform.position = currentSegment.startPoint;
            transform.rotation = currentSegment.transform.rotation;
        }

        if (currentSegment != null)
        {
            Move();
        }
    }

    private void Move()
    {
        var direction = (currentSegment.endPoint - transform.position).normalized;
        transform.position += direction * Time.deltaTime * speed;
    }
}
