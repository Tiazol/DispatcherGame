using System.Collections;
using System.Collections.Generic;

using UnityEditor.Animations;

using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        WagonGenerator.Instance.WagonPrepared += StartBlinking;
        WagonGenerator.Instance.WagonInstantiated += StopBlinking;
    }

    private void StartBlinking(WagonType type)
    {
        image.sprite = sprites[(int)type];
        animator.SetBool("IsBlinking", true);
    }

    private void StopBlinking()
    {
        animator.SetBool("IsBlinking", false);
    }
}
