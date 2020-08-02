using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float Speed { get; set; }
    private const float distanceDiff = 0.0625f;
    private const float rotateDiff = 0.1f;
    private Quaternion rotateStep = Quaternion.Euler(1f, 1f, 1f);

    public RailroadSegment startSegment;
    private RailroadSegment currentSegment;
    private bool isMoving = true;

    private void Start()
    {
        currentSegment = startSegment;
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if (currentSegment != null)
        {
            if (Vector3.Distance(transform.position, currentSegment.endPoint) < distanceDiff)
            {
                currentSegment = currentSegment.GetNextRailroadSegment();
                
                if (currentSegment != null)
                {
                    transform.rotation = currentSegment.transform.rotation;
                    //if (Quaternion.Dot(transform.rotation, currentSegment.transform.rotation) < rotateDiff)
                    //{
                    //    StartCoroutine(Rotate());
                    //}
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

        if (WagonStop.Instance.CheckPosition(this))
        {
            Stop();
        }
    }

    private void Move()
    {
        var direction = (currentSegment.endPoint - transform.position).normalized;
        transform.position += direction * Time.deltaTime * Speed;
    }

    private IEnumerator Rotate()
    {
        transform.Rotate(rotateStep.eulerAngles*2);
        yield return null;
    }

    private void Stop()
    {
        Destroy(gameObject);
    }
}
