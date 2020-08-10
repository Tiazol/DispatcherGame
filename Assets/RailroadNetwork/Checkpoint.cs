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
        do
        {
            WType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);
        }
        while (CheckpointsManager.Instance.UsedWagonTypes.Contains(WType)); // переписать инстанс в поиск в родителях?
        CheckpointsManager.Instance.UsedWagonTypes.Add(WType);
        sr.sprite = sprites[(int)WType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WagonCatched?.Invoke();
    }
}
