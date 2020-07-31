using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float speed;
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
            if (transform.position == currentSegment.endPoint)
            {
                currentSegment = GetNewSegment();
            }
        }
        else
        {
            currentSegment = startSegment;
            transform.position = startSegment.startPoint;
        }

        if (currentSegment != null)
        {
            Move();
        }
    }

    private void Move()
    {
        var direction = (currentSegment.endPoint - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private RailroadSegment GetNewSegment()
    {
        return RailNetworkManager.Instance.GetNewRailroadSegment(currentSegment);
    }
}
