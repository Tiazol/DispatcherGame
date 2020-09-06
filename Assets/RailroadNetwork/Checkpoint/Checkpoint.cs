using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public WagonType WType { get; set; }

    public event System.Action WagonPassed;

    [SerializeField] private Sprite[] sprites;
    private CheckpointsManager checkpointsManager;
    private SpriteRenderer sr;

    private void Awake()
    {
        checkpointsManager = GetComponentInParent<CheckpointsManager>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        do
        {
            var typesCount = System.Enum.GetNames(typeof(WagonType)).Length;
            WType = (WagonType)Random.Range(0, typesCount);
        }
        while (checkpointsManager.UsedWagonTypes.Contains(WType));

        checkpointsManager.UsedWagonTypes.Add(WType);
        sr.sprite = sprites[(int)WType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wagon"))
        {
            WagonPassed?.Invoke();
        }
    }
}
