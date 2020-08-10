using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float Speed { get; set; } // Задается в WagonGenerator

    private const float distanceDiff = 0.0625f;

    public RailroadSegment startSegment;
    private RailroadSegment currentSegment;

    private WagonType wagonType;

    private SpriteRenderer sr;
    public Sprite[] sprites;

    private float distance;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentSegment = startSegment;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckpointsManager.Instance.Checkpoints.ContainsKey(collision.gameObject))
        {
            var checkpoint = CheckpointsManager.Instance.Checkpoints[collision.gameObject];

            if (checkpoint.WType != wagonType)
            {
                ProgressManager.Instance.WrongWagons++;
            }

            Stop();
        }
    }

    private void Move()
    {
        if (Mathf.Abs(distance - currentSegment.Length) < distanceDiff)
        {
            currentSegment = currentSegment.GetNextRailroadSegment();
            distance = 0;

            if (currentSegment == null)
            {
                currentSegment = startSegment;
            }
        }

        transform.position = currentSegment.GetPointAtDistance(distance);

        var rot = currentSegment.GetRotationAtDistance(distance);
        transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y + 90, rot.eulerAngles.x + 90);

        distance += Time.deltaTime * Speed;
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
