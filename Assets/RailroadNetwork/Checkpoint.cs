using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite[] sprites;
    public WagonType WType { get; set; }
    private SpriteRenderer sr;
    public event System.Action WagonCatched;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.Instance.Checkpoints.Add(gameObject, this);
        do
        {
            WType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);
        }
        while (CheckpointsManager.Instance.UsedWagonTypes.Contains(WType));
        CheckpointsManager.Instance.UsedWagonTypes.Add(WType);
        sr.sprite = sprites[(int)WType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WagonCatched?.Invoke();
    }
}
