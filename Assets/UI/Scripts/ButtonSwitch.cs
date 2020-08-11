using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public Image[] switchGraphics;

    private const string animator_SwitchToLeft = "SwitchToLeft";
    private const string animator_SwitchToRight = "SwitchToRight";

    private RailroadSegment segment;
    private Animator animator;
    private bool isAnimating;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        segment = RailroadManager.Instance.GetFirstRailroadSegment();
    }

    public void SwitchToLeft()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToLeft);
            segment.SwitchToLeft();
        }
    }

    public void SwitchToRight()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToRight);
            segment.SwitchToRight();
        }
    }

    public void Animator_EndAnimation()
    {
        isAnimating = false;
    }
}
