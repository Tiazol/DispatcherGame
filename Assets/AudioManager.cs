using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;

    public void ToggleAudio(bool enabled)
    {
        var value = enabled ? 1.0f : 0.0f;
        audioMixerGroup.audioMixer.SetFloat("MasterVolume", value);
    }
}
