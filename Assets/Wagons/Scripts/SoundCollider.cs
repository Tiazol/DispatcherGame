using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollider : MonoBehaviour
{
    public AudioClip[] wheelSounds;

    private AudioSource audioSource;
    private float hHalfSize = 3.0f;
    private float vHalfSize = 5.0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fishplate"))
        {
            var clipIndex = Random.Range(0, wheelSounds.Length);
            var volModif = Random.Range(0.75f, 1.0f);

            var hRelative = transform.position.x / hHalfSize;
            var vRelative = transform.position.y / vHalfSize;
            if (Mathf.Abs(vRelative) < 0.25f)
            {
                vRelative = Mathf.Sign(vRelative) * 0.25f;
            }

            var vol = (1 - Mathf.Abs(vRelative)) * (1 - Mathf.Abs(hRelative));

            audioSource.panStereo = hRelative;
            audioSource.PlayOneShot(wheelSounds[clipIndex], vol * volModif);
        }
    }
}
    