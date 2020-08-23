using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SoundState
{
    Off,
    On
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public SoundState CurrentSoundState
    {
        get => currentSoundState;
        private set
        {
            currentSoundState = value;
            var intValue = currentSoundState == SoundState.On ? 0 : -80;
            mainGroup.audioMixer.SetFloat("MasterVolume", intValue);
            PlayerPrefs.SetInt(pp_soundState, intValue);
            SoundStateChanged?.Invoke(currentSoundState);
        }
    }
    public event System.Action<SoundState> SoundStateChanged;

    public void SwitchOnSound()
    {
        CurrentSoundState = SoundState.On;
    }

    public void SwitchOffSound()
    {
        CurrentSoundState = SoundState.Off;
    }

    public void SwitchCurrentSoundState()
    {
        CurrentSoundState = CurrentSoundState == SoundState.On ? SoundState.Off : SoundState.On;
    }

    private const string pp_soundState = "SoundState";
    [SerializeField] private AudioMixerGroup mainGroup;
    private SoundState currentSoundState;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(pp_soundState))
        {
            CurrentSoundState = PlayerPrefs.GetInt(pp_soundState) == 1 ? SoundState.On : SoundState.Off;
        }
        else
        {
            CurrentSoundState = SoundState.On;
        }
    }
}
