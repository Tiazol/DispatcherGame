using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip click;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        if (click == null)
        {
            Debug.Log("click is null", this);
        }
        audioSource.PlayOneShot(click);
    }
}
