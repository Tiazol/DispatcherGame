using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RailroadSwitch : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RailroadSegment segment;

    private const string animator_SwitchToLeft = "SwitchToLeft";
    private const string animator_SwitchToRight = "SwitchToRight";

    private State state;
    private Animator animator;
    private bool isAnimating;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        state = segment.SelectedRailroadSegment == segment.NextSegment1 ? State.Left : State.Right;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // TODO
        //segment = RailroadManager.Instance.GetRailroadSegmentAtTopPosition()
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Если свайп "по горизонтали"

        if (/*Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y)*/ true)
        {
            // Если смещение вправо

            if (eventData.delta.x > 0)
            {
                SwitchToRight();
            }

            // Если смещение влево

            else
            {
                SwitchToLeft();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void SwitchToLeft()
    {
        if (state != State.Left && !isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToLeft);
            state = State.Left;
            segment.SwitchToLeft();
            audioSource.Play();
        }
    }

    public void SwitchToRight()
    {
        if (state != State.Right && !isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animator_SwitchToRight);
            state = State.Right;
            segment.SwitchToRight();
            audioSource.Play();
        }
    }

    public void Animator_EndAnimation()
    {
        isAnimating = false;
    }
}

public enum State
{
    Left,
    Right
}
