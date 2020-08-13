using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Image[] switchGraphics;
    public RailroadSegment segment;

    public Vector2 SwipeZoneCentr => transform.TransformPoint(rectTransform.rect.center);
    public Vector2 SwipeZoneSize => rectTransform.rect.size;

    private const string animator_SwitchToLeft = "SwitchToLeft";
    private const string animator_SwitchToRight = "SwitchToRight";

    private RectTransform rectTransform;
    private State state;
    private Animator animator;
    private bool isAnimating;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        state = segment.SelectedRailroadSegment == segment.NextSegment1 ? State.Left : State.Right;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Если свайп "по горизонтали"

        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            // Если смещение вправо

            if (eventData.delta.x > 0)
            {
                Debug.Log($"{name}, вправо");
                SwitchToRight();
            }

            // Если смещение влево

            else
            {
                Debug.Log($"{name}, влево");

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
