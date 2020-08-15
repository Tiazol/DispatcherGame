using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float Speed { get; set; } // Задается в WagonGenerator

    private const float distanceDiff = 0.0625f;
    private float distance;
    private WagonType wagonType;
    private RailroadSegment startSegment;
    private RailroadSegment currentSegment;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startSegment = RailroadManager.Instance.GetFirstRailroadSegment();
        currentSegment = startSegment;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (CheckpointsManager.Instance.Checkpoints.ContainsKey(collision.gameObject))
        //{
        //    var checkpoint = CheckpointsManager.Instance.Checkpoints[collision.gameObject];
            
        //    if (checkpoint.WType != wagonType)
        //    {
        //        ProgressManager.Instance.WrongWagons++;
        //    }
        //}

        if (collision.CompareTag("Finish"))
        {
            var checkpoint = collision.GetComponentInParent<Checkpoint>();

            if (checkpoint.WType != wagonType)
            {
                ProgressManager.Instance.WrongWagons++;
            }
        }

        if (collision.CompareTag("WagonDeleter"))
        {
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

    public void SetWagonType(WagonType type, Sprite sprite)
    {
        wagonType = type;
        sr.sprite = sprite;
    }
}

public enum WagonType
{
    Red, Green, Blue, Purple
}
