using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    public Image image;
    public Text wagonsCount;
    public Sprite[] sprites;
    public WagonGenerator wagonGenerator;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        wagonGenerator.WagonPrepared += StartBlinking;
        wagonGenerator.WagonLaunched += StopBlinking;

        wagonsCount.text = $"0 / {wagonGenerator.wagonsToLaunch}";
    }

    //private void Start()
    //{
    //    WagonGenerator.Instance.WagonPrepared += StartBlinking;
    //    WagonGenerator.Instance.WagonLaunched += StopBlinking;
    //}

    private void StartBlinking(WagonType type)
    {
        image.sprite = sprites[(int)type];
        animator.SetBool("IsBlinking", true);
    }

    private void StopBlinking()
    {
        animator.SetBool("IsBlinking", false);
        wagonsCount.text = $"{wagonGenerator.PassedWagonsCount} / {wagonGenerator.wagonsToLaunch}";
    }
}
