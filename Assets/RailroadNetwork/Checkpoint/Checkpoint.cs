using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite[] sprites;
    public WagonType WType { get; set; }

    public event System.Action WagonPassed;

    private CheckpointsManager checkpointsManager;
    private SpriteRenderer sr;
    private BoxCollider2D passingCollider;
    private BoxCollider2D finishCollider;

    private void Awake()
    {
        checkpointsManager = GetComponentInParent<CheckpointsManager>();
        sr = GetComponentInChildren<SpriteRenderer>();

        var colliders = GetComponentsInChildren<BoxCollider2D>();
        if (colliders[0].transform.position.y < colliders[1].transform.position.y)
        {
            passingCollider = colliders[0];
            finishCollider = colliders[1];
        }
        else
        {
            passingCollider = colliders[1];
            finishCollider = colliders[0];
        }

        
    }

    private void Start()
    {
        do
        {
            WType = (WagonType)Random.Range(0, System.Enum.GetNames(typeof(WagonType)).Length);
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
