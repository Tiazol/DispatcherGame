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
            var volume = GetIntFromSoundState(currentSoundState);
            mainGroup.audioMixer.SetFloat("MasterVolume", volume);
            PlayerPrefs.SetInt(pp_soundState, volume);
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

    //public void SwitchCurrentSoundState()
    //{
    //    CurrentSoundState = CurrentSoundState == SoundState.On ? SoundState.Off : SoundState.On;
    //}

    public void SwitchSoundToState(bool state)
    {
        CurrentSoundState = state ? SoundState.On : SoundState.Off;
    }

    private const string pp_soundState = "SoundState";
    [SerializeField] private AudioMixerGroup mainGroup;
    private SoundState currentSoundState;

    private void Awake()
    {
        Instance = this;

        CurrentSoundState = PlayerPrefs.HasKey(pp_soundState) ? GetSoundStateFromInt(PlayerPrefs.GetInt(pp_soundState)) : SoundState.On;
    }

    private int GetIntFromSoundState(SoundState state)
    {
        return state == SoundState.On ? 0 : -80;
    }

    private SoundState GetSoundStateFromInt(int value)
    {
        return value == 0 ? SoundState.On : SoundState.Off;
    }
}
