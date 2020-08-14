using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSound : MonoBehaviour
{
    private AudioSource audioSource;

    private float hHalfSize = 3.0f;
    private float vHalfSize = 5.0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var hRelative = transform.position.x / hHalfSize;
        var vRelative = transform.position.y / vHalfSize;
        if (Mathf.Abs(vRelative) < 0.25f)
        {
            vRelative = Mathf.Sign(vRelative) * 0.25f;
        }

        var vol = (1 - Mathf.Abs(vRelative)) * (1 - Mathf.Abs(hRelative));

        audioSource.panStereo = hRelative;

    }
}
