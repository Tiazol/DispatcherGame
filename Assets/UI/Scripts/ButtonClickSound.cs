using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        audioSource.PlayOneShot(click);
    }
}
