using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public WagonType wagonType;

    private void Start()
    {
        GameManager.Instance.Checkpoints.Add(gameObject, this);
    }
}
