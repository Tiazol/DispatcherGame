using UnityEngine;

public enum WagonType
{
    Red, Green, Blue, Purple
}

public class Wagon : MonoBehaviour
{
    public float Speed { get; set; }

    private const float distanceDiff = 0.04f;
    private float distance;
    private WagonType wagonType;
    private RailroadSegment startingSegment;
    private RailroadSegment currentSegment;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public void SetType(WagonType type, Sprite sprite)
    {
        wagonType = type;
        sr.sprite = sprite;
    }

    public void SetStartingSegment(RailroadSegment segment)
    {
        currentSegment = startingSegment = segment;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            var checkpoint = collision.GetComponentInParent<Checkpoint>();

            if (checkpoint.WType == wagonType)
            {
                ProgressManager.Instance.SuccessfulWagons++;
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
                currentSegment = startingSegment;
            }
        }

        rb.MovePosition(currentSegment.GetPointAtDistance(distance));

        var rot = currentSegment.GetRotationAtDistance(distance);
        transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y + 90, rot.eulerAngles.x + 90);

        distance += Time.deltaTime * Speed;
    }

    private void Stop()
    {
        Destroy(gameObject);
    }
}
