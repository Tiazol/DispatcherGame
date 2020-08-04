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
    private Vector3 currentPoint;
    private WagonType wagonType;

    private Rigidbody2D rb;

    private SpriteRenderer sr;
    public Sprite[] sprites;

    private int pointIndex;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        pointIndex = 0;
        currentSegment = startSegment;
        currentPoint = startSegment.Points[pointIndex];
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.Checkpoints.ContainsKey(collision.gameObject))
        {
            var checkpoint = GameManager.Instance.Checkpoints[collision.gameObject];
            if (checkpoint.WType == wagonType)
            {
                GameManager.Instance.Score++;
            }
            else
            {
                GameManager.Instance.Score--;
            }
            Stop();
        }
    }

    private void Move()
    {
        var direction = (currentPoint - transform.position).normalized;

        //TODO: переделать физику движения в будущем
        transform.position += direction * Time.deltaTime * Speed;

        if (Vector3.Distance(currentPoint, transform.position) < distanceDiff)
        {
            if (currentPoint != currentSegment.Points[currentSegment.Points.Count - 1])
            {
                currentPoint = currentSegment.Points[pointIndex++];
            }
            else
            {
                pointIndex = 0;
                currentSegment = currentSegment.GetNextRailroadSegment();
                if (currentSegment == null)
                {
                    currentSegment = startSegment;
                    transform.position = currentSegment.startPoint;
                }
                currentPoint = currentSegment.Points[pointIndex];
            }
        }
    }

    private void Stop()
    {
        Destroy(gameObject);
    }

    public void SetWagonType(int index)
    {
        wagonType = (WagonType)index;
        sr.sprite = sprites[index];
    }
}

public enum WagonType
{
    Red, Green, Blue, Purple
}
