using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SoundIconSpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    private List<Image> images;

    private void Awake()
    {
        images = new List<Image>(GetComponentsInChildren<Image>());
        var im = GetComponent<Image>();
        images.Remove(im);
    }

    private void Start()
    {
        AudioManager.Instance.SoundStateChanged += SwitchSoundIcon;
    }

    private void SwitchSoundIcon(SoundState state)
    {
        if (state == SoundState.On)
        {
            foreach (var image in images)
            {
                image.sprite = soundOn;
            }
        }
        else
        {
            foreach (var image in images)
            {
                image.sprite = soundOff;
            }
        }
    }
}
