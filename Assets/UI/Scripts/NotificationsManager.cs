using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    public Image image;
    public Text levelNumber;
    public Text wagonsCount;
    public Sprite[] sprites;
    //public WagonGenerator wagonGenerator;

    public event Action Prepared;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        //wagonGenerator.WagonPrepared += StartBlinking;
        //wagonGenerator.WagonLaunched += StopBlinking;
    }

    private void Start()
    {
        WagonGenerator.Instance.WagonPrepared += StartBlinking;
        WagonGenerator.Instance.WagonLaunched += StopBlinking;
        WagonGenerator.Instance.StartWorking();

        levelNumber.text = $"{LocalizationManager.Instance.GetLocalizedString("level")} {GameManager.Instance.CurrentLevelNumber}";
        wagonsCount.text = $"0 / {WagonGenerator.Instance.wagonsToLaunch}";
    }

    private void StartBlinking(WagonType type)
    {
        image.sprite = sprites[(int)type];
        animator.SetBool("IsBlinking", true);
    }

    private void StopBlinking()
    {
        animator.SetBool("IsBlinking", false);
        wagonsCount.text = $"{WagonGenerator.Instance.PassedWagonsCount} / {WagonGenerator.Instance.wagonsToLaunch}";
    }
}
